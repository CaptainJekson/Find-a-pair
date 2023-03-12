using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using Code.Features.LevelFeature.Interfaces;
using Scellecs.Morpeh;

namespace Code.Features.LevelFeature.Systems
{
    public class LevelStorageSystem : IInitializer, ILevelStorage
    {
        private LevelConfig _level;
        private List<Card> _cards;
        
        public World World { get; set; }
        
        public void OnAwake()
        {

        }

        public bool TryGetCurrentLevel(out LevelConfig level)
        {
            if (_level == null)
            {
                level = null;
                return false;
            }

            level = _level;
            return true;
        }

        public void SetCurrentLevel(LevelConfig level)
        {
            _level = level;
        }

        public List<Card> GetCards()
        {
            return _cards;
        }

        public void SetCards(List<Card> cards)
        {
            _cards = cards;
        }

        public void Dispose()
        {
            _level = null;
        }
    }
}