using PyShopTestTask;

namespace GameTask.Tests
{
    public class Game_GetScore
    {
        private GameStamp[] gameStampsOneItem = new GameStamp[]
        {
            new GameStamp(0,3,4)
        };

        private GameStamp[] gameStampsTwoItem = new GameStamp[]
        {
            new GameStamp(0,0,0),
            new GameStamp(3,1,0)
        };

        private GameStamp[] gameStampsThreeItem = new GameStamp[]
        {
            new GameStamp(0,0,0),
            new GameStamp(3,1,0),
            new GameStamp(6,1,1)
        };

        private GameStamp[] gameStamps = new GameStamp[]
        {
            new GameStamp(0,0,0),
            new GameStamp(3,1,0),
            new GameStamp(6,1,1),
            new GameStamp(7,1,2),
            new GameStamp(9,1,3),
            new GameStamp(10,1,3),
            new GameStamp(13,2,3),
            new GameStamp(16,2,4),
            new GameStamp(17,3,4),
        };

        [Fact]
        public void GetScore_ComplexInput_ReturnCorrespondResult()
        {
            var game = new Game(gameStamps);
            Assert.Equal(game.getScore(-2), new Score(0, 0));
            Assert.Equal(game.getScore(-1), new Score(0, 0));
            Assert.Equal(game.getScore(0), new Score(0, 0));
            Assert.Equal(game.getScore(1), new Score(0, 0));
            Assert.Equal(game.getScore(2), new Score(0, 0));
            Assert.Equal(game.getScore(3), new Score(1, 0));
            Assert.Equal(game.getScore(4), new Score(1, 0));
            Assert.Equal(game.getScore(5), new Score(1, 0));
            Assert.Equal(game.getScore(6), new Score(1, 1));
            Assert.Equal(game.getScore(7), new Score(1, 2));
            Assert.Equal(game.getScore(8), new Score(1, 2));
            Assert.Equal(game.getScore(9), new Score(1, 3));
            Assert.Equal(game.getScore(10), new Score(1, 3));
            Assert.Equal(game.getScore(11), new Score(1, 3));
            Assert.Equal(game.getScore(12), new Score(1, 3));
            Assert.Equal(game.getScore(13), new Score(2, 3));
            Assert.Equal(game.getScore(14), new Score(2, 3));
            Assert.Equal(game.getScore(15), new Score(2, 3));
            Assert.Equal(game.getScore(16), new Score(2, 4));
            Assert.Equal(game.getScore(17), new Score(3, 4));
            Assert.Equal(game.getScore(18), new Score(3, 4));
            Assert.Equal(game.getScore(19), new Score(3, 4));
        }

        [Fact]
        public void GetScore_NegativeInput_ReturnScoreCorrespomdMinOffset()
        {
            var game3 = new Game(gameStampsThreeItem);
            var game2 = new Game(gameStampsTwoItem);
            var game1 = new Game(gameStampsOneItem);
            Assert.Equal(game3.getScore(-1), new Score(0,0));
            Assert.Equal(game2.getScore(-1), new Score(0,0));
            Assert.Equal(game1.getScore(-1), new Score(3,4));
        }

        [Fact]
        public void GetScore_InputMoreThanMaxOffset_ReturnScoreCorrespomdMaxOffset()
        {
            var game1 = new Game(gameStampsOneItem);
            var game2 = new Game(gameStampsTwoItem);
            var game3 = new Game(gameStampsThreeItem);
            Assert.Equal(game1.getScore(10), new Score(3, 4));
            Assert.Equal(game2.getScore(10), new Score(1, 0));
            Assert.Equal(game3.getScore(10), new Score(1, 1));
        }

        [Fact]
        public void GetScore3Item_InputWithNoCorrespondOffset_ReturnNearestPreviousScore()
        {
            var game = new Game(gameStampsThreeItem);
            Assert.Equal(game.getScore(4), new Score(1, 0));
            Assert.Equal(game.getScore(1), new Score(0, 0));
        }

        [Fact]
        public void GetScore2Item_InputWithNoCorrespondOffset_ReturnNearestPreviousScore()
        {
            var game = new Game(gameStampsTwoItem);
            Assert.Equal(game.getScore(1), new Score(0, 0));
        }

        [Fact]
        public void GetScore3Item_InputWithCorrespondOffset_ReturnCorrespondScore()
        {
            var game = new Game(gameStampsThreeItem);
            Assert.Equal(game.getScore(3), new Score(1, 0));
            Assert.Equal(game.getScore(6), new Score(1, 1));
        }

        [Fact]
        public void GetScore2Item_InputWithCorrespondOffset_ReturnCorrespondScore()
        {
            var game = new Game(gameStampsTwoItem);
            Assert.Equal(game.getScore(0), new Score(0, 0));
            Assert.Equal(game.getScore(3), new Score(1, 0));
        }

        [Fact]
        public void GetScore1Item_InputWithCorrespondOffset_ReturnCorrespondScore()
        {
            var game = new Game(gameStampsOneItem);
            Assert.Equal(game.getScore(0), new Score(3, 4));
        }

        [Fact]
        public void GetScore_GameStampsEmpty_ReturnZeroScore()
        {
            var game = new Game(new GameStamp[] { });
            Assert.Equal(game.getScore(10), new Score(0, 0));
        }
    }
}