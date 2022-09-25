using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecisionObject : MonoBehaviour
{
    public delegate void OnClickInteraction();
    public static event OnClickInteraction OnClickInteracted;
    
    [SerializeField] private Button button;
    [SerializeField] private Button lockButton;
    [SerializeField] private TMP_Text text;
    [SerializeField] string decisionText;

    [SerializeField] private int skillCost;
    [SerializeField] private int stressCost;
    [SerializeField] private int healthCost;
    [SerializeField] private int moneyCost;
    [SerializeField] private int foodCost;
    private PlayerStatusData playerStatusData = PlayerStatusData.instance;
    public void Init(string GetName, int GetSkillCost, int GetStressCost,int GetHealthCost, int GetMoneyCost, int GetFoodCost)
    {
        decisionText = GetName;
        this.name = GetName;
        text.text = GetName;
        
        skillCost = GetSkillCost;
        stressCost = GetStressCost;
        healthCost = GetHealthCost;
        moneyCost = GetMoneyCost;
        foodCost = GetFoodCost;
    }
    public void OnClick(DecisionObject decisionObject,GameObject obj, BaseObject baseObject)
    {
        decisionObject.button.onClick.AddListener(()=> DecisionEffect(obj,baseObject));
        decisionObject.lockButton.onClick.AddListener(SetLockButton);
    }
    void DecisionEffect(GameObject obj,BaseObject baseObject)
    {
        OnClickInteracted?.Invoke();
        
        playerStatusData.SkillCost(skillCost);
        playerStatusData.StressCost(stressCost);
        playerStatusData.HealthCost(healthCost);
        playerStatusData.MoneyCost(moneyCost);
        playerStatusData.FoodCost(foodCost);
        
        SetSellDecision(obj,baseObject);
    }
    void SetSellDecision(GameObject obj,BaseObject baseObject)
    {
        if (decisionText == Constants.Requirments.Sell)
        {
            obj.SetActive(false);
            baseObject.gameObject.SetActive(false);
        }
    }
    public void SetRequirementDecision()
    {
        LaptopRequirement();
        DoorRequirement();
        RefrigeratorRequirement();
    }
    void LaptopRequirement()
    {
        if (decisionText == Constants.Requirments.Course)
        {
            if (playerStatusData.stress >= 50)
            {
                GetLockButton().SetActive(true);
                
            }
            else
            {
                GetLockButton().SetActive(false);
                
            }
        }
        if (decisionText == Constants.Requirments.GetJob)
        {
            if (playerStatusData.skill >= 100)
            {
                GetLockButton().SetActive(false);
            }
            else
            {
                GetLockButton().SetActive(true);
            }
        }
    }
    void DoorRequirement()
    {
        if (decisionText == Constants.Requirments.Vacation)
        {
            if (playerStatusData.money < 40)
            {
                GetLockButton().SetActive(true);
            }
            else
            {
                GetLockButton().SetActive(false);
            }
        }
        if (decisionText == Constants.Requirments.Snack)
        {
            if (playerStatusData.money < 20)
            {
                GetLockButton().SetActive(true);
            }
            else
            {
                GetLockButton().SetActive(false);
            }
        }
        if (decisionText == Constants.Requirments.Lock)
        {
            if (playerStatusData.money < 20)
            {
                GetLockButton().SetActive(true);
            }
            else
            {
                GetLockButton().SetActive(false);
            }
        }
    }
    void RefrigeratorRequirement()
    {
        if (decisionText == Constants.Requirments.Eat)
        {
            if (playerStatusData.food < 10 || playerStatusData.health >= 100)
            {
                GetLockButton().SetActive(true);
            }
            else
            {
                GetLockButton().SetActive(false);
            }
        }
    }
    private GameObject GetLockButton()
    {
        return lockButton.gameObject;
    }
    void SetLockButton()
    {
        if (decisionText == Constants.Requirments.Course)
        {
            //Debug.Log(name + " is locked");
        }
        if (decisionText == Constants.Requirments.GetJob)
        {
            //Debug.Log(name + " is locked");
        }
        Debug.Log(name + " is locked");
    }
}