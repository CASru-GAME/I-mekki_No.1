namespace App.Common._Data
{
    static class _PlayerStatus
    {
        private const int MaxHp = 3;
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
            RecoverAllHp();
        }
        static public void RecoverAllHp()
        {
            _hp = MaxHp;
        }
        static public void AddHp()
        {
            if (_hp < MaxHp)
            {
                _hp++;
            }
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
