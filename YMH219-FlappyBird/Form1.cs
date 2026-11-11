namespace YMH219_FlappyBird
{
    public partial class Form1 : Form
    {
        private Bird bird;
        private System.Windows.Forms.Timer gameTimer;

        public Form1()
        {
            InitializeComponent();

            // Pencere ayarları
            ClientSize = new Size(400, 600);
            BackColor = Color.SkyBlue;
            DoubleBuffered = true;   // titremeyi azaltır
            KeyPreview = true;       // tuş basışlarını form yakalasın

            // Kuşu oluştur ve forma ekle
            bird = new Bird(80, 250);
            Controls.Add(bird.Sprite);

            // Oyun döngüsü: her 20 ms'de bir Tick çalışır
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 20;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            KeyDown += Form1_KeyDown;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            bird.Update();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                bird.Jump();
        }
    }
}
