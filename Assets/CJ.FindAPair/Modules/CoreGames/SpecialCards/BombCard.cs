namespace CJ.FindAPair.Modules.CoreGames.SpecialCards
{
    public class BombCard : SpecialCard
    {
        public override void OpenSpecialCard(CardOld specialCardOld)
        {
            _gameWatcher.InitiateDefeat();
        }
    }
}