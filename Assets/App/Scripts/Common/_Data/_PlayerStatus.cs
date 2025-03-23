namespace App.Common._Data
{
    static class _PlayerStatus
    {
        static private int _hp;
        static void SetHp(int hp)
        {
            _hp = hp;
        }
        static int GetHp()
        {
            return _hp;
        }
        static public void ClearAll()
        {
            _hp = 0;
        }
    }
}
