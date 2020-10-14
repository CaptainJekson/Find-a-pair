using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.CardTable
{
    [RequireComponent(typeof(RectTransform), typeof(GridLayoutGroup), typeof(LevelCreator))]
    public class UIFlexibleGridController : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private GridLayoutGroup _gridLayoutGroup;
        private LevelCreator _levelCreator;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            _levelCreator = GetComponent<LevelCreator>();
        }

        private void OnEnable()
        {
            _levelCreator.OnLevelCreated += UpdateCellSizes;
        }

        private void OnDisable()
        {
            _levelCreator.OnLevelCreated -= UpdateCellSizes;
        }

        private void UpdateCellSizes()
        {
            float width = (_rectTransform.rect.width / _gridLayoutGroup.constraintCount) * _levelCreator.ReductionRatio;
            float height = (_rectTransform.rect.height / _gridLayoutGroup.constraintCount) * _levelCreator.ReductionRatio;

            _gridLayoutGroup.cellSize = new Vector2(width, height);
        }
    }
}

