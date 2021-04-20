using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CJ.FindAPair.Modules.CoreGames
{
    [RequireComponent(typeof(RectTransform), typeof(GridLayoutGroup))]
    public class UIFlexibleGridController : MonoBehaviour
    {
        private LevelCreator _levelCreator;
        private RectTransform _rectTransform;
        private GridLayoutGroup _gridLayoutGroup;

        [Inject]
        private void Construct(LevelCreator levelCreator)
        {
            _levelCreator = levelCreator;
        }
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        }

        private void OnEnable()
        {
            if(_levelCreator != null)
                _levelCreator.OnLevelCreated += UpdateCellSizes;
        }

        private void OnDisable()
        {
            _levelCreator.OnLevelCreated -= UpdateCellSizes;
        }

        private void UpdateCellSizes()
        {
            var width = (_rectTransform.rect.width / _gridLayoutGroup.constraintCount) * _levelCreator.Scale;
            var height = (_rectTransform.rect.height / _gridLayoutGroup.constraintCount) * _levelCreator.Scale;

            _gridLayoutGroup.cellSize = new Vector2(width, height);
        }
    }
}

