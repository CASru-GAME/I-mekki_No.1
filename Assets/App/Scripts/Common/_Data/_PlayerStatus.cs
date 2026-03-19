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
            _hp = 3;
        }
        static public void AddHp()
        {
            _hp++;
        }
        static public bool SubHp(bool isdead = false)
        {
            if(!isdead){
                _hp--;
                if(_hp <= 0)
                {
                    _hp = 0;
                    SceneLoader.Instance.LoadResultScene();
                    return true;
                }
            }
            return false;
        }
    }
}
