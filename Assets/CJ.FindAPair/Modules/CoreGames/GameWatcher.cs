using System;
using System.Collections;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.Service.Ads;
using CJ.FindAPair.Modules.Service.Ads.Configs;
using CJ.FindAPair.Modules.Service.Save;
using DG.Tweening;
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
        private ISaver _gameSaver;
        private IAdsDriver _adsDriver;
        private UnityAdsConfig _unityAdsConfig;

        private int _life;
        private int _time;
        private int _score;
        private int _comboCounter;
        private int _accruedScore;

        private int _quantityOfPairs;
        private int _quantityOfMatchedPairs;

        private IEnumerator _timerCoroutine;
        private Action _showAdsAction;

        public event UnityAction<int> ScoreСhanged;
        public event UnityAction<int> LifeСhanged;
        public event UnityAction<int> TimeСhanged;
        public event UnityAction ThereWasAVictory;
        public event UnityAction ThereWasADefeat;
        public event UnityAction LivesIsOut;
        public event UnityAction TimeIsOut;

        [Inject]
        public void Construct(LevelCreator levelCreator, CardComparator cardComparator,
            GameSettingsConfig gameSettingsConfig, ISaver gameSaver, IAdsDriver adsDriver,
            UnityAdsConfig unityAdsConfig)
        {
            _levelCreator = levelCreator;
            _cardComparator = cardComparator;
            _gameSettingsConfig = gameSettingsConfig;
            _gameSaver = gameSaver;
            _adsDriver = adsDriver;
            _unityAdsConfig = unityAdsConfig;
        }

        private void OnEnable()
        {
            _cardComparator.CardsMatched += AddScore;
            _cardComparator.CardsNotMatched += RemoveLife;
            _levelCreator.OnLevelCreated += StartTheGame;
            _levelCreator.OnLevelDeleted += ResetTimer;
            _levelCreator.OnLevelDeleted += ResetCounts;
            _adsDriver.AdsIsSkipped += InitiateDefeatAtSkipAds;
            _adsDriver.AdsIsFailed += InitiateDefeatAtSkipAds;
            _adsDriver.AdsIsComplete += AddCoolDownAdsTime;
        }
        
        private void OnDisable()
        {
            _cardComparator.CardsMatched -= AddScore;
            _cardComparator.CardsNotMatched -= RemoveLife;
            _levelCreator.OnLevelCreated -= StartTheGame;
            _levelCreator.OnLevelDeleted -= ResetTimer;
            _levelCreator.OnLevelDeleted -= ResetCounts;
            _adsDriver.AdsIsSkipped -= InitiateDefeatAtSkipAds;
            _adsDriver.AdsIsFailed -= InitiateDefeatAtSkipAds;
            _adsDriver.AdsIsComplete -= AddCoolDownAdsTime;
        }
        
        public void InitiateDefeat()
        {
            StopTimer();
            _showAdsAction?.Invoke();
            _showAdsAction = null;
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

        public void ContinueGameWithAdsInEnd()
        {
            _life += _gameSettingsConfig.AdditionalLife;
            _time += _gameSettingsConfig.AdditionalTimeInSecond;
            LifeСhanged?.Invoke(_life);   
            TimeСhanged?.Invoke(_time);
            StartTimer();
            
            _showAdsAction = () =>
            {
                _adsDriver.ShowAds(_unityAdsConfig.PlacementRewardedVideoId);
            };
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
                //card.GetComponent<AnimationCardOld>().PlayCombo(scoreCombo);
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
                LivesIsOut?.Invoke();
            }
        }

        private void InitiateVictory()
        {
            StopTimer();
            _showAdsAction?.Invoke();
            _showAdsAction = null;
            
            SaveCoins();

            ThereWasAVictory?.Invoke();
        }

        private void SaveCoins()
        {
            var saveData = _gameSaver.LoadData();
            saveData.ItemsData.Coins += _score;
            _gameSaver.SaveData(saveData);
        }

        private void InitiateDefeatAtSkipAds(string placementId)
        {
            if (placementId == _unityAdsConfig.PlacementRewardedVideoId)
            {
                InitiateDefeat();
            }
        }
        
        private void AddCoolDownAdsTime(string placementId)
        {
            if (placementId == _unityAdsConfig.PlacementRewardedVideoId)
            {
                
            }
        }
        
        private void StartTheGame()
        {
            _quantityOfPairs = (_levelCreator.Cards.Count / (int)_levelCreator.LevelConfig.QuantityOfCardOfPair)
                             - _levelCreator.LevelConfig.QuantityPairOfSpecialCard;

            _life = _levelCreator.LevelConfig.Tries;
            _time = _levelCreator.LevelConfig.Time;
            _score = 0;

            LifeСhanged?.Invoke(_life);
            TimeСhanged?.Invoke(_time);
            ScoreСhanged?.Invoke(_score);

            var sequence = DOTween.Sequence();
            sequence.AppendInterval(_gameSettingsConfig.StartTimeShow);
            sequence.AppendCallback(StartTimer);
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
            while (true)
            {
                _time--;
                
                TimeСhanged?.Invoke(_time);

                yield return new WaitForSeconds(1.0f);

                if (_time <= 1)
                {
                    InitiateDefeat();
                    TimeIsOut?.Invoke();
                }
            }
        }
    }
}
