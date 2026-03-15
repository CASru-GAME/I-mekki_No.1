using UnityEngine;
using DG.Tweening;

namespace App.Result
{
    public class ShowResult : MonoBehaviour
    {
        [SerializeField] private GameObject _resultTitle;
        [SerializeField] private GameObject _resultView;
        [SerializeField] private GameObject _goTitleButton;
        
        // 画面比率で移動距離を指定（0～1の範囲）
        [SerializeField] private float _resultTitleOffsetX = 0.2f; // 画面幅の20%
        [SerializeField] private float _resultViewOffsetX = 0.1f;

        void Start()
        {
            _goTitleButton.transform.localScale = Vector3.zero;

            // 画面比率から実際のピクセル距離を計算
            Vector3 resultTitleOffset = new Vector3(
                Screen.width * _resultTitleOffsetX,
                0,
                0
            );

            Vector3 resultViewOffset = new Vector3(
                Screen.width * _resultViewOffsetX,
                0,
                0
            );

            // リザルトの表示
            _resultTitle.transform.DOMove(_resultTitle.transform.position + resultTitleOffset, 0.5f)
                .OnComplete(() =>
                {
                    _resultView.transform.DOMove(_resultView.transform.position + resultViewOffset, 0.5f)
                        .OnComplete(() =>
                        {
                            // リザルトの表示が終わったら、ボタンを表示
                            _goTitleButton.transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutBounce);
                        });
                });
        }

        public void OnclickGoTitle()
        {
            // タイトル場面にもどる
        }
    }
}
