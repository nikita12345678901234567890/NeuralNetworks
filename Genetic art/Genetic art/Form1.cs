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
            
            moira = new ArtTrainer(Image, 13, 10);
        }

        private void doButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                moira.Train(random);
                Output.Image = moira.GetBestImage(Output.Width, Output.Height);
                Output.Update();
            }

            Output.Image = moira.GetBestImage(Output.Width, Output.Height);
        }
    }
}