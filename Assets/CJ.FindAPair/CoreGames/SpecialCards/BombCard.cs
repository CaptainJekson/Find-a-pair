using CJ.FindAPair.CardTable;
using CJ.FindAPair.CoreGames;

namespace CJ.FindAPair.Game.SpecialCards
{
    public class BombCard : SpecialCard
    {
        public override void OpenSpecialCard(Card specialCard)
        {
            _gameWatcher.InitiateDefeat();
        }
    }
}