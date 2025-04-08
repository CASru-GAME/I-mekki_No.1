using UnityEngine;
using DG.Tweening;

namespace App.Scripts.Game.UI
{
    public class GameWindow : MonoBehaviour
    {
        //メニューボタンに付ける
        [SerializeField] private GameObject _window;
        private RectTransform _windowTransfom;

        void Start()
        {
            _windowTransfom = _window.GetComponent<RectTransform>();
        }

        public void OnClickMenu()
        {
            _windowTransfom.localScale = Vector3.zero; // 初期サイズを0に設定
            _window.SetActive(true); // ウインドウをアクティブに
            _windowTransfom.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack); // サイズを拡大してアニメーション
        }

        public void OnClickDelete()
        {
            _windowTransfom.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
            {
                _window.SetActive(false); // アニメーション完了後に非アクティブ化
            });
        }

        public void OnclickRetire()
        {
            //タイトル画面に遷移
        }
        
    }
}
