using System.Collections;
using System.Linq;
using CJ.FindAPair.Animation;
using UnityEngine;

namespace CJ.FindAPair.Modules.CoreGames.Booster
{
    public class ElectroshockBooster : Modules.CoreGames.Booster.Booster
    {
        [SerializeField][Range(1.0f, 7.0f)] private float _timeShow;
        
        public override void ActivateBooster()
        {
            foreach (var card in _levelCreator.Cards.Where(card => !card.IsMatched))
            {
                card.Show(true);
                card.GetComponent<AnimationCard>().PlayElectroshock();
            }
            
            StartCoroutine(DelayHideCards());
        }

        private IEnumerator DelayHideCards()
        {
            yield return new WaitForSeconds(_timeShow);

            foreach (var card in _levelCreator.Cards.Where(card => !card.IsMatched))
                card.Hide(true);
        }
    }
}