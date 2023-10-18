namespace WindowSubs
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            apiKeyTextBox = new TextBox();
            apiKeyLabel = new Label();
            audioChunkLabel = new Label();
            audioChunkNumeric = new NumericUpDown();
            targetLanguageLabel = new Label();
            targetLanguageCombo = new ComboBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)audioChunkNumeric).BeginInit();
            SuspendLayout();
            // 
            // apiKeyTextBox
            // 
            apiKeyTextBox.Location = new Point(12, 47);
            apiKeyTextBox.Name = "apiKeyTextBox";
            apiKeyTextBox.Size = new Size(766, 23);
            apiKeyTextBox.TabIndex = 0;
            // 
            // apiKeyLabel
            // 
            apiKeyLabel.AutoSize = true;
            apiKeyLabel.Location = new Point(13, 24);
            apiKeyLabel.Name = "apiKeyLabel";
            apiKeyLabel.Size = new Size(74, 15);
            apiKeyLabel.TabIndex = 1;
            apiKeyLabel.Text = "GPT Api Key:";
            // 
            // audioChunkLabel
            // 
            audioChunkLabel.AutoSize = true;
            audioChunkLabel.Location = new Point(13, 90);
            audioChunkLabel.Name = "audioChunkLabel";
            audioChunkLabel.Size = new Size(112, 15);
            audioChunkLabel.TabIndex = 2;
            audioChunkLabel.Text = "Audio Chunk in ms:";
            // 
            // audioChunkNumeric
            // 
            audioChunkNumeric.Location = new Point(18, 116);
            audioChunkNumeric.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            audioChunkNumeric.Name = "audioChunkNumeric";
            audioChunkNumeric.Size = new Size(120, 23);
            audioChunkNumeric.TabIndex = 3;
            audioChunkNumeric.Value = new decimal(new int[] { 30000, 0, 0, 0 });
            // 
            // targetLanguageLabel
            // 
            targetLanguageLabel.AutoSize = true;
            targetLanguageLabel.Location = new Point(209, 90);
            targetLanguageLabel.Name = "targetLanguageLabel";
            targetLanguageLabel.Size = new Size(97, 15);
            targetLanguageLabel.TabIndex = 4;
            targetLanguageLabel.Text = "Target Language:";
            // 
            // targetLanguageCombo
            // 
            targetLanguageCombo.FormattingEnabled = true;
            targetLanguageCombo.Location = new Point(209, 115);
            targetLanguageCombo.Name = "targetLanguageCombo";
            targetLanguageCombo.Size = new Size(164, 23);
            targetLanguageCombo.TabIndex = 5;
            // 
            // button1
            // 
            button1.Location = new Point(703, 116);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 6;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 168);
            Controls.Add(button1);
            Controls.Add(targetLanguageCombo);
            Controls.Add(targetLanguageLabel);
            Controls.Add(audioChunkNumeric);
            Controls.Add(audioChunkLabel);
            Controls.Add(apiKeyLabel);
            Controls.Add(apiKeyTextBox);
            MaximizeBox = false;
            Name = "Settings";
            Text = "Config";
            ((System.ComponentModel.ISupportInitialize)audioChunkNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox apiKeyTextBox;
        private Label apiKeyLabel;
        private Label audioChunkLabel;
        private NumericUpDown audioChunkNumeric;
        private Label targetLanguageLabel;
        private ComboBox targetLanguageCombo;
        private Button button1;
    }
}