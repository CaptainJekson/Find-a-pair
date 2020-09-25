using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Levels")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private int _levelNumber;

    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private List<bool> _item = new List<bool>();


    public void SetLevel(bool[,] fieldsArray, int levelNumber)
    {
        _width = fieldsArray.GetLength(0);
        _height = fieldsArray.GetLength(1);

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                _item.Add(fieldsArray[i, j]);
            }
        }

        _levelNumber = levelNumber;
    }

}
