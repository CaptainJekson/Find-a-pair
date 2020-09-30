using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private Transform _card;
    [SerializeField] private Vector2 _testStartPosition;    //TODO Test

    private void Awake()
    {
        CreateLevel(_card, _levelConfig.LevelField, _levelConfig.Width, _levelConfig.Height);
    }

    private void CreateLevel(Transform card, List<bool> levelField, int width, int height)
    {
        int indexLevelField = 0;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (levelField[indexLevelField] == true)
                    Instantiate(_card, _testStartPosition, Quaternion.identity);
                
                _testStartPosition.x += 0.2f; //TODO Test

                indexLevelField++;
            }

            _testStartPosition.y += 0.2f; //TODO Test
        }
    }
}
