using NAudio.Lame;
using NAudio.Wave;
using System.Timers;
using WindowSubs.GPT;
using Timer = System.Timers.Timer;

namespace WindowSubs.Audio
{
    public class AudioRecorder
    {
        private readonly WasapiLoopbackCapture capture;
        private readonly Timer timer;
        private LameMP3FileWriter writer;
        private string currentFilePath;
        private readonly Queue<string> filesQueue = new();
        private readonly CancellationTokenSource cancellationTokenSource = new();
        public event Action<string> TranscriptionReceived;
        public event Action<string> OriginalAudioToTextReceived;
        private readonly AppConfig _appConfig;

        public AudioRecorder(AppConfig appConfig)
        {
            _appConfig = appConfig;
            capture = new WasapiLoopbackCapture();
            capture.DataAvailable += OnDataAvailable;

            timer = new Timer(_appConfig.ChunkTime);
            timer.Elapsed += OnTimerElapsed;
            timer.Start();

            _ = Task.Run(QueueProcessing);

        }

        public void Start()
        {
            if (capture.CaptureState != NAudio.CoreAudioApi.CaptureState.Capturing)
            {
                StartNewRecording();
                capture.StartRecording();
            }
        }

        public void Stop()
        {
            cancellationTokenSource.Cancel();
            capture.StopRecording();
            timer.Stop();
            FinishCurrentRecording();
        }
        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (writer != null)
            {
                var stereoBuffer = ConvertToStereo(e.Buffer, e.BytesRecorded);
                writer.Write(stereoBuffer, 0, stereoBuffer.Length);
            }
        }
        private byte[] ConvertToStereo(byte[] buffer, int bytesRecorded)
        {
            using var sourceStream = new RawSourceWaveStream(buffer, 0, bytesRecorded, capture.WaveFormat);
            var stereoFormat = new WaveFormat(capture.WaveFormat.SampleRate, 2);
            using var resampled = new MediaFoundationResampler(sourceStream, stereoFormat);
            var stereoBuffer = new byte[(int)((long)resampled.WaveFormat.AverageBytesPerSecond * bytesRecorded / capture.WaveFormat.AverageBytesPerSecond)];
            using var stereoStream = new MemoryStream(stereoBuffer);
            int bytesRead;
            do
            {
                bytesRead = resampled.Read(stereoBuffer, 0, stereoBuffer.Length);
                stereoStream.Write(stereoBuffer, 0, bytesRead);
            } while (bytesRead > 0);
            return stereoStream.ToArray();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            FinishCurrentRecording();
            filesQueue.Enqueue(currentFilePath);
            StartNewRecording();
        }

        private void FinishCurrentRecording()
        {
            if (writer != null)
            {
                writer.Dispose();
                writer = null;
            }
        }

        private void StartNewRecording()
        {
            currentFilePath = $"audio_{DateTime.UtcNow:yyyyMMdd_HHmmss}.mp3";
            var stereoFormat = new WaveFormat(capture.WaveFormat.SampleRate, 2);
            writer = new LameMP3FileWriter(currentFilePath, stereoFormat, LAMEPreset.VBR_90);
        }

        private async Task QueueProcessing()
        {
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                if (filesQueue.Count > 0)
                {
                    var audioToText = await WhisperTranscribe.TranscribeAudioAsync(filesQueue.Dequeue(), _appConfig.ApiKey);
                    if (!string.IsNullOrEmpty(audioToText))
                    {
                        OriginalAudioToTextReceived?.Invoke(audioToText);
                        var translatedText = await Translate.TranslateAsync(audioToText, Enum.GetName(_appConfig.Language), _appConfig.ApiKey);
                        TranscriptionReceived?.Invoke(translatedText);
                    }
                }
            }
        }
    }
}
