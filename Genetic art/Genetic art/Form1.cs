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
            
            moira = new ArtTrainer(Image, 4, 10);
        }

        private void doButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                iLabel.Text = i.ToString();
                iLabel.Update();

                moira.Train(random);
                Output1.Image = moira.GetBestImage(Original.Image.Width, Original.Image.Height);//Output.Width, Output.Height);
                Output1.Update();
            }
        }
    }
}