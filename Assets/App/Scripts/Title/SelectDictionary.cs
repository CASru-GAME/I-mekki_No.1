using UnityEngine;
using App.Common._Data;
using App.Common.SE;

namespace App.Scripts.Title
{
    public class SelectDictionary : MonoBehaviour
    {
        [SerializeField] private GameObject _rightArrow;
        [SerializeField] private GameObject _leftArrow;
        [SerializeField] private int _showNum=1;
        private bool[] _isDictionaryOpen => _PlayerStatistics.isDictionaryOpen;
        [SerializeField] private GameObject[] _dictionary = new GameObject[21];
        private int _dictionarylength = _PlayerStatistics.DictionaryNumMax;

        [SerializeField] private DictionaryDatabase _database;
        [SerializeField] private TMPro.TMP_Text _titleText;
        [SerializeField] private TMPro.TMP_Text _descriptionText;
        [SerializeField] private UnityEngine.UI.Image _illustrationImage;

        [SerializeField] private bool _isTest = false;

        private Vector2 _dragStartPos;
        private float _swipeThreshold = 100f; // スワイプ判定距離(px)
        private bool isDragging = false;
        [SerializeField] private SEPlayer _sePlayer;

        void Update()
        {
            if (Input.touchCount > 0) //タッチの判定
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    StartDrag(touch.position);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    DragEnd(touch.position);
                }
            }
            else //マウスの判定
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartDrag(Input.mousePosition);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    DragEnd(Input.mousePosition);
                }
            }
        }

        void StartDrag(Vector2 pos)
        {
            isDragging = true;
            _dragStartPos = pos;
        }

        void DragEnd(Vector2 endPos)
        {
            if (!isDragging) return;

            isDragging = false;

            float diffX = endPos.x - _dragStartPos.x;

            if (Mathf.Abs(diffX) < _swipeThreshold) return;

            if (diffX < 0)
            {
                Onclick("right");
                _sePlayer.PlaySE();
            }
            else
            {
                Onclick("left");
                _sePlayer.PlaySE();
            }
        }

        public void checkDictionary()
        {
            for (int i = 0; i < _dictionary.Length; i++)
            {
                _dictionary[i].SetActive(_isTest || _isDictionaryOpen[i]);
            }
        }

        public void Onclick(string _direction)
        {
            //図鑑をめくる
            if (_direction == "right") _showNum += 1;
            if (_direction == "left") _showNum -= 1;
            
            _showNum = Mathf.Clamp(_showNum, 0, _dictionarylength - 1);

            _leftArrow.SetActive(_showNum > 0);
            _rightArrow.SetActive(_showNum < _dictionarylength - 1);
            ShowDictionaryContent();
        }

        private void ShowDictionaryContent()
        {
            // 開放されていない場合
            if (!_isTest && !_isDictionaryOpen[_showNum])
            {
                _titleText.text = "???";
                _descriptionText.text = "No data.";
                _illustrationImage.gameObject.SetActive(false);
                return;
            }

            //開放されている場合はScriptableObjectから読み込み
            var item = _database.Items[_showNum];
            _titleText.text = item.Title;
            _descriptionText.text = item.Description;
            _illustrationImage.sprite = item.Illustration;
            _illustrationImage.gameObject.SetActive(true);

        }

        public void showBook(int _bookNum)
        {
            //クリックした図鑑の説明を表示
            _showNum = _bookNum;
            Onclick(null);
            ShowDictionaryContent();
        }
    }
}