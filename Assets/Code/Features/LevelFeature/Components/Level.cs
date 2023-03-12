using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using Scellecs.Morpeh;

namespace Code.Features.LevelFeature.Components
{
    public struct Level : IComponent
    {
        public LevelConfig levelConfig;
        public Dictionary<Card, bool> cards;
        public List<Card> enableCards;
        public List<Card> disableCards;
    }
}