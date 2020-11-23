using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PointValuesController : MonoBehaviour
{
    private int currentValue = 100;
    private int numberOfCards = 0;
    [SerializeField] private TMP_Text tmpText;
    private int frame = 0;


    // Start is called before the first frame update
    void Start()
    {
        tmpText.text = currentValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //this.frame++;
        //if (this.frame >= 60)
        //{
        //    this.addOnePointPerSec();
        //}
    }

    // Add points to score - killing mobs, surviving waves
    public void addPoints(int value)
    {
        this.currentValue += value;
        tmpText.text = currentValue.ToString();
    }

    // Remove points from score - placing figures
    public void removePoints(int value)
    {
        this.currentValue -= value;
        if (currentValue < 0)
        {
            this.addCard();
            currentValue = 0;
        }
        tmpText.text = currentValue.ToString();
    }

    // Add yellow card
    public void addCard()
    {
        if (this.numberOfCards >= 3)
        {
            Console.WriteLine("GAME OVER");
        }
        this.numberOfCards++;

    }

    // testing purposes
    public void addOnePointPerSec()
    {
        this.addPoints(1);
        this.frame = 0;
    }

}
