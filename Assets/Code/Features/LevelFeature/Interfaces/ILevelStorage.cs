using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;

namespace Code.Features.LevelFeature.Interfaces
{
    public interface ILevelStorage
    {
        bool TryGetCurrentLevel(out LevelConfig level);
        void SetCurrentLevel(LevelConfig level);
        List<Card> GetCards();
        void SetCards(List<Card> cards);
    }
}