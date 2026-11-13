namespace YMH219_FlappyBird
{
    public class GameState
    {
        public int Score { get; private set; }
        public bool IsStarted { get; private set; }
        public bool IsGameOver { get; private set; }
        public int PipeSpeed { get; private set; }

        private const int StartSpeed = 4;

        public GameState()
        {
            Reset();
        }

        public void Start() => IsStarted = true;
        public void AddPoint() => Score++;
        public void End() => IsGameOver = true;

        public void Reset()
        {
            Score = 0;
            IsStarted = false;
            IsGameOver = false;
            PipeSpeed = StartSpeed;
        }
    }
}