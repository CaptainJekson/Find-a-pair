using UnityEngine;


namespace CJ.FindAPair.Configuration
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Find a pair/Settings")]
    public class GameSettingsConfig : ScriptableObject
    {
        [SerializeField] [Range (1, 10)] private float _startTimeShow;
        [SerializeField] [Range(1, 7)] private float _animationSpeedCard;
        [SerializeField] [Range(0.1f , 3.0f)] private float _delayTimeHide;

        public float AnimationSpeedCard => _animationSpeedCard;
        public float StartTimeShow => _startTimeShow;

        public float DelayTimeHide => _delayTimeHide; 
    }
}
