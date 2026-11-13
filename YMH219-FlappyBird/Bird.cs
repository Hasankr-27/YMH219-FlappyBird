namespace YMH219_FlappyBird
{
    public class Bird
    {
        public const int Width = 34;
        public const int Height = 24;

        public float X { get; private set; }
        public float Y { get; private set; }
        public float Velocity { get; private set; }

        private const float Gravity = 0.6f;
        private const float JumpPower = -9f;
        private int flapTimer = 0;   // kanat animasyonu için sayaç

        // Çarpışma kutusu: çizimden biraz küçük, oyun daha adil olsun
        public Rectangle Bounds => new Rectangle((int)X + 3, (int)Y + 3, Width - 6, Height - 6);

        public Bird(int x, int y)
        {
            Reset(x, y);
        }

        public void Jump()
        {
            Velocity = JumpPower;
            flapTimer = 10;
        }

        public void Update()
        {
            Velocity += Gravity;
            Y += Velocity;
            if (flapTimer > 0) flapTimer--;
        }

        public void Reset(int x, int y)
        {
            X = x;
            Y = y;
            Velocity = 0;
            flapTimer = 0;
        }

        public void Draw(Graphics g)
        {
            // Hıza göre eğim: yukarı çıkarken burnu kalkar, düşerken aşağı bakar
            float angle = Math.Clamp(Velocity * 4, -25, 70);

            var saved = g.Save();
            g.TranslateTransform(X + Width / 2f, Y + Height / 2f);  // kuşun merkezine git
            g.RotateTransform(angle);                                // döndür

            // Gövde
            var body = new RectangleF(-Width / 2f, -Height / 2f, Width, Height);
            g.FillEllipse(Brushes.Gold, body);
            using (var pen = new Pen(Color.FromArgb(120, 80, 0), 2))
                g.DrawEllipse(pen, body);

            // Kanat: zıpladıktan hemen sonra yukarıda, sonra aşağıda
            float wingY = flapTimer > 5 ? -8 : 0;
            var wing = new RectangleF(-12, wingY, 14, 9);
            g.FillEllipse(Brushes.Orange, wing);
            using (var pen = new Pen(Color.FromArgb(120, 80, 0), 1.5f))
                g.DrawEllipse(pen, wing);

            // Göz
            g.FillEllipse(Brushes.White, 4, -9, 10, 10);
            g.FillEllipse(Brushes.Black, 9, -6, 4, 4);

            // Gaga
            PointF[] beak = { new PointF(13, -1), new PointF(22, 3), new PointF(13, 7) };
            g.FillPolygon(Brushes.OrangeRed, beak);

            g.Restore(saved);   // döndürmeyi geri al, diğer çizimler etkilenmesin
        }
    }
}