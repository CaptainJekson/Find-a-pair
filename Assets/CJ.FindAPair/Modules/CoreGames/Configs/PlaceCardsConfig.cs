using System;
using System.Collections.Generic;
using UnityEngine;

namespace CJ.FindAPair.Modules.CoreGames.Configs
{
    [CreateAssetMenu(fileName = "PlaceCardsConfig", menuName = "Find a pair/PlaceCardsConfig")]
    public class PlaceCardsConfig : ScriptableObject
    {
        [SerializeField] private float _offsetFactorX;
        [SerializeField] private float _offsetFactorY;
        [SerializeField] private List<PlaceWidthSetting> _placeWidthSettings;
        [SerializeField] private List<PlaceHeightSetting> _placeHeightSetting;

        public List<PlaceWidthSetting> PlaceWidthSettings => _placeWidthSettings;
        public List<PlaceHeightSetting> PlaceHeightSetting => _placeHeightSetting;
        public float OffsetFactorX => _offsetFactorX;
        public float OffsetFactorY => _offsetFactorY;

        public float GetScale(int width, int height)
        {
            foreach (var widthSetting in _placeWidthSettings)
            {
                if (width <= widthSetting.AllowableWidth)
                {
                    return widthSetting.Scale;
                }
            }
            
            return 1.0f;
        }

        public Vector2 GetStartPosition(int width, int height)
        {
            foreach (var widthSetting in _placeWidthSettings)
            {
                if (width <= widthSetting.AllowableWidth)
                {
                    return new Vector2(widthSetting.StartPositionX, 3.0f);
                }
            }
            
            return new Vector2(-1.5f, 3.0f);
        }
    }

    [Serializable]
    public class PlaceWidthSetting
    {
        public int AllowableWidth;
        public float StartPositionX;
        public float Scale;
    }
    
    [Serializable]
    public class PlaceHeightSetting
    {
        public int AllowableHeight;
        public float StartPositionY;
    }
}