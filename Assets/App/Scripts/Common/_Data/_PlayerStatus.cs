namespace App.Common._Data
{
    static class _PlayerStatus
    {
        static private int _hp;
        static public void SetHp(int hp)
        {
            _hp = hp;
        }
        static public int GetHp()
        {
            return _hp;
        }
        static public void ClearAll()
        {
            _hp = 0;
        }
        static public void AddHp()
        {
            if(_hp == null)
            {
                return;
            }
            _hp ++;
        }        
        static public void SubHp()
        {
            if(_hp == null)
            {
                return;
            }
            _hp --;
        }
    }
}
