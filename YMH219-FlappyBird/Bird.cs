namespace YMH219_FlappyBird
{
    public class Bird
    {
        // Ekranda görünen kuş (şimdilik sarı kutu)
        public PictureBox Sprite { get; }

        // Kuşun dikey hızı: pozitifse aşağı, negatifse yukarı gider
        public float Velocity { get; private set; }

        private const float Gravity = 0.6f;
        private const float JumpPower = -9f;

        public Bird(int x, int y)
        {
            Sprite = new PictureBox
            {
                Size = new Size(34, 24),
                Location = new Point(x, y),
                BackColor = Color.Gold
            };
        }

        public void Jump()
        {
            Velocity = JumpPower;
        }

        public void Update()
        {
            Velocity += Gravity;
            Sprite.Top += (int)Velocity;
        }
    }
}