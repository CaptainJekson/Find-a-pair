using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Levels")]
public class LevelConfig : ScriptableObject
{
    private bool[,] _fieldsArray;
    private int _levelNumber;

    public bool[,] FieldArray => _fieldsArray;
    public int LevelNumber => _levelNumber;

    public void SetLevel(bool[,] fieldsArray, int levelNumber)
    {
        _fieldsArray = fieldsArray;
        _levelNumber = levelNumber;
    }

}
