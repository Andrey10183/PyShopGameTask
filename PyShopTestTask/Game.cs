namespace PyShopTestTask
{
    public class Game
    {
        const int TIMESTAMPS_COUNT = 10;

        const double PROBABILITY_SCORE_CHANGED = 0.5;

        const double PROBABILITY_HOME_SCORE = 0.45;

        const int OFFSET_MAX_STEP = 3;

        GameStamp[] gameStamps;

        public Game()
        {
            this.gameStamps = new GameStamp[] { };
        }

        public Game(GameStamp[] gameStamps)
        {
            this.gameStamps = gameStamps;
        }

        GameStamp generateGameStamp(GameStamp previousValue)
        {
            Random rand = new Random();

            bool scoreChanged = rand.NextDouble() > 1 - PROBABILITY_SCORE_CHANGED;
            int homeScoreChange = scoreChanged && rand.NextDouble() > 1 - PROBABILITY_HOME_SCORE ? 1 : 0;
            int awayScoreChange = scoreChanged && homeScoreChange == 0 ? 1 : 0;
            int offsetChange = (int)(Math.Floor(rand.NextDouble() * OFFSET_MAX_STEP)) + 1;

            return new GameStamp(
                previousValue.offset + offsetChange,
                previousValue.score.home + homeScoreChange,
                previousValue.score.away + awayScoreChange
                );
        }

        static Game generateGame()
        {
            Game game = new Game();
            game.gameStamps = new GameStamp[TIMESTAMPS_COUNT];

            GameStamp currentStamp = new GameStamp(0, 0, 0);
            for (int i = 0; i < TIMESTAMPS_COUNT; i++)
            {
                game.gameStamps[i] = currentStamp;
                currentStamp = game.generateGameStamp(currentStamp);
            }

            return game;
        }

        public static void task1()
        {
            Game game = generateGame();
            game.printGameStamps();
        }

        public void printGameStamps()
        {
            foreach (GameStamp stamp in this.gameStamps)
            {
                Console.WriteLine($"{stamp.offset}: {stamp.score.home}-{stamp.score.away}");
            }
        }

        public string PrintScore(Score score)
        {
            return $"{score.home}-{score.away}";
        }

        public Score getScore(int offset)
        {
            if (gameStamps == null || gameStamps.Length == 0) return new Score(0,0);

            var right = gameStamps.Length - 1;
            var left = 0;
            int middle;

            var upLimit = gameStamps[gameStamps.Length - 1].offset;
            if (offset > upLimit) return gameStamps[gameStamps.Length - 1].score;
            if (offset < 0) return gameStamps[0].score;
            if (gameStamps[right].offset == offset) return gameStamps[right].score;

            while (
                gameStamps[right].offset != offset &&
                gameStamps[left].offset != offset &&
                right - left > 1)
            {
                middle = (right + left) / 2;
                if (gameStamps[middle].offset > offset)
                    right = middle;
                else
                    left = middle;
            }

            return gameStamps[left].score;
        }
    }
}
