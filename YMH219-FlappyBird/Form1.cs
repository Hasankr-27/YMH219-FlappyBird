using System.Drawing.Drawing2D;

namespace YMH219_FlappyBird
{
    public partial class Form1 : Form
    {
        private Bird bird;
        private Background background;
        private GameState state = new GameState();
        private List<Pipe> pipes = new List<Pipe>();
        private System.Windows.Forms.Timer gameTimer;
        private Button btnRestart;

        private const int PipeSpacing = 250;
        private const int BirdStartX = 80;
        private const int BirdStartY = 250;

        // Zemin hariç oynanabilir yükseklik
        private int PlayHeight => ClientSize.Height - Background.GroundHeight;

        public Form1()
        {
            InitializeComponent();

            ClientSize = new Size(400, 600);
            FormBorderStyle = FormBorderStyle.FixedSingle;   // pencere boyutu sabit
            MaximizeBox = false;
            DoubleBuffered = true;
            KeyPreview = true;
            Text = "Flappy Bird";

            bird = new Bird(BirdStartX, BirdStartY);
            background = new Background(ClientSize);

            for (int i = 0; i < 2; i++)
                pipes.Add(new Pipe(ClientSize.Width + i * PipeSpacing, PlayHeight));

            btnRestart = new Button
            {
                Text = "Yeniden Başlat",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(160, 44),
                BackColor = Color.Orange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Visible = false
            };
            btnRestart.FlatAppearance.BorderSize = 0;
            btnRestart.Location = new Point((ClientSize.Width - btnRestart.Width) / 2, 320);
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
            background.Update(state.PipeSpeed);

            if (state.IsStarted)
            {
                bird.Update();

                foreach (var pipe in pipes)
                {
                    pipe.Move(state.PipeSpeed);

                    if (pipe.IsOffScreen)
                        pipe.Reset(pipe.X + pipes.Count * PipeSpacing);

                    if (!pipe.Passed && pipe.Right < bird.Bounds.Left)
                    {
                        pipe.Passed = true;
                        state.AddPoint();
                    }

                    if (pipe.CollidesWith(bird.Bounds))
                    {
                        GameOver();
                        return;
                    }
                }

                if (bird.Bounds.Bottom >= PlayHeight || bird.Y <= 0)
                {
                    GameOver();
                    return;
                }
            }

            Invalidate();   // ekranı yeniden çiz -> OnPaint çalışır
        }

        // Her karede tüm sahne burada çiziliyor (sıra önemli: arkadan öne)
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;   // kenarlar yumuşak olsun

            background.DrawSky(g);
            foreach (var pipe in pipes)
                pipe.Draw(g);
            background.DrawGround(g);
            bird.Draw(g);

            DrawOutlinedText(g, state.Score.ToString(), 44, 30);

            if (!state.IsStarted)
                DrawOutlinedText(g, "SPACE ile başla", 24, 180);

            if (state.IsGameOver)
                DrawGameOverPanel(g);
        }

        // Kenarlıklı beyaz yazı (oyunlardaki gibi)
        private void DrawOutlinedText(Graphics g, string text, float size, int y)
        {
            using var path = new GraphicsPath();
            using var family = new FontFamily("Segoe UI");
            using var format = new StringFormat { Alignment = StringAlignment.Center };

            path.AddString(text, family, (int)FontStyle.Bold, size,
                           new RectangleF(0, y, ClientSize.Width, size * 2), format);

            using var outline = new Pen(Color.FromArgb(60, 40, 20), 6) { LineJoin = LineJoin.Round };
            g.DrawPath(outline, path);
            g.FillPath(Brushes.White, path);
        }

        private void DrawGameOverPanel(Graphics g)
        {
            var panel = new Rectangle(60, 160, ClientSize.Width - 120, 230);

            using (var shadow = new SolidBrush(Color.FromArgb(80, 0, 0, 0)))
                g.FillRectangle(shadow, panel.X + 6, panel.Y + 6, panel.Width, panel.Height);
            using (var fill = new SolidBrush(Color.FromArgb(250, 235, 190)))
                g.FillRectangle(fill, panel);
            using (var border = new Pen(Color.FromArgb(120, 80, 40), 4))
                g.DrawRectangle(border, panel);

            DrawOutlinedText(g, "GAME OVER", 34, 180);

            using var font = new Font("Segoe UI", 16, FontStyle.Bold);
            using var brush = new SolidBrush(Color.FromArgb(120, 80, 40));
            using var format = new StringFormat { Alignment = StringAlignment.Center };
            g.DrawString("Skor: " + state.Score, font, brush,
                         new RectangleF(0, 250, ClientSize.Width, 40), format);
        }

        private void GameOver()
        {
            state.End();
            gameTimer.Stop();
            btnRestart.Visible = true;
            Invalidate();
        }

        private void RestartGame()
        {
            state.Reset();
            bird.Reset(BirdStartX, BirdStartY);

            for (int i = 0; i < pipes.Count; i++)
                pipes[i].Reset(ClientSize.Width + i * PipeSpacing);

            btnRestart.Visible = false;
            ActiveControl = null;
            gameTimer.Start();
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !state.IsGameOver)
            {
                if (!state.IsStarted)
                    state.Start();   // ilk Space oyunu başlatır
                bird.Jump();
            }

            if (e.KeyCode == Keys.Enter && state.IsGameOver)
                RestartGame();
        }
    }
}