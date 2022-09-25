using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class PlayerStatusData : MonoBehaviour
{
    public static PlayerStatusData instance;
    
    public int skill;
    public int stress;
    public int health;
    public int money;
    public int food;
    
    public bool isHungry;

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
        food = 30;
    }
    public void SetHungry(bool isHungry)
    {
        this.isHungry = isHungry;
    }

    private void SetValueName(int valueName)
    {
        if(valueName < 0)
        {
            valueName = 0;
        }
        if(valueName > 100)
        {
            valueName = 100;
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
        if (health < randomHealth)
        {
            Debug.Log("Player need eat");
        }
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
        money += value;
        if (money < 0)
        {
            money = 0;
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
}