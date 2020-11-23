using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PointValuesController : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpText;
    [SerializeField] private List<GameObject> cards;
    
    public event Action OnGetCard = delegate { };

    private CurrentLevelController currentLevelController;
    private int numberOfCards = 0;
    private int frame = 0;
    private int currentValue;

    public void Init(CurrentLevelController currentLevelController, int pointsStartValue)
    {
        this.currentLevelController = currentLevelController;
        currentValue = pointsStartValue;
        tmpText.text = currentValue.ToString();
        cards.ForEach((x) => x.gameObject.SetActive(false));
    }

    public void AddPoints(int value)
    {
        this.currentValue += value;
        tmpText.text = currentValue.ToString();
    }

    public bool RemovePoints(int value)
    {
        currentValue -= value;

        if (currentValue < 0)
        {
            currentValue += value;
            AddCard();
            OnGetCard();
            tmpText.text = currentValue.ToString();
            return false;
        }

        tmpText.text = currentValue.ToString();
        return true;
    }

    private void AddCard()
    {
        this.numberOfCards++;
        cards[numberOfCards - 1].SetActive(true);

        if (this.numberOfCards >= 3)
        {
            currentLevelController.GameOver();
        }
    }
}