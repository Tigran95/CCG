using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardsRotator : MonoBehaviour
{


    [SerializeField] private Transform _rotatorsPlace;
    [SerializeField] private float _rotationDeviationValue;
    [SerializeField] private float _tweenSpeed;
    private float _rotationDeviationStartValue;


    public static CardsRotator Instance;
   
    public float TweenSpeed { get { return _tweenSpeed; } }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _rotationDeviationStartValue = _rotationDeviationValue;
    }
    public void Initialize()
    {
        
        for(int i = 0; i < CardsPooler.Instance.AllCards.Count; i++)
        {
            CardsPooler.Instance.AllCards[i].SetParentForRotator(_rotatorsPlace);
        }
        RotateCardsInSeconds(0.0f);
    }

    public void RotateCardsInSeconds(float sec)
    {
        InputController.InputBlocked = true;
        _rotationDeviationValue = _rotationDeviationStartValue* CardsPooler.Instance.AllCards.Count / 10;

        for (int i = 0; i < CardsPooler.Instance.AllCards.Count; i++)
        {
            CardsPooler.Instance.AllCards[i].RotateWithValueAndSec(_rotationDeviationValue,sec);
        }
    }


}
