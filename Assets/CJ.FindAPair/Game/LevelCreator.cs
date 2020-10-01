using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private Transform _card;
    
    private Vector2 _testStartPosition;    //TODO Test

    private void Awake()
    {
        CreateLevel(_card, _levelConfig.LevelField, _levelConfig.Width, _levelConfig.Height);
        
        _testStartPosition = Vector2.zero;
    }

    private void CreateLevel(Transform card, List<bool> levelField, int width, int height)
    {
        var indexLevelField = 0;

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                if (levelField[indexLevelField] == true)
                    Instantiate(_card, _testStartPosition, Quaternion.identity);
                
                _testStartPosition.y += 0.5f; //TODO Test

                indexLevelField++;
            }

            _testStartPosition.y = 0.0f;
            _testStartPosition.x += 0.5f; //TODO Test
        }
    }
}
