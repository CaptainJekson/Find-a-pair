using CJ.FindAPair.Modules.CoreGames.Booster;
using CJ.FindAPair.Modules.CoreGames.SpecialCards;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.CoreGames.Installer
{
    public class CoreGamePlacer : MonoBehaviour
    {
        [Inject]
        public void ConstructCoreGame(LevelCreator levelCreator, RayCaster rayCaster, BoosterHandler boosterHandler, 
            SpecialCardHandler specialCardHandler)
        {
            levelCreator.transform.SetParent(transform);
            rayCaster.transform.SetParent(transform);
            boosterHandler.transform.SetParent(transform);
            specialCardHandler.transform.SetParent(transform);
        }
    }
}