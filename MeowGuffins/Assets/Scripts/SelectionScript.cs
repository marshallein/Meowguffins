using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScript : MonoBehaviour
{
    public const int MAX_CARDS = 2;

    public static Queue<MeowCard> SelectedCards;

    private static SelectionScript _instance;
    public static SelectionScript Instance { get => _instance;  }

    private void Awake()
    {
        _instance = this;
        SelectedCards = new Queue<MeowCard>();
    }

    public void SelectCard(MeowCard card)
    {
        if (SelectedCards.Contains(card))
        {
            return;
        }
        if (SelectedCards.Count >= MAX_CARDS)
        {
            var toBeDeselectedCard = SelectedCards.Dequeue();
            toBeDeselectedCard.Deactivate();
        }
        SelectedCards.Enqueue(card);
        card.Activate();
    }
}
