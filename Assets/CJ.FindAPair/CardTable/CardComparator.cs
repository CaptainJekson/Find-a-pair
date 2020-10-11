using CJ.FindAPair.CardTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LevelCreator))]
public class CardComparator : MonoBehaviour
{
    private LevelCreator _levelCreator;
    private List<Card> _comparisonCards;

    public event UnityAction СardsMatched;
    public event UnityAction СardsNotMatched;

    private void Awake()
    {
        _levelCreator = GetComponent<LevelCreator>();
        _comparisonCards = new List<Card>();
    }

    private void OnEnable()
    {
        StartCoroutine(DelaySubscription(0.1f));
    }

    private void OnDisable()
    {
        foreach (var card in _levelCreator.Cards)
        {
            card.СardOpens -= () => ToCompare(card);
        }
    }

    private IEnumerator DelaySubscription(float time)
    {
        yield return new WaitForSeconds(time);

        foreach (var card in _levelCreator.Cards)
        {
            card.СardOpens += () => ToCompare(card);
        }
    }

    private void ToCompare(Card card)
    {
        var quantityOfCardOfPair = (int)_levelCreator.LevelConfig.QuantityOfCardOfPair;

        _comparisonCards.Add(card);

        for (int i = 0; i < _comparisonCards.Count - 1; i++)
        {
            var isCardEqual = _comparisonCards[i].NumberPair 
                == _comparisonCards[_comparisonCards.Count - 1].NumberPair;

            if (isCardEqual)
            {
                if (_comparisonCards.Count >= quantityOfCardOfPair && isCardEqual)
                {
                    СardsMatched?.Invoke();
                    _comparisonCards.Clear();
                }
            }
            else
            {
                СardsNotMatched?.Invoke();
                HideCards();
                _comparisonCards.Clear();
            }
        }
    }

    private void HideCards()
    {
        foreach (var _comparisonCard in _comparisonCards)
        {
            _comparisonCard.DelayHide();
        }
    }
}
