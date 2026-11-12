namespace YMH219_FlappyBird
{
    public partial class Form1 : Form
    {
        private Bird bird;
        private System.Windows.Forms.Timer gameTimer;

        private List<Pipe> pipes = new List<Pipe>();   // YENİ
        private int pipeSpeed = 4;                       // YENİ
        private const int PipeSpacing = 250;             // YENİ: borular arası mesafe

        public Form1()
        {
            InitializeComponent();

            ClientSize = new Size(400, 600);
            BackColor = Color.SkyBlue;
            DoubleBuffered = true;
            KeyPreview = true;

            bird = new Bird(80, 250);
            Controls.Add(bird.Sprite);

            // YENİ: iki boru çifti oluştur, ekranın sağından başlasınlar
            for (int i = 0; i < 2; i++)
            {
                var pipe = new Pipe(ClientSize.Width + i * PipeSpacing, ClientSize.Height);
                pipes.Add(pipe);
                Controls.Add(pipe.TopPipe);
                Controls.Add(pipe.BottomPipe);
            }

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 20;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            KeyDown += Form1_KeyDown;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            bird.Update();

            // YENİ: boruları kaydır, ekrandan çıkanı en arkaya at
            foreach (var pipe in pipes)
            {
                pipe.Move(pipeSpeed);

                if (pipe.IsOffScreen)
                    pipe.Reset(pipe.X + pipes.Count * PipeSpacing);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                bird.Jump();
        }
    }
}