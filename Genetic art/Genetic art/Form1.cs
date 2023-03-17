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
            Image.LockBits(new Rectangle(0, 0, (int)Original.Image.HorizontalResolution, (int)Original.Image.VerticalResolution), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);
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