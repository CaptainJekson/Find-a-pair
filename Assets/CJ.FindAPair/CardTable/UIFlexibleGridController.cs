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

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            _levelCreator = GetComponent<LevelCreator>();

            UpdateCellSizes();
        }

        private void Update()
        {
            UpdateCellSizes();
        }

        private void UpdateCellSizes()
        {
            var width = (_rectTransform.rect.width / _gridLayoutGroup.constraintCount) * _levelCreator.ReductionRatio;
            var height = (_rectTransform.rect.height / _gridLayoutGroup.constraintCount) * _levelCreator.ReductionRatio;

            _gridLayoutGroup.cellSize = new Vector2(width, height);
        }
    }
}

