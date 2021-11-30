﻿using UnityEngine.Events;

namespace CJ.FindAPair.Modules.CoreGames.SpecialCards
{
    public class BombCard : SpecialCard
    {
        public event UnityAction BombDetonate;
        
        public override void OpenSpecialCard(Card specialCard)
        {
            _gameWatcher.InitiateDefeat();
            BombDetonate?.Invoke();
        }
    }
}