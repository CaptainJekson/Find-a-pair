using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR 
using CJ.FindAPair.CustomEditor;
#endif

namespace CJ.FindAPair.Configuration
{
    [CreateAssetMenu(fileName = "Level", menuName = "Find a pair/Level")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private int _level;
        [SerializeField] private int _width;
        [SerializeField] private int _height;

        [SerializeField] private QuantityOfCardOfPair _quantityOfCardOfPair;
        [SerializeField] private int _tries = 1;
        [SerializeField] private int _time = 1;
        [SerializeField] private int _quantityPairOfBombs = 0;

        [SerializeField] private List<bool> _levelField = new List<bool>();

        public List<bool> LevelField => _levelField;
        public int Width => _width;
        public int Height => _height;
        public QuantityOfCardOfPair QuantityOfCardOfPair => _quantityOfCardOfPair;
        public int Tries => _tries;
        public int Time => _time;
        public int QuantityPairOfBombs => _quantityPairOfBombs;

        public void SetSizeLevel(bool[,] _levelMatrix, int level)
        {
            _width = _levelMatrix.GetLength(0);
            _height = _levelMatrix.GetLength(1);

            for (var i = 0; i < _width; i++)
            {
                for (var j = 0; j < _height; j++)
                {
                    _levelField.Add(_levelMatrix[i, j]);
                }
            }

            _level = level;
        }
        
        public void SetConditionsLevel(QuantityOfCardOfPair quantityOfCardOfPair, int tries, int time, int quantityPairOfBombs)
        {
            _quantityOfCardOfPair = quantityOfCardOfPair;
            _tries = tries;
            _time = time;
            _quantityPairOfBombs = quantityPairOfBombs;
        }
    }
}

