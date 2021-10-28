using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CJ.FindAPair.Modules.CoreGames.Configs
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Find a pair/Settings")]
    public class GameSettingsConfig : ScriptableObject
    {
        [SerializeField] [Range(0.1f, 3.0f)] private float _animationSpeedCard;
        [SerializeField] [Range(0.1f , 3.0f)] private float _delayTimeHide;
        [SerializeField] private int _pointsTwoCards;
        [SerializeField] private int _pointsThreeCards;
        [SerializeField] private int _pointsFourCards;

        [SerializeField] private int _additionalTimeInSecond;
        [SerializeField] private int _additionalLife;
        [SerializeField] private int _cooldownAdsContinueGameInSecond;

        [SerializeField] private List<int> _scoreCombo;

        public float AnimationSpeedCard => _animationSpeedCard;
        public float DelayTimeHide => _delayTimeHide;
        public int PointsTwoCards => _pointsTwoCards;
        public int PointsThreeCards => _pointsThreeCards;
        public int PointsFourCards => _pointsFourCards;
        public int AdditionalTimeInSecond => _additionalTimeInSecond;
        public int AdditionalLife => _additionalLife;
        public int CooldownAdsContinueGameInSecond => _cooldownAdsContinueGameInSecond;
        public List<int> ScoreCombo => _scoreCombo;
    }
}
