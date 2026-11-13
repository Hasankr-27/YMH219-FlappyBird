using System.Drawing.Drawing2D;

namespace YMH219_FlappyBird
{
    public class Pipe
    {
        public const int Width = 60;
        private const int GapSize = 160;
        private const int CapHeight = 24;    // borunun ağız kısmı
        private const int CapOverhang = 4;   // ağız gövdeden ne kadar taşar

        private readonly int areaHeight;     // oynanabilir alan (zemin hariç)
        private static readonly Random random = new Random();

        public int X { get; private set; }
        public int GapTop { get; private set; }
        public bool Passed { get; set; }

        public int Right => X + Width;
        public bool IsOffScreen => Right < 0;

        public Rectangle TopRect => new Rectangle(X, 0, Width, GapTop);
        public Rectangle BottomRect => new Rectangle(X, GapTop + GapSize, Width,
                                                     areaHeight - GapTop - GapSize);

        public Pipe(int x, int areaHeight)
        {
            this.areaHeight = areaHeight;
            Reset(x);
        }

        public void Reset(int x)
        {
            X = x;
            GapTop = random.Next(80, areaHeight - 80 - GapSize);
            Passed = false;
        }

        public void Move(int speed)
        {
            X -= speed;
        }

        public bool CollidesWith(Rectangle area)
        {
            return TopRect.IntersectsWith(area) || BottomRect.IntersectsWith(area);
        }

        public void Draw(Graphics g)
        {
            DrawPart(g, TopRect, capAtBottom: true);
            DrawPart(g, BottomRect, capAtBottom: false);
        }

        private static void DrawPart(Graphics g, Rectangle body, bool capAtBottom)
        {
            if (body.Height <= 0) return;

            Color dark = Color.FromArgb(40, 110, 30);
            Color light = Color.FromArgb(120, 200, 80);

            using var edge = new Pen(Color.FromArgb(30, 70, 20), 2);

            // Gövde (soldan sağa renk geçişi, 3D hissi verir)
            using (var brush = new LinearGradientBrush(body, dark, light, LinearGradientMode.Horizontal))
                g.FillRectangle(brush, body);
            g.DrawRectangle(edge, body);

            // Ağız kısmı (boşluğa bakan uç)
            int capY = capAtBottom ? body.Bottom - CapHeight : body.Top;
            var cap = new Rectangle(body.X - CapOverhang, capY, body.Width + CapOverhang * 2, CapHeight);
            using (var brush = new LinearGradientBrush(cap, dark, light, LinearGradientMode.Horizontal))
                g.FillRectangle(brush, cap);
            g.DrawRectangle(edge, cap);
        }
    }
}