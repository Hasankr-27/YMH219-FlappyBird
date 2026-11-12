namespace YMH219_FlappyBird
{
    public class Bird
    {
        public PictureBox Sprite { get; }
        public float Velocity { get; private set; }

        private const float Gravity = 0.6f;
        private const float JumpPower = -9f;

        public Bird(int x, int y)
        {
            Sprite = new PictureBox
            {
                Size = new Size(34, 24),
                BackColor = Color.Gold
            };
            Reset(x, y);
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

        // Kuşu başlangıç noktasına koyar, hızını sıfırlar
        public void Reset(int x, int y)
        {
            Sprite.Location = new Point(x, y);
            Velocity = 0;
        }
    }
}