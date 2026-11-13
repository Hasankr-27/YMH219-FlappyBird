using System.Drawing.Drawing2D;

namespace YMH219_FlappyBird
{
    public class Background
    {
        public const int GroundHeight = 80;   // zemin yüksekliği

        private readonly Size size;
        private readonly List<PointF> clouds = new List<PointF>();
        private float groundOffset = 0;
        private static readonly Random random = new Random();

        public Background(Size size)
        {
            this.size = size;
            for (int i = 0; i < 4; i++)
                clouds.Add(new PointF(i * 120 + random.Next(0, 60), random.Next(30, 250)));
        }

        public void Update(int speed)
        {
            // Bulutlar borulardan yavaş kayar, derinlik hissi verir
            for (int i = 0; i < clouds.Count; i++)
            {
                var c = clouds[i];
                c.X -= speed * 0.3f;
                if (c.X < -100)
                    c = new PointF(size.Width + random.Next(0, 80), random.Next(30, 250));
                clouds[i] = c;
            }

            // Zemindeki çizgiler borularla aynı hızda kayar
            groundOffset = (groundOffset + speed) % 24;
        }

        public void DrawSky(Graphics g)
        {
            var skyRect = new Rectangle(0, 0, size.Width, size.Height - GroundHeight);
            using (var sky = new LinearGradientBrush(skyRect,
                       Color.FromArgb(80, 180, 240), Color.FromArgb(200, 235, 255),
                       LinearGradientMode.Vertical))
            {
                g.FillRectangle(sky, skyRect);
            }

            foreach (var c in clouds)
                DrawCloud(g, c.X, c.Y);
        }

        public void DrawGround(Graphics g)
        {
            int top = size.Height - GroundHeight;

            using (var dirt = new SolidBrush(Color.FromArgb(222, 200, 140)))
                g.FillRectangle(dirt, 0, top, size.Width, GroundHeight);

            using (var grass = new SolidBrush(Color.FromArgb(110, 190, 60)))
                g.FillRectangle(grass, 0, top, size.Width, 14);

            // Çimdeki kayan çizgiler
            using (var stripe = new SolidBrush(Color.FromArgb(90, 160, 45)))
            {
                for (float x = -groundOffset; x < size.Width + 24; x += 24)
                {
                    g.FillPolygon(stripe, new[]
                    {
                        new PointF(x, top), new PointF(x + 12, top),
                        new PointF(x + 6, top + 14), new PointF(x - 6, top + 14)
                    });
                }
            }

            using (var line = new Pen(Color.FromArgb(80, 60, 30), 3))
                g.DrawLine(line, 0, top, size.Width, top);
        }

        private static void DrawCloud(Graphics g, float x, float y)
        {
            // Üç beyaz elips üst üste binince bulut oluyor
            g.FillEllipse(Brushes.White, x, y + 10, 50, 30);
            g.FillEllipse(Brushes.White, x + 25, y, 50, 40);
            g.FillEllipse(Brushes.White, x + 55, y + 10, 45, 30);
        }
    }
}