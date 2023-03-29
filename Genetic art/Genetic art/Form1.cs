namespace Genetic_art
{
    public partial class Form1 : Form
    {
        ArtTrainer moira;
        Random random = new Random();
        Bitmap Image;
        public Form1()
        {
            InitializeComponent();
            Image = new Bitmap(Original.Image);
            
            moira = new ArtTrainer(Image, 5, 20);
        }

        private void doButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 150; i++)
            {
                iLabel.Text = i.ToString();
                iLabel.Update();

                moira.Train(random);

                for (int j = 0; j < OutputPanel.Controls.Count; j++)
                {
                    PictureBox box = (PictureBox)OutputPanel.Controls[j];

                    box.Image = moira.GetImage(Original.Image.Width, Original.Image.Height, j);
                    box.Update();
                }
            }
        }
    }
}