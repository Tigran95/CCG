using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CardData", menuName = "Card Data", order = 51)]
public class CardData : ScriptableObject
{
    [SerializeField] private int _hp;

    [SerializeField] private int _power;

    [SerializeField] private int _mana;

    [SerializeField] private string _name;

    [SerializeField] private string _description;

    public int HP { get { return _hp; } set { _hp = value; } }
    public int Power { get { return _power; } set { _power = value; } }
    public int Mana { get { return _mana; } set { _mana = value; } }
    public string Name { get { return _name; } set { _name = value; } }
    public string Description { get { return _description; } set { _description = value; } }
}
