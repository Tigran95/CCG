using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manas : MonoBehaviour
{
    [SerializeField] private Mana _mana;
    [SerializeField] private RectTransform _instantPlace;
    [SerializeField] private int _instantCount;

    private List<Mana> _allManas = new List<Mana>();

    public int ManasCount { get { return _allManas.Count; } }

    public static Manas Instance;
    private void Awake()
    {
        Instance = this;
        PoolManas();
    }

    private void PoolManas()
    {
        for (int i = 0; i < _instantCount; i++)
        {
            Mana mana = Instantiate(_mana, _instantPlace);
            _allManas.Add(mana);
        }
    }

    
}
