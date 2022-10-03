using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class PlayerStatusData : MonoBehaviour
{
    public static PlayerStatusData instance;
    
    public delegate void EventName();
    public static event EventName OnResetAction;
    
    public int skill;
    public int stress;
    public int health;
    public int money;
    public int book = 5;
    public int food;
    public int action;
    private int _maxAction = 3;
    private int _totalAction;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        skill = 20;
        stress = 10;
        health = 100;
        money = 50;
        //books = 5;
        food = 30;
        action = _maxAction;
    }

    private void Update()
    {
        ResetAction();
    }
    public void ActionCost(int value)
    {
        action -= value;
        _totalAction += value;
    }

    public void ResetAction()
    {
        if (action == 0)
        {
            OnResetAction?.Invoke();
            action = _maxAction;
        }
    }
    public void SkillCost(int value)
    {
        skill += value;
        if(skill < 0)
        {
            skill = 0;
        }
        if(skill > 100)
        {
            skill = 100;
        }
    }
    public void StressCost(int value)
    {
        stress += value;
        if (stress < 0)
        {
            stress = 0;
        }
        if (stress > 100)
        {
            stress = 100;
        }
    }
    public void HealthCost(int value)
    {
        health += value;
        if (health < 0)
        {
            health = 0;
        }
        if (health > 100)
        {
            health = 100;
        }
    }
    public void ReduceHealth()
    {
        int randomHealth = Random.Range(30, 40);
        health -= randomHealth;
        if (health < 0)
        {
            health = 0;
        }
        if (health > 100)
        {
            health = 100;
        }
    }
    public void MoneyCost(int value)
    {
        var PositiveValue = Mathf.Abs(value);
       // Debug.Log(PositiveValue);

        if (money < PositiveValue)
        {
            //Debug.Log("Not enough money");
            return;
        }
        money += value;
        if (money < 0)
        {
            money = 0;
        }
    }
    public void BookCost(int value)
    {
        book += value;
        if (book < 0)
        {
            book = 0;
        }
    }
    public void FoodCost(int value)
    {
        if (value >= 20)
        {
            food += Random.Range(10, value);
        }
        else
        {
            food += value;
        }
        
        if (food < 0)
        {
            food = 0;
        }
    }
    public void ResetFood()
    {
        food = 0;
    }
}