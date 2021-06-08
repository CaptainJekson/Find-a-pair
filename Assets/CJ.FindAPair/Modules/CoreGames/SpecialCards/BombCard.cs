namespace CJ.FindAPair.Modules.CoreGames.SpecialCards
{
    public class BombCard : SpecialCard
    {
        public override void OpenSpecialCard(Card specialCardOld)
        {
            _gameWatcher.InitiateDefeat();
        }
    }
}