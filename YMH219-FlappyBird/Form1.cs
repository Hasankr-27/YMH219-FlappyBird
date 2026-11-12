namespace YMH219_FlappyBird
{
    public partial class Form1 : Form
    {
        private Bird bird;
        private GameState state = new GameState();
        private List<Pipe> pipes = new List<Pipe>();
        private System.Windows.Forms.Timer gameTimer;

        private const int PipeSpacing = 250;
        private const int BirdStartX = 80;
        private const int BirdStartY = 250;

        private Label lblScore;
        private Label lblGameOver;
        private Button btnRestart;

        public Form1()
        {
            InitializeComponent();

            ClientSize = new Size(400, 600);
            BackColor = Color.SkyBlue;
            DoubleBuffered = true;
            KeyPreview = true;
            Text = "Flappy Bird";

            bird = new Bird(BirdStartX, BirdStartY);
            Controls.Add(bird.Sprite);

            for (int i = 0; i < 2; i++)
            {
                var pipe = new Pipe(ClientSize.Width + i * PipeSpacing, ClientSize.Height);
                pipes.Add(pipe);
                Controls.Add(pipe.TopPipe);
                Controls.Add(pipe.BottomPipe);
            }

            // Skor yazısı (sol üst)
            lblScore = new Label
            {
                Text = "Skor: 0",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(10, 10)
            };
            Controls.Add(lblScore);
            lblScore.BringToFront();   // boruların önünde görünsün

            // Game Over yazısı
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

            // Yeniden başlat butonu
            btnRestart = new Button
            {
                Text = "Yeniden Başlat",
                Font = new Font("Segoe UI", 12),
                Size = new Size(160, 40),
                Visible = false
            };
            btnRestart.Click += (s, e) => RestartGame();
            Controls.Add(btnRestart);

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
                pipe.Move(state.PipeSpeed);

                if (pipe.IsOffScreen)
                    pipe.Reset(pipe.X + pipes.Count * PipeSpacing);

                // Boruyu geçti mi? Geçtiyse skor +1
                if (!pipe.Passed && pipe.Right < bird.Sprite.Left)
                {
                    pipe.Passed = true;
                    state.AddPoint();
                    lblScore.Text = "Skor: " + state.Score;
                }

                if (pipe.CollidesWith(bird.Sprite.Bounds))
                {
                    GameOver();
                    return;
                }
            }

            if (bird.Sprite.Bottom >= ClientSize.Height || bird.Sprite.Top <= 0)
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            state.End();
            gameTimer.Stop();

            lblGameOver.Location = new Point(
                (ClientSize.Width - lblGameOver.Width) / 2,
                ClientSize.Height / 2 - 80);
            lblGameOver.Visible = true;
            lblGameOver.BringToFront();

            btnRestart.Location = new Point(
                (ClientSize.Width - btnRestart.Width) / 2,
                ClientSize.Height / 2);
            btnRestart.Visible = true;
            btnRestart.BringToFront();
        }

        private void RestartGame()
        {
            state.Reset();
            bird.Reset(BirdStartX, BirdStartY);

            for (int i = 0; i < pipes.Count; i++)
                pipes[i].Reset(ClientSize.Width + i * PipeSpacing);

            lblScore.Text = "Skor: 0";
            lblGameOver.Visible = false;
            btnRestart.Visible = false;

            ActiveControl = null;   // odak butonda kalmasın, Space oyuna gitsin
            gameTimer.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !state.IsGameOver)
                bird.Jump();

            // Oyun bitince Enter ile de yeniden başlasın
            if (e.KeyCode == Keys.Enter && state.IsGameOver)
                RestartGame();
        }
    }
}