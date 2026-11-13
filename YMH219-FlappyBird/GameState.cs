namespace YMH219_FlappyBird
{
    public class GameState
    {
        public int Score { get; private set; }
        public int HighScore { get; private set; }
        public bool IsNewRecord { get; private set; }
        public bool IsStarted { get; private set; }
        public bool IsGameOver { get; private set; }
        public int PipeSpeed { get; private set; }

        private const int StartSpeed = 4;

        // Rekor, programın çalıştığı klasördeki bu dosyada tutulur
        private static readonly string SaveFile =
            Path.Combine(AppContext.BaseDirectory, "highscore.txt");

        public GameState()
        {
            LoadHighScore();
            Reset();
        }

        public void Start() => IsStarted = true;
        public void AddPoint() => Score++;

        public void End()
        {
            IsGameOver = true;

            if (Score > HighScore)
            {
                HighScore = Score;
                IsNewRecord = true;
                SaveHighScore();
            }
        }

        public void Reset()
        {
            Score = 0;
            IsNewRecord = false;
            IsStarted = false;
            IsGameOver = false;
            PipeSpeed = StartSpeed;
        }

        private void LoadHighScore()
        {
            try
            {
                if (File.Exists(SaveFile) && int.TryParse(File.ReadAllText(SaveFile), out int saved))
                    HighScore = saved;
            }
            catch (IOException)
            {
                HighScore = 0;   // dosya okunamazsa oyun yine de çalışsın
            }
        }

        private void SaveHighScore()
        {
            try
            {
                File.WriteAllText(SaveFile, HighScore.ToString());
            }
            catch (IOException)
            {
                // kaydedilemezse oyunu bozmayalım
            }
        }
    }
}