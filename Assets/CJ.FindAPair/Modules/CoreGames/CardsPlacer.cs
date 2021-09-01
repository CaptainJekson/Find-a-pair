using CJ.FindAPair.Modules.CoreGames.Configs;
using UnityEngine;

namespace CJ.FindAPair.Modules.CoreGames
{
    public class CardsPlacer : MonoBehaviour
    {
        public void PlaceCards(LevelConfig _level, Card cardPrefab)
        {
            foreach (var cell in _level.LevelField)
            {
                var newCard = Instantiate(cardPrefab, transform);

                var width = _level.Height;
                var height = _level.Width;
            }
        }
    }
}
