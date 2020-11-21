using System;

public static class CardRemover 
{
    public static void RemoveCardFromList(Card card)
    {
        CardsPooler.Instance.AllCards.Remove(card);
        CardsDataChanger.Instance.ChangeIndexAfterCardDestroy();
        CardsRotator.Instance.RotateCardsInSeconds(CardsRotator.Instance.TweenSpeed);
    }
}
