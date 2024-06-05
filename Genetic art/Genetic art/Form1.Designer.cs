namespace Genetic_art
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Original = new PictureBox();
            doButton = new Button();
            iLabel = new Label();
            OutputPanel = new Panel();
            bestLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)Original).BeginInit();
            SuspendLayout();
            // 
            // Original
            // 
            Original.Image = (Image)resources.GetObject("Original.Image");
            Original.Location = new Point(12, 12);
            Original.Name = "Original";
            Original.Size = new Size(150, 150);
            Original.SizeMode = PictureBoxSizeMode.AutoSize;
            Original.TabIndex = 0;
            Original.TabStop = false;
            // 
            // doButton
            // 
            doButton.Location = new Point(1683, 12);
            doButton.Name = "doButton";
            doButton.Size = new Size(75, 23);
            doButton.TabIndex = 2;
            doButton.Text = "Do";
            doButton.UseVisualStyleBackColor = true;
            doButton.Click += doButton_Click;
            // 
            // iLabel
            // 
            iLabel.AutoSize = true;
            iLabel.Location = new Point(1745, 48);
            iLabel.Name = "iLabel";
            iLabel.Size = new Size(13, 15);
            iLabel.TabIndex = 3;
            iLabel.Text = "0";
            // 
            // OutputPanel
            // 
            OutputPanel.Location = new Point(167, 12);
            OutputPanel.Name = "OutputPanel";
            OutputPanel.Size = new Size(1510, 828);
            OutputPanel.TabIndex = 13;
            // 
            // bestLabel
            // 
            bestLabel.AutoSize = true;
            bestLabel.Location = new Point(1745, 136);
            bestLabel.Name = "bestLabel";
            bestLabel.Size = new Size(13, 15);
            bestLabel.TabIndex = 14;
            bestLabel.Text = "0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1770, 852);
            Controls.Add(bestLabel);
            Controls.Add(iLabel);
            Controls.Add(doButton);
            Controls.Add(Original);
            Controls.Add(OutputPanel);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)Original).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox Original;
        private Button doButton;
        private Label iLabel;
        private Panel OutputPanel;
        private Label bestLabel;
    }
}