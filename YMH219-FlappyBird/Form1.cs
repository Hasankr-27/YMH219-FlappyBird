namespace YMH219_FlappyBird
{
    public partial class Form1 : Form
    {
        private Bird bird;
        private System.Windows.Forms.Timer gameTimer;

        private List<Pipe> pipes = new List<Pipe>();
        private int pipeSpeed = 4;
        private const int PipeSpacing = 250;

        private bool isGameOver = false;   // YENİ
        private Label lblGameOver;         // YENİ

        public Form1()
        {
            InitializeComponent();

            ClientSize = new Size(400, 600);
            BackColor = Color.SkyBlue;
            DoubleBuffered = true;
            KeyPreview = true;

            bird = new Bird(80, 250);
            Controls.Add(bird.Sprite);

            for (int i = 0; i < 2; i++)
            {
                var pipe = new Pipe(ClientSize.Width + i * PipeSpacing, ClientSize.Height);
                pipes.Add(pipe);
                Controls.Add(pipe.TopPipe);
                Controls.Add(pipe.BottomPipe);
            }

            // YENİ: Game Over yazısı (başta gizli)
            lblGameOver = new Label
            {
                Text = "GAME OVER",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Firebrick,
                AutoSize = true,
                Visible = false
            };
            Controls.Add(lblGameOver);

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 20;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            KeyDown += Form1_KeyDown;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            bird.Update();

            foreach (var pipe in pipes)
            {
                pipe.Move(pipeSpeed);

                if (pipe.IsOffScreen)
                    pipe.Reset(pipe.X + pipes.Count * PipeSpacing);

                // YENİ: boruya çarptı mı?
                if (pipe.CollidesWith(bird.Sprite.Bounds))
                {
                    GameOver();
                    return;
                }
            }

            // YENİ: yere veya tavana çarptı mı?
            if (bird.Sprite.Bottom >= ClientSize.Height || bird.Sprite.Top <= 0)
            {
                GameOver();
            }
        }

        // YENİ: oyunu bitirir
        private void GameOver()
        {
            isGameOver = true;
            gameTimer.Stop();

            // yazıyı ortala ve en öne getir
            lblGameOver.Location = new Point(
                (ClientSize.Width - lblGameOver.Width) / 2,
                (ClientSize.Height - lblGameOver.Height) / 2);
            lblGameOver.Visible = true;
            lblGameOver.BringToFront();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !isGameOver)   // YENİ: oyun bittiyse zıplama
                bird.Jump();
        }
    }
}