using CJ.FindAPair.CardTable;
using CJ.FindAPair.Configuration;
using CJ.FindAPair.UI;
using Doozy.Engine.UI;
using System;
using System.Collections;
using UnityEngine;

namespace CJ.FindAPair.Game
{
    [RequireComponent(typeof(LevelCreator)), RequireComponent(typeof(CardComparator))]
    public class GameWatcher : MonoBehaviour
    {
        [SerializeField] private GameSettingsConfig _gameSettingsConfig;
        [SerializeField] private UIValue _lifeText;
        [SerializeField] private UIValue _timeText;
        [SerializeField] private UIValue _scoreText;

        private LevelCreator _levelCreator;
        private CardComparator _cardComparator;

        private int _life;
        private int _time;
        private int _score;

        private int _quantityOfPairs;
        private int _quantityOfMatchedPairs;

        private IEnumerator _timerCoroutine;

        private void Awake()
        {
            _levelCreator = GetComponent<LevelCreator>();
            _cardComparator = GetComponent<CardComparator>();
        }

        private void OnEnable()
        {
            _cardComparator.СardsMatched += AddPoint;
            _cardComparator.СardsNotMatched += RemoveLife;
            _cardComparator.OpenCardBomb += InitiateDefeat;
            _levelCreator.OnLevelCreated += InitTimer;
            _levelCreator.OnLevelDeleted += ResetTimer;
            _levelCreator.OnLevelDeleted += ResetCounts;
        }

        private void OnDisable()
        {
            _cardComparator.СardsMatched -= AddPoint;
            _cardComparator.СardsNotMatched -= RemoveLife;
            _cardComparator.OpenCardBomb -= InitiateDefeat;
            _levelCreator.OnLevelCreated -= InitTimer;
            _levelCreator.OnLevelDeleted -= ResetTimer;
            _levelCreator.OnLevelDeleted -= ResetCounts;
        }

        private void AddPoint()
        {
            _score++;
            _quantityOfMatchedPairs++;
            _scoreText.SetValue(_score.ToString());

            if (_quantityOfMatchedPairs >= _quantityOfPairs)
            {
                InitiateVictory();
            }
        }

        private void RemoveLife()
        {
            _life--;
            _lifeText.SetValue(_life.ToString());

            if (_life <= 0)
            {
                InitiateDefeat();
            }
        }

        private void InitiateVictory()
        {
            StopTimer();

            UIView.ShowView("General", "BlockPanel");
            UIView.ShowView("GameResult", "Victory");

            //TODO Add Reward
        }

        private void InitiateDefeat()
        {
            StopTimer();

            UIView.ShowView("General", "BlockPanel");
            UIView.ShowView("GameResult", "Defeat");
        }

        private string TimeConverer(int secondTime)  //TODO повторяется в UIPreviewLevel
        {
            var time = TimeSpan.FromSeconds(secondTime);

            return time.ToString(@"mm\:ss");
        }

        private void InitTimer()   //TODO Rename
        {
            _quantityOfPairs = (_levelCreator.Cards.Count / (int)_levelCreator.LevelConfig.QuantityOfCardOfPair)
                             - _levelCreator.LevelConfig.QuantityPairOfBombs;

            _life = _levelCreator.LevelConfig.Tries;
            _time = _levelCreator.LevelConfig.Time;
            _score = 0;

            _lifeText.SetValue(_life.ToString());
            _timeText.SetValue(TimeConverer(_time));
            _scoreText.SetValue(_score.ToString());

            StartTimer();
        }

        private void StartTimer()
        {
            _timerCoroutine = TimerTick();
            StartCoroutine(_timerCoroutine);
        }

        private void StopTimer()
        {
            StopCoroutine(_timerCoroutine);
        }

        private void ResetTimer()
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
            _timeText.SetValue(TimeConverer(0));
        }

        private void ResetCounts()
        {
            _quantityOfMatchedPairs = 0;
            _quantityOfPairs = 0;
            _life = 0;
            _time = 0;
            _score = 0;
        }

        private IEnumerator TimerTick()
        {
            yield return new WaitForSeconds(_gameSettingsConfig.StartTimeShow);

            while (true)
            {
                _time--;
                _timeText.SetValue(TimeConverer(_time));

                yield return new WaitForSeconds(1.0f);

                if (_time <= 1)
                {
                    InitiateDefeat();
                }
            }
        }
    }
}

