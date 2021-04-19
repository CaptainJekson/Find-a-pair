using System;
using System.Collections;
using CJ.FindAPair.Animation;
using CJ.FindAPair.CardTable;
using CJ.FindAPair.Configuration;
using CJ.FindAPair.Game;
using Doozy.Engine.UI;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.CoreGames
{
    public class GameWatcher : MonoBehaviour
    {
       
        //[SerializeField] private UIValue _lifeText;
        //[SerializeField] private UIValue _timeText;
        //[SerializeField] private UIValue _scoreText;
        
        private GameSettingsConfig _gameSettingsConfig;
        private LevelCreator _levelCreator;
        private CardComparator _cardComparator;

        private int _life;
        private int _time;
        private int _score;
        private int _comboCounter;
        private int _accruedScore;

        private int _quantityOfPairs;
        private int _quantityOfMatchedPairs;

        private IEnumerator _timerCoroutine;

        [Inject]
        public void Construct(LevelCreator levelCreator, CardComparator cardComparator,
            GameSettingsConfig gameSettingsConfig)
        {
            _levelCreator = levelCreator;
            _cardComparator = cardComparator;
            _gameSettingsConfig = gameSettingsConfig;
        }

        private void Awake()
        {
            //GameSaver.Init();
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
            //_scoreText.SetValue(_score.ToString());
        }

        private void AddScore()
        {
            _accruedScore = AccrueScore();
            _quantityOfMatchedPairs++;
            
            AddComboScore();
            
            //_scoreText.SetValue(_score.ToString());
            
            _comboCounter++;

            if (_quantityOfMatchedPairs >= _quantityOfPairs)
            {
                InitiateVictory();
            }
        }

        private void AddComboScore()
        {
            if (_comboCounter < 1) 
                return;
            
            var scoreCombo = _gameSettingsConfig.ScoreCombo.Count > _comboCounter ? 
                _gameSettingsConfig.ScoreCombo[_comboCounter - 1] : 
                _gameSettingsConfig.ScoreCombo[_gameSettingsConfig.ScoreCombo.Count - 1];

            _score += scoreCombo;

            foreach (var card in _cardComparator.ComparisonCards)
            {
                card.GetComponent<AnimationCard>().PlayCombo(scoreCombo);
            }
        }

        private void RemoveScore()
        {
            if (_score > 0)
            {
                _quantityOfMatchedPairs--;
                _score -= _accruedScore;
            }
            
            //_scoreText.SetValue(_score.ToString());
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
            _comboCounter = 0;
            
            if(_life > 0)
                _life--;
            
            //_lifeText.SetValue(_life.ToString());

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

            GameSaver.SaveResources(PlayerResourcesType.Gold, _score);
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

            //_lifeText.SetValue(_life.ToString());
            //_timeText.SetValue(TimeConverer(_time));
            //_scoreText.SetValue(_score.ToString());

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
            //_timeText.SetValue(TimeConverer(0));
        }

        private void ResetCounts()
        {
            _quantityOfMatchedPairs = 0;
            _quantityOfPairs = 0;
            _life = 0;
            _time = 0;
            _score = 0;
            _comboCounter = 0;
        }

        private IEnumerator TimerTick()
        {
            yield return new WaitForSeconds(_gameSettingsConfig.StartTimeShow);

            while (true)
            {
                _time--;
                //_timeText.SetValue(TimeConverer(_time));

                yield return new WaitForSeconds(1.0f);

                if (_time <= 1)
                {
                    InitiateDefeat();
                }
            }
        }
    }
}

