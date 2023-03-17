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
            this.Original = new System.Windows.Forms.PictureBox();
            this.Output = new System.Windows.Forms.PictureBox();
            this.doButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Original)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Output)).BeginInit();
            this.SuspendLayout();
            // 
            // Original
            // 
            this.Original.Image = ((System.Drawing.Image)(resources.GetObject("Original.Image")));
            this.Original.Location = new System.Drawing.Point(12, 12);
            this.Original.Name = "Original";
            this.Original.Size = new System.Drawing.Size(515, 585);
            this.Original.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Original.TabIndex = 0;
            this.Original.TabStop = false;
            // 
            // Output
            // 
            this.Output.Location = new System.Drawing.Point(533, 12);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(515, 585);
            this.Output.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Output.TabIndex = 1;
            this.Output.TabStop = false;
            // 
            // doButton
            // 
            this.doButton.Location = new System.Drawing.Point(1054, 12);
            this.doButton.Name = "doButton";
            this.doButton.Size = new System.Drawing.Size(75, 23);
            this.doButton.TabIndex = 2;
            this.doButton.Text = "Do";
            this.doButton.UseVisualStyleBackColor = true;
            this.doButton.Click += new System.EventHandler(this.doButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 609);
            this.Controls.Add(this.doButton);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.Original);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Original)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Output)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox Original;
        private PictureBox Output;
        private Button doButton;
    }
}