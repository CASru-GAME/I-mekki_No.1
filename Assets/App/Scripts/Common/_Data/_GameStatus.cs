namespace App.Common._Data
{
    public static class _GameStatus
    {
        public enum gameStatus
        {
            Title,
            SelectDifficulty,
            InGame,
            Result
        }
        public enum difficulty
        {
            Easy = 0,
            Normal = 1,
            Hard = 2
        }
        public enum stage
        {
            BeforeStage,
            Stage1,
            Stage2,
            Stage3
        }
        public static stage _stage = stage.BeforeStage;
        public static gameStatus _gameStatus = gameStatus.Title;
        public static difficulty _difficulty = difficulty.Easy;
    }
}
