namespace App.Common._Data
{
    public static class _GameStatus
    {
        enum difficulty
        {
            Easy,
            Normal,
            Hard
        }

        enum stage
        {
            Stage1,
            Stage2,
            Stage3
        }
        static difficulty _difficulty;
        static stage _stage;
    }
}
