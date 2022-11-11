namespace MonteCarlo
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
            this.TicTacButton = new System.Windows.Forms.Button();
            this.ChackersButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TicTacButton
            // 
            this.TicTacButton.Location = new System.Drawing.Point(195, 80);
            this.TicTacButton.Name = "TicTacButton";
            this.TicTacButton.Size = new System.Drawing.Size(75, 23);
            this.TicTacButton.TabIndex = 0;
            this.TicTacButton.Text = "ToeTicTac";
            this.TicTacButton.UseVisualStyleBackColor = true;
            this.TicTacButton.Click += new System.EventHandler(this.TicTacButton_Click);
            // 
            // ChackersButton
            // 
            this.ChackersButton.Location = new System.Drawing.Point(347, 80);
            this.ChackersButton.Name = "ChackersButton";
            this.ChackersButton.Size = new System.Drawing.Size(75, 23);
            this.ChackersButton.TabIndex = 1;
            this.ChackersButton.Text = "Chackers";
            this.ChackersButton.UseVisualStyleBackColor = true;
            this.ChackersButton.Click += new System.EventHandler(this.ChackersButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 614);
            this.Controls.Add(this.ChackersButton);
            this.Controls.Add(this.TicTacButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button TicTacButton;
        private Button ChackersButton;
    }
}