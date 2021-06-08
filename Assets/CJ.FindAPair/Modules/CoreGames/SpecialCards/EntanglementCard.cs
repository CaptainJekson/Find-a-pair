﻿using System.Collections.Generic;
using System.Linq;
using CJ.FindAPair.Constants;
using UnityEngine;

namespace CJ.FindAPair.Modules.CoreGames.SpecialCards
{
    public class EntanglementCard : SpecialCard
    {
        public override void OpenSpecialCard(CardOld specialCardOld)
        {
            var cards = GetNotMatchedCards();
            ShuffleNumberCard(cards);
            specialCardOld.DelayHide();
        }

        private List<CardOld> GetNotMatchedCards()
        {
            var allCards = _levelCreator.Cards;

            return allCards.Where(card => !card.IsMatched && card.NumberPair < ConstantsCard.NUMBER_SPECIAL).ToList();
        }
        
        private void ShuffleNumberCard(List<CardOld> cards)
        {
            for (var i = cards.Count - 1; i > 0; i--)
            {
                var j = Random.Range(0, i);
                var temp = cards[i].NumberPair;
                cards[i].NumberPair = cards[j].NumberPair;
                cards[j].NumberPair = temp;
            }
            
            foreach (var card in cards)
            {
                card.SetNumberText();
            }
        }
    }
}