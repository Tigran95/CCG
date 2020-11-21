using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingSimulationManager : MonoBehaviour
{
    [SerializeField] private List<CardData> _allCardsData = new List<CardData>();

    public int RandomHP { get { return Random.Range(-2, 10); } }
    public int RandomMana { get { return Random.Range(1, 15); } }
    public int RandomAttack { get { return Random.Range(1, 10); } }

    public static CheckingSimulationManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public List<CardData> GetNonDublicatedCardsByCount(int Count)
    {
        if (Count > _allCardsData.Count)
        {
            throw new System.Exception("The number of cards is greater than the data");
        }
        List<CardData> forReturn = new List<CardData>();
        int count = _allCardsData.Count;
        for(int i = 0; i < Count; i++)
        {
            CardData randomData = _allCardsData[Random.Range(0, count)];
            _allCardsData.Remove(randomData);
            _allCardsData.Add(randomData);
            forReturn.Add(randomData);
            count--;
        }
        return forReturn;
    }
}
