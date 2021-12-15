using System;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.Service.Ads;
using CJ.FindAPair.Modules.Service.Ads.Configs;
using CJ.FindAPair.Modules.UI.Installer;
using DG.Tweening;
using Zenject;

namespace CJ.FindAPair.Modules.CoreGames
{
    public class GameWatcher
    {
        private GameSettingsConfig _gameSettingsConfig;
        private LevelCreator _levelCreator;
        private CardComparator _cardComparator;
        private CardsPlacer _cardsPlacer;
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

        private Sequence _timerSequence;
        private Action _showAdsAction;
        private UIRoot _uiRoot;

        public int Score => _score;
        
        public event Action<int> ScoreСhanged;
        public event Action<int> LifeСhanged;
        public event Action<int> TimeСhanged;
        public event Action ThereWasAVictory;
        public event Action ThereWasADefeat;
        public event Action LivesIsOut;
        public event Action TimeIsOut;
        public event Action ConfirmShowAds;

        [Inject]
        public void Construct(LevelCreator levelCreator, CardComparator cardComparator,
            GameSettingsConfig gameSettingsConfig, ISaver gameSaver, IAdsDriver adsDriver,
            UnityAdsConfig unityAdsConfig, UIRoot uiRoot, CardsPlacer cardsPlacer)
        {
            _levelCreator = levelCreator;
            _cardComparator = cardComparator;
            _gameSettingsConfig = gameSettingsConfig;
            _cardsPlacer = cardsPlacer;
            _gameSaver = gameSaver;
            _adsDriver = adsDriver;
            _unityAdsConfig = unityAdsConfig;
            _uiRoot = uiRoot;
            _timerSequence = DOTween.Sequence();
            
            Subscribe();
        }

        private void Subscribe()
        {
            _cardComparator.CardsMatched += AddScore;
            _cardComparator.CardsNotMatched += RemoveLife;
            _cardsPlacer.CardsDealt += StartTheGame;
            _levelCreator.OnLevelDeleted += ResetTimer;
            _levelCreator.OnLevelDeleted += ResetCounts;
            _adsDriver.AdsIsSkipped += InitiateDefeatAtSkipAds;
            _adsDriver.AdsIsFailed += InitiateDefeatAtSkipAds;
            _adsDriver.AdsIsComplete += AddCoolDownAdsTime;
        }
        
        public bool IsIncomeLevel()
        { 
            var completedLevels = _gameSaver.LoadData().CompletedLevels;

            foreach (var completedLevel in completedLevels)
            {
                if (completedLevel == _levelCreator.LevelConfig.LevelNumber)
                {
                    return false;
                }
            }

            return true;
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
            _quantityOfMatchedPairs--;
            
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
            ConfirmShowAds?.Invoke();
            StartTimer();
            
            _showAdsAction = () =>
            {
                _adsDriver.ShowAds(_unityAdsConfig.PlacementRewardedVideoId);
            };
        }        

        private void AddScore()
        {
            if (IsIncomeLevel())
            {
                _accruedScore = AccrueScore();
                AddComboScore();
                ScoreСhanged?.Invoke(_score);
                _comboCounter++;
            }
            
            _quantityOfMatchedPairs++;
            
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

            ThereWasAVictory?.Invoke();
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
            sequence.AppendCallback(() => _uiRoot.CloseWindow<FullBlockerWindow>());
            sequence.AppendCallback(StartTimer);
        }

        private void StartTimer()
        {
            if (_time <= 0) return;
            _timerSequence = DOTween.Sequence();
            TimerTick();
        }

        private void StopTimer()
        {
            _timerSequence.Kill();
        }

        private void ResetTimer()
        {
            _timerSequence.Kill();
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

        private void TimerTick()
        {
            _timerSequence.AppendInterval(1.0f);
            _timerSequence.AppendCallback(() => _time--);
            _timerSequence.AppendCallback(() =>
            { 
                TimeСhanged?.Invoke(_time);
                
                if (_time <= 1)
                {
                    InitiateDefeat();
                    TimeIsOut?.Invoke();
                }
            });
            _timerSequence.SetLoops(-1, LoopType.Incremental);
        }
    }
}