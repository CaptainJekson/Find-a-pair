using System;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.CoreGames
{
    public class LevelBackground : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}