using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardsDataChanger : MonoBehaviour
{
    [SerializeField] private Button _changeCardsDataBtn;
    private int _indexOfCard;


    public static CardsDataChanger Instance;
    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        _changeCardsDataBtn.onClick.AddListener(() => ChangeCardsData());
    }
    private void ChangeCardsData()
    {
        if (InputController.InputBlocked) { return; }
        if (CardsPooler.Instance.AllCards.Count == 0)
        {
            Debug.Log("CardsEmpty");
            return;
        }

        if(_indexOfCard >= CardsPooler.Instance.AllCards.Count)
        {
            Debug.Log("ReverseCheck");
            _indexOfCard = 0;
        }

        Card card = CardsPooler.Instance.AllCards[_indexOfCard];

        card.CurrentCardData.HP = CheckingSimulationManager.Instance.RandomHP;
        card.CurrentCardData.Mana = CheckingSimulationManager.Instance.RandomMana;
        card.CurrentCardData.Power = CheckingSimulationManager.Instance.RandomAttack;
        card.UpdateCardsDataView();
        _indexOfCard++;
    }

    public void ChangeIndexAfterCardDestroy()
    {
        _indexOfCard--;
        if (_indexOfCard < 0)
        {
            _indexOfCard = 0;
        }
    }
}
