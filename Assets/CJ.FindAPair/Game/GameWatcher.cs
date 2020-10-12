using CJ.FindAPair.CardTable;
using CJ.FindAPair.Configuration;
using CJ.FindAPair.UI;
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

            _quantityOfPairs = _levelCreator.LevelConfig.LevelField.Count / 
                (int)_levelCreator.LevelConfig.QuantityOfCardOfPair;

            _life = _levelCreator.LevelConfig.Tries;
            _time = _levelCreator.LevelConfig.Time;
            _score = 0;
        }

        private void Start()
        {
            _lifeText.SetValue(_life.ToString());
            _timeText.SetValue(TimeConverer(_time));
            _scoreText.SetValue(_score.ToString());

            _timerCoroutine = TimerTick();

            StartCoroutine(_timerCoroutine);
        }

        private void OnEnable()
        {
            _cardComparator.СardsMatched += AddPoint;
            _cardComparator.СardsNotMatched += RemoveLife;
        }

        private void OnDisable()
        {
            _cardComparator.СardsMatched -= AddPoint;
            _cardComparator.СardsNotMatched -= RemoveLife;
        }

        private void AddPoint()
        {
            _score++;
            _quantityOfMatchedPairs++;
            _scoreText.SetValue(_score.ToString());

            if(_quantityOfMatchedPairs >= _quantityOfPairs)
            {
                СauseVictory();
            }
        }

        private void RemoveLife()
        {
            _life--;
            _lifeText.SetValue(_life.ToString());

            if(_life <= 0)
            {
                СauseDefeat();
            }
        }

        private void СauseVictory() //TODO
        {
            StopCoroutine(_timerCoroutine);
            Debug.Log("Вы затащили!!!!");
        }

        private void СauseDefeat()  //TODO
        {
            StopCoroutine(_timerCoroutine);
            Debug.Log("Вы просрали!!!!");
        }

        private string TimeConverer(int secondTime) //TODO
        {
            return secondTime.ToString();
        }

        private IEnumerator TimerTick()
        {
            yield return new WaitForSeconds(_gameSettingsConfig.StartTimeShow);

            while(true)
            {
                _time--;
                _timeText.SetValue(TimeConverer(_time));

                yield return new WaitForSeconds(1.0f);

                if (_time <= 1)
                {
                    СauseDefeat();
                }
            }
        }
    }
}

