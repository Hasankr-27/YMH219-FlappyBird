namespace YMH219_FlappyBird
{
    public class Pipe
    {
        public PictureBox TopPipe { get; }
        public PictureBox BottomPipe { get; }

        private const int PipeWidth = 60;
        private const int GapSize = 160;     // kuşun geçeceği boşluk
        private readonly int formHeight;
        private static readonly Random random = new Random();

        // Borunun yatay konumu
        public int X => TopPipe.Left;

        // Ekranın solundan tamamen çıktı mı?
        public bool IsOffScreen => TopPipe.Right < 0;

        public Pipe(int x, int formHeight)
        {
            this.formHeight = formHeight;
            TopPipe = new PictureBox { BackColor = Color.ForestGreen };
            BottomPipe = new PictureBox { BackColor = Color.ForestGreen };
            Reset(x);
        }

        // Boruyu verilen x'e koyar, boşluğu rastgele bir yüksekliğe ayarlar
        public void Reset(int x)
        {
            int gapTop = random.Next(100, formHeight - 100 - GapSize);

            TopPipe.SetBounds(x, 0, PipeWidth, gapTop);
            BottomPipe.SetBounds(x, gapTop + GapSize, PipeWidth,
                                 formHeight - gapTop - GapSize);
        }

        public void Move(int speed)
        {
            TopPipe.Left -= speed;
            BottomPipe.Left -= speed;
        }
    }
}