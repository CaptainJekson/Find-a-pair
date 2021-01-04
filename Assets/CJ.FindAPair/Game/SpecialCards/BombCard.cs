using CJ.FindAPair.CardTable;

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