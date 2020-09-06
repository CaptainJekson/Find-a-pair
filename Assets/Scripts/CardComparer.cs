using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardComparer : MonoBehaviour
{
    public const int _first = 0;
    public const int _second = 1;

    private List<Card> _pickedCards;

    public bool IsCoupleCard => _pickedCards.Count >= 2;
    public int QuantityGuessedCouples { get; set; }

    public event UnityAction СardsMatched;
    public event UnityAction СardsNotMatched;

    private void Awake()
    {
        _pickedCards = new List<Card>();
    }

    public void AddCardToCompare(Card card)
    {
        if (IsCoupleCard)
            _pickedCards.Clear();

        _pickedCards.Add(card);
    }

    public void ToCompare()
    {
        if (_pickedCards[_first].Number == _pickedCards[_second].Number)
        {
            _pickedCards[_first].ActivateGuessing();
            _pickedCards[_second].ActivateGuessing();
            QuantityGuessedCouples++;

            СardsMatched?.Invoke();
        }
        else
        {
            _pickedCards[_first].HideCard();
            _pickedCards[_second].HideCard();

            СardsNotMatched?.Invoke();
        }
    }
}
