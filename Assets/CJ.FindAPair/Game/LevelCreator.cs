using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;

    private void Awake()
    {
        Debug.Log("Level number: " + _levelConfig.LevelNumber);

        Debug.Log(_levelConfig.FieldArray == null);

        //for (int i = 0; i < _levelConfig.FieldArray.GetLength(0); i++)
        //{
        //    for (int j = 0; j < _levelConfig.FieldArray.GetLength(1); j++)
        //    {
        //        Debug.Log(_levelConfig.FieldArray[i, j]);
        //    }

        //    Debug.Log("\n");
        //}
    }
}
