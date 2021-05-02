using System;
using System.Collections;
using CI.QuickSave;
using CJ.FindAPair.Animation;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.Service.Save;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace CJ.FindAPair.Modules.CoreGames
{
    public class GameWatcher : MonoBehaviour
    {
        private GameSettingsConfig _gameSettingsConfig;
        private LevelCreator _levelCreator;
        private CardComparator _cardComparator;
        private GameSaver _gameSaver;

        private int _life;
        private int _time;
        private int _score;
        private int _comboCounter;
        private int _accruedScore;

        private int _quantityOfPairs;
        private int _quantityOfMatchedPairs;

        private IEnumerator _timerCoroutine;

        public event UnityAction<int> ScoreСhanged;
        public event UnityAction<int> LifeСhanged;
        public event UnityAction<int> TimeСhanged;
        public event UnityAction ThereWasAVictory;
        public event UnityAction ThereWasADefeat;
        

        [Inject]
        public void Construct(LevelCreator levelCreator, CardComparator cardComparator,
            GameSettingsConfig gameSettingsConfig, GameSaver gameSaver)
        {
            _levelCreator = levelCreator;
            _cardComparator = cardComparator;
            _gameSettingsConfig = gameSettingsConfig;
            _gameSaver = gameSaver;
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
            ThereWasADefeat?.Invoke();
        }

        public void RemoveQuantityOfMatchedPairs()
        {
            RemoveLife();
            RemoveScore();
        }

        public void ResetScore()
        {
            _score = 0;
            ScoreСhanged?.Invoke(_score);
        }

        private void AddScore()
        {
            _accruedScore = AccrueScore();
            _quantityOfMatchedPairs++;
            
            AddComboScore();
            
            ScoreСhanged?.Invoke(_score);
            
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
            
            ScoreСhanged?.Invoke(_score);
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
            
            LifeСhanged?.Invoke(_life);

            if (_life <= 0)
            {
                InitiateDefeat();
            }
        }

        private void InitiateVictory()
        {
            StopTimer();
            
            _gameSaver.AddPlayerCoin(_score);
            ThereWasAVictory?.Invoke();
        }

        private void InitTimer()   //TODO Rename
        {
            _quantityOfPairs = (_levelCreator.Cards.Count / (int)_levelCreator.LevelConfig.QuantityOfCardOfPair)
                             - _levelCreator.LevelConfig.QuantityPairOfSpecialCard;

            _life = _levelCreator.LevelConfig.Tries;
            _time = _levelCreator.LevelConfig.Time;
            _score = 0;

            LifeСhanged?.Invoke(_life);
            TimeСhanged?.Invoke(_time);
            ScoreСhanged?.Invoke(_score);

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
            TimeСhanged?.Invoke(0);
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
                
                TimeСhanged?.Invoke(_time);

                yield return new WaitForSeconds(1.0f);

                if (_time <= 1)
                {
                    InitiateDefeat();
                }
            }
        }
    }
}
