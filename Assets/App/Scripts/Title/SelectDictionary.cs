using UnityEngine;
using System.Collections.Generic;
using App.Common._Data;
using TMPro;

namespace App.Scripts.Title
{
    public class SelectDictionary : MonoBehaviour
    {
        [SerializeField] private GameObject _rightArrow;
        [SerializeField] private GameObject _leftArrow;
        [SerializeField] private int _showNum=1;
        [SerializeField] private bool[] _isDictionaryOpen;
        [SerializeField] private GameObject[] _dictionary = new GameObject[24];
        private int _dictionarylength = _PlayerStatistics.DictionaryNumMax;

        [SerializeField] private DictionaryDatabase _database;
        [SerializeField] private TMPro.TMP_Text _titleText;
        [SerializeField] private TMPro.TMP_Text _descriptionText;

        [SerializeField] private bool _testBool = false;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //テスト用
            if(_testBool)
            {
                for (int i = 0; i < _dictionary.Length; i++)
                {
                    _dictionary[i].SetActive(_isDictionaryOpen[i]);
                }
                _testBool = false;
            }
        }

        public void checkDictionary()
        {
            //図鑑メニュー移動時に図鑑の開放状況を確認して、図鑑オブジェクトの表示・非表示
            _isDictionaryOpen = _PlayerStatistics.isDictionaryOpen;
            for (int i = 0; i < _dictionary.Length; i++)
            {
                _dictionary[i].SetActive(_isDictionaryOpen[i]);
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
            if (!_isDictionaryOpen[_showNum])
            {
                _titleText.text = "???";
                _descriptionText.text = "No data.";
                return;
            }

            //開放されている場合はScriptableObjectから読み込み
            var item = _database.Items[_showNum];
            _titleText.text = item.Title;
            _descriptionText.text = item.Description;
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
