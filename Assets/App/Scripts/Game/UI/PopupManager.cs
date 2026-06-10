using UnityEngine;

namespace App.Game.UI
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] private PopupText _popupPrefab;

        [SerializeField] private Transform _popupRoot;

        [SerializeField] private Vector3 _popupOffset = new Vector3(-10f, 1.5f, 0f);
        [SerializeField] private PopupDatabase _popupDatabase;
        public PopupDatabase PopupDatabase => _popupDatabase;

        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;

            // popupRoot未設定時は自分を使用
            if (_popupRoot == null)
            {
                _popupRoot = this.transform;
            }
        }

        private void Start()
        {
            //ポッポアップのデモ、トリガーは他スクリプトの予定
            //PopupItem _item = _popupDatabase.Items[21];
            //ShowPopup(Vector3.zero, _item.PopupText, _item.TextColor);
        }

        // ポップアップ表示
        public void ShowPopup(Vector3 _worldPos, string _message, Color _color)
        {
            // Popup生成
            PopupText _popup = Instantiate(_popupPrefab, _popupRoot);

            // 表示位置補正
            Vector3 _popupWorldPos = _worldPos + _popupOffset;

            // ワールド座標 → スクリーン座標
            Vector3 _screenPos =_mainCamera.WorldToScreenPoint(_popupWorldPos);

            // UI座標設定
            RectTransform _rect = _popup.GetComponent<RectTransform>();
            _rect.position = _screenPos;

            // 表示
            _popup.Show(_message, _color);
        }
    }
}