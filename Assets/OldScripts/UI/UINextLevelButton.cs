using System.Linq;
using CJ.FindAPair.CoreGames;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CJ.FindAPair.UI
{
    [RequireComponent(typeof(Button))]
    class UINextLevelButton : MonoBehaviour
    {
        [SerializeField] private LevelCreator _levelCreator;
        [SerializeField] private LevelConfigCollection _levelConfigCollection;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ButtonClickHandler);
        }

        private void ButtonClickHandler()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(1.0f);
            sequence.AppendCallback(CreateNextLevel);
        }

        private void CreateNextLevel()
        {
            LevelConfig nextLevel;
            var currentLevelNumber = _levelCreator.LevelConfig.LevelNumber;

            ++currentLevelNumber;

            try
            {
                nextLevel = _levelConfigCollection.Levels.First(item => item.LevelNumber == currentLevelNumber);
            }
            catch
            {
                nextLevel = _levelConfigCollection.Levels.First(item => item.LevelNumber == 1);
            }

            _levelCreator.ClearLevel();
            _levelCreator.CreateLevel(nextLevel);
        }
    }
}
