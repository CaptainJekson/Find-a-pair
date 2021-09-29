using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

#endif

namespace CJ.FindAPair.Modules.CoreGames.Configs
{
    [CreateAssetMenu(fileName = "Level", menuName = "Find a pair/Level")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private int _level;
        [SerializeField] private bool _isHard;
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private float _heightOffset;

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
        public bool IsHard => _isHard;
        public int Width => _width;
        public int Height => _height;
        public float HeightOffset => _heightOffset;
        public QuantityOfCardOfPair QuantityOfCardOfPair => _quantityOfCardOfPair;
        public int Tries => _tries;
        public int Time => _time;
        public int QuantityPairOfFortune => _quantityPairOfFortune;
        public int QuantityPairOfEntanglement => _quantityPairOfEntanglement;
        public int QuantityPairOfReset => _quantityPairOfReset;
        public int QuantityPairOfBombs => _quantityPairOfBombs;

        public int QuantityPairOfSpecialCard => _quantityPairOfFortune + _quantityPairOfEntanglement +
                                                _quantityPairOfReset + _quantityPairOfBombs;

        public void SetSizeLevel(bool[,] levelMatrix, int level, float heightOffset)
        {
            _width = levelMatrix.GetLength(0);
            _height = levelMatrix.GetLength(1);
            _heightOffset = heightOffset;

            for (var i = 0; i < _width; i++)
            {
                for (var j = 0; j < _height; j++)
                {
                    _levelField.Add(levelMatrix[i, j]);
                }
            }

            _level = level;
        }
        
        public void SetConditionsLevel(QuantityOfCardOfPair quantityOfCardOfPair, bool isHard, int tries, int time, 
            int quantityPairOfFortune, int quantityPairOfEntanglement, int quantityPairOfReset,
            int quantityPairOfBombs)
        {
            _quantityOfCardOfPair = quantityOfCardOfPair;
            _isHard = isHard;
            _tries = tries;
            _time = time;
            _quantityPairOfFortune = quantityPairOfFortune;
            _quantityPairOfEntanglement = quantityPairOfEntanglement;
            _quantityPairOfReset = quantityPairOfReset;
            _quantityPairOfBombs = quantityPairOfBombs;
        }
    }
}

