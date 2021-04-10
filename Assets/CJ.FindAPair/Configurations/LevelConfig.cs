using System;
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
        [SerializeField][Range(0.0f, 1.0f)] private float _scale;

        [SerializeField] private QuantityOfCardOfPair _quantityOfCardOfPair;
        [SerializeField] private int _tries = 1;
        [SerializeField] private int _time = 1;
        [SerializeField] private int _quantityPairOfFortune = 0;
        [SerializeField] private int _quantityPairOfEntanglement = 0;
        [SerializeField] private int _quantityPairOfReset = 0;
        [SerializeField] private int _quantityPairOfBombs = 0;

        [SerializeField] private List<bool> _levelField = new List<bool>();

        public List<bool> LevelField => _levelField;
        public int LevelNumber => _level;
        public int Width => _width;
        public int Height => _height;
        public float Scale => _scale;
        public QuantityOfCardOfPair QuantityOfCardOfPair => _quantityOfCardOfPair;
        public int Tries => _tries;
        public int Time => _time;
        public int QuantityPairOfFortune => _quantityPairOfFortune;
        public int QuantityPairOfEntanglement => _quantityPairOfEntanglement;
        public int QuantityPairOfReset => _quantityPairOfReset;
        public int QuantityPairOfBombs => _quantityPairOfBombs;

        public int QuantityPairOfSpecialCard => _quantityPairOfFortune + _quantityPairOfEntanglement +
                                                _quantityPairOfReset + _quantityPairOfBombs;

        public void SetSizeLevel(bool[,] levelMatrix, int level, float scale)
        {
            _width = levelMatrix.GetLength(0);
            _height = levelMatrix.GetLength(1);
            _scale = scale;

            for (var i = 0; i < _width; i++)
            {
                for (var j = 0; j < _height; j++)
                {
                    _levelField.Add(levelMatrix[i, j]);
                }
            }

            _level = level;
        }
        
        public void SetConditionsLevel(QuantityOfCardOfPair quantityOfCardOfPair, int tries, int time, 
            int quantityPairOfFortune, int quantityPairOfEntanglement, int quantityPairOfReset,
            int quantityPairOfBombs)
        {
            _quantityOfCardOfPair = quantityOfCardOfPair;
            _tries = tries;
            _time = time;
            _quantityPairOfFortune = quantityPairOfFortune;
            _quantityPairOfEntanglement = quantityPairOfEntanglement;
            _quantityPairOfReset = quantityPairOfReset;
            _quantityPairOfBombs = quantityPairOfBombs;
        }
    }
}

