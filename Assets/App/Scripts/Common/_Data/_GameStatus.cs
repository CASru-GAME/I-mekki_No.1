namespace App.Common._Data
{
    public static class _GameStatus
    {
        static private enum difficulty
        {
            Easy,
            Normal,
            Hard
        }

        static private enum stage
        {
            Stage1,
            Stage2,
            Stage3
        }
        static private difficulty _difficulty;
        static private stage _stage;
    }
}