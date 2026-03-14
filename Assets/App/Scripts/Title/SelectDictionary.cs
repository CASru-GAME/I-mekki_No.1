using UnityEngine;
using App.Common._Data;

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


        [SerializeField] private bool _testBool = false;

        public void checkDictionary()
        {
            for (int i = 0; i < _dictionary.Length; i++)
            {
                _dictionary[i].SetActive(_testBool || _isDictionaryOpen[i]);
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
            if (!_testBool && !_isDictionaryOpen[_showNum])
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
