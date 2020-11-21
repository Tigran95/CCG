using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Card : MonoBehaviour
{
    [SerializeField] private Transform _rotator;
    [SerializeField] private CardInput _cardInput;
    [SerializeField] private Image _destroyPreparerImg;
    [SerializeField] private Image _readyForUseImg;

    [SerializeField] private Text _name;
    [SerializeField] private Text _descr;
    [SerializeField] private Text _hp;
    [SerializeField] private Text _mana;
    [SerializeField] private Text _power;

    public bool CanIDrag { get; private set; }

    public Transform Rotator { get { return _rotator; } }
    public void SetParentForRotator(Transform rotatorsParent)
    {
        _rotator.parent = rotatorsParent;
    }

    private CardData _cardData;

    public CardData CurrentCardData { get { return _cardData; } }

    public void SetCardsStartData(CardData data)
    {
        _cardData = new CardData(); 
        _cardData.HP = data.HP;
        _cardData.Mana = data.Mana;
        _cardData.Power = data.Power;
        _cardData.Description = data.Description;
        _cardData.Name = data.Name;
        UpdateCardsDataView();
    }

    public void UpdateCardsDataView()
    {
        _name.text = _cardData.Name;
        _descr.text = _cardData.Description;
        _hp.text = _cardData.HP.ToString();
        _mana.text = _cardData.Mana.ToString();
        _power.text = _cardData.Power.ToString();

        if (_cardData.HP < 1)
        {
            PrepareCardForDestroy();
        }
        else
        {
            CardUsage();
        }
    }

    public void RotateWithValueAndSec(float rotationDeviation,float sec)
    {
      
        transform.parent = _rotator;
        Quaternion rotatorDefoultRotation = _rotator.transform.rotation;

        int allCardsCount = CardsPooler.Instance.AllCards.Count;

        Sequence seq = DOTween.Sequence();

        if (allCardsCount != 1)
        {
            seq.Join (_rotator.transform.DORotate(new Vector3(0, 0, 
                rotationDeviation - CardsPooler.Instance.AllCards.IndexOf(this) * rotationDeviation * 2 / (allCardsCount - 1)),sec)).SetEase(Ease.OutQuint);
        }
        else
        {
            seq.Join(_rotator.transform.DORotate(new Vector3(0, 0, 0), sec)).SetEase(Ease.OutQuint);
        }

        seq.OnComplete(() =>
        {
            InputController.InputBlocked = false;
            transform.parent = CardsPooler.Instance.CardsPlace;
            _cardInput.UpdateInListPositions();
        });
    }

    private void PrepareCardForDestroy()
    {
        Destroy(GetComponent<CardInput>());
        var seq = DOTween.Sequence();
        seq.Append(_destroyPreparerImg.DOColor(new Color(_destroyPreparerImg.color.r, _destroyPreparerImg.color.g, _destroyPreparerImg.color.b, 1), 0.5f));
        seq.OnComplete(() =>
        {
            CardRemover.RemoveCardFromList(this);
            Destroy(gameObject);
        });
    }

    private void CardUsage()
    {
        if (_cardData.Mana <= Manas.Instance.ManasCount)
        {
            CanIDrag = true;
            var seq = DOTween.Sequence();
            seq.Append(_readyForUseImg.DOColor(new Color(1, 1, 1, 1), 0.5f));
        }
        else
        {
            CanIDrag = false;
            var seq = DOTween.Sequence();
            seq.Append(_readyForUseImg.DOColor(new Color(1, 1, 1, 0), 0.2f));
        }
    }
}
