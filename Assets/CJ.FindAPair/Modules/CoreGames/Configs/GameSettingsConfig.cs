using System.Collections.Generic;
using UnityEngine;

namespace CJ.FindAPair.Modules.CoreGames.Configs
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Find a pair/Settings")]
    public class GameSettingsConfig : ScriptableObject
    {

        [SerializeField] [Range (1, 10)] private float _startTimeShow;
        [SerializeField] [Range(0.1f, 3.0f)] private float _animationSpeedCard;
        [SerializeField] [Range(0.1f , 3.0f)] private float _delayTimeHide;
        [SerializeField] private int _pointsTwoCards;
        [SerializeField] private int _pointsThreeCards;
        [SerializeField] private int _pointsFourCards;

        [SerializeField] private int _additionalTimeInSecond;
        [SerializeField] private int _additionalLife;
        [SerializeField] private int _delayAdsContinueGameInSecond;

        [SerializeField] private List<int> _scoreCombo;

        public float AnimationSpeedCard => _animationSpeedCard;
        public float StartTimeShow => _startTimeShow;
        public float DelayTimeHide => _delayTimeHide;
        public int PointsTwoCards => _pointsTwoCards;
        public int PointsThreeCards => _pointsThreeCards;
        public int PointsFourCards => _pointsFourCards;
        public int AdditionalTimeInSecond => _additionalTimeInSecond;
        public int AdditionalLife => _additionalLife;
        public int DelayAdsContinueGameInSecond => _delayAdsContinueGameInSecond;
        public List<int> ScoreCombo => _scoreCombo;
    }
}
