using CJ.FindAPair.Modules.CoreGames;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "TemplatesConfig", menuName = "Find a pair/TemplatesConfig")]
    public class TemplatesConfig : ScriptableObject
    {
        [SerializeField] public Card card;
    }
}