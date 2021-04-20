using CJ.FindAPair.CoreGames;

namespace CJ.FindAPair.Modules.CoreGames.SpecialCards
{
    public class BombCard : SpecialCard
    {
        public override void OpenSpecialCard(Card specialCard)
        {
            _gameWatcher.InitiateDefeat();
        }
    }
}