using UnityEngine;
using System.Collections.Generic;
using App.Common._Data;

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
            _isDictionaryOpen = _PlayerStatistics.isDictionaryOpen;
            for (int i = 0; i < _dictionary.Length; i++)
            {
                _dictionary[i].SetActive(_isDictionaryOpen[i]);
            }

        }

        public void Onclick(string _direction)
        {
            if (_direction == "right") _showNum += 1;
            if (_direction == "left") _showNum -= 1;
            
            _showNum = Mathf.Clamp(_showNum, 0, _dictionarylength - 1);

            _leftArrow.SetActive(_showNum > 0);
            _rightArrow.SetActive(_showNum < _dictionarylength - 1);
        }

        public void showBook(int _bookNum)
        {
            _showNum = _bookNum;
            Onclick(null);
        }
    }
}
