using System;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.CoreGames
{
    [RequireComponent(typeof(Canvas))]
    public class CameraInCanvas : MonoBehaviour
    {
        private GameCamera _gameCamera;
        private Canvas _canvas;
        
        [Inject]
        public void Construct(GameCamera gameCamera)
        {
            _gameCamera = gameCamera;
        }

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.worldCamera = _gameCamera.GetComponent<Camera>();
        }
    }
}