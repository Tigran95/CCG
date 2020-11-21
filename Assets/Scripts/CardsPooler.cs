using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsPooler : MonoBehaviour
{
    [SerializeField] private Card _card;
    [SerializeField] private int _cardsPoolCount;
    [SerializeField] private Transform _cardsPlace;

    private List<Card> _allCards = new List<Card>();

    public List<Card> AllCards { get { return _allCards; } }
    public Transform CardsPlace { get { return _cardsPlace; } }

    public static CardsPooler Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Pool();
    }

    private void Pool()
    {
        for(int i = 0; i < _cardsPoolCount; i++)
        {
            Card card = Instantiate(_card, _cardsPlace);
            _allCards.Add(card);
        }

       List<CardData> data= CheckingSimulationManager.Instance.GetNonDublicatedCardsByCount(_cardsPoolCount);

        for(int i = 0; i < data.Count; i++)
        {
            _allCards[i].SetCardsStartData(data[i]);
        }

        CardsRotator.Instance.Initialize();
    }


}
