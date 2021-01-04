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
        [SerializeField] private GameSaver _gameSaver;
        [SerializeField] private UIValue _lifeText;
        [SerializeField] private UIValue _timeText;
        [SerializeField] private UIValue _scoreText;

        private LevelCreator _levelCreator;
        private CardComparator _cardComparator;

        private int _life;
        private int _time;
        private int _score;
        private int _accruedScore;

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
            _cardComparator.CardsMatched += AddScore;
            _cardComparator.CardsNotMatched += RemoveLife;
            _levelCreator.OnLevelCreated += InitTimer;
            _levelCreator.OnLevelDeleted += ResetTimer;
            _levelCreator.OnLevelDeleted += ResetCounts;
        }

        private void OnDisable()
        {
            _cardComparator.CardsMatched -= AddScore;
            _cardComparator.CardsNotMatched -= RemoveLife;
            _levelCreator.OnLevelCreated -= InitTimer;
            _levelCreator.OnLevelDeleted -= ResetTimer;
            _levelCreator.OnLevelDeleted -= ResetCounts;
        }
        
        public void InitiateDefeat()
        {
            StopTimer();

            UIView.ShowView("General", "BlockPanel");
            UIView.ShowView("GameResult", "Defeat");
        }

        public void RemoveQuantityOfMatchedPairs()
        {
            RemoveLife();
            RemoveScore();
        }

        public void ResetScore()
        {
            _score = 0;
            _scoreText.SetValue(_score.ToString());
        }

        private void AddScore()
        {
            _accruedScore = AccrueScore();
            _quantityOfMatchedPairs++;
            _scoreText.SetValue(_score.ToString());

            if (_quantityOfMatchedPairs >= _quantityOfPairs)
            {
                InitiateVictory();
            }
        }

        private void RemoveScore()
        {
            if (_score > 0)
            {
                _quantityOfMatchedPairs--;
                _score -= _accruedScore;
            }
            
            _scoreText.SetValue(_score.ToString());
        }
        
        private int AccrueScore()
        {
            switch (_levelCreator.LevelConfig.QuantityOfCardOfPair)
            {
                case QuantityOfCardOfPair.TwoCards:
                    _score += _gameSettingsConfig.PointsTwoCards;
                    return _gameSettingsConfig.PointsTwoCards;
                case QuantityOfCardOfPair.ThreeCards:
                    _score += _gameSettingsConfig.PointsThreeCards;
                    return _gameSettingsConfig.PointsThreeCards;
                case QuantityOfCardOfPair.FourCards:
                    _score += _gameSettingsConfig.PointsFourCards;
                    return _gameSettingsConfig.PointsFourCards;
            }

            return 0;
        }
        
        private void RemoveLife()
        {
            if(_life > 0)
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

            _gameSaver.SaveInt(SaveTypeInt.Score, _score);
        }

        private string TimeConverer(int secondTime)  //TODO повторяется в UIPreviewLevel
        {
            var time = TimeSpan.FromSeconds(secondTime);

            return time.ToString(@"mm\:ss");
        }

        private void InitTimer()   //TODO Rename
        {
            _quantityOfPairs = (_levelCreator.Cards.Count / (int)_levelCreator.LevelConfig.QuantityOfCardOfPair)
                             - _levelCreator.LevelConfig.QuantityPairOfSpecialCard;

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

