using System.Collections.Generic;
using UnityEngine;

namespace CJ.FindAPair.Configuration
{
    [CreateAssetMenu(fileName = "Level", menuName = "Levels")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private int _level;

        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private List<bool> _levelField = new List<bool>();

        public List<bool> LevelField => _levelField;
        public int Width => _width;
        public int Height => _height;

        public void SetLevel(bool[,] _levelMatrix, int level)
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
    }
}

