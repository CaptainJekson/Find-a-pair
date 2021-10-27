using System;
using System.Collections.Generic;
using DG.Tweening;
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
        
        [SerializeField] private float _cardDealSpeed;
        [SerializeField] private float _timeBetweenDeals;
        [SerializeField] private Ease _cardDealEase;
        [SerializeField] private Vector2 _cardsDeckPosition;
        
        public List<PlaceWidthSetting> PlaceWidthSettings => _placeWidthSettings;
        public List<PlaceHeightSetting> PlaceHeightSetting => _placeHeightSetting;
        public float OffsetFactorX => _offsetFactorX;
        public float OffsetFactorY => _offsetFactorY;
        public float CardDealSpeed => _cardDealSpeed;
        public float TimeBetweenDeals => _timeBetweenDeals;
        public Ease CardDealEase => _cardDealEase;
        public Vector2 CardsDeckPosition => _cardsDeckPosition;

        public float GetScale(int width, int height)
        {
            foreach (var widthSetting in _placeWidthSettings)
            {
                if (width <= widthSetting.AllowableWidth)
                {
                    return widthSetting.Scale;
                }
            }
            
            return _placeWidthSettings[_placeWidthSettings.Count - 1].Scale;
        }

        public Vector2 GetStartPosition(int width, int height, float heightOffset)
        {
            var startPositionX = 0.0f;
            var startPositionY = 0.0f; 
            
            foreach (var widthSetting in _placeWidthSettings)
            {
                if (width <= widthSetting.AllowableWidth)
                {
                    startPositionX = widthSetting.StartPositionX;
                    break;
                }
            }

            foreach (var heightSetting in _placeHeightSetting)
            {
                if (height <= heightSetting.AllowableHeight)
                {
                    startPositionY = heightSetting.StartPositionY;
                    break;
                }
            }
            
            return new Vector2(startPositionX, startPositionY + heightOffset);
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