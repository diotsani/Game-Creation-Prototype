using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecisionObject : MonoBehaviour
{
    public delegate void EventName();
    public static event EventName OnClickInteracted;
    
    public Button button;
    [SerializeField] private Button lockButton;
    [SerializeField] private TMP_Text text;
    public string decisionText;

    [SerializeField] private int skillCost;
    [SerializeField] private int stressCost;
    [SerializeField] private int healthCost;
    [SerializeField] private int moneyCost;
    [SerializeField] private int bookCost;
    [SerializeField] private int foodCost;
    public BaseObject baseObject { get; private set;}
    private PlayerStatusData playerStatusData = PlayerStatusData.instance;
    public void Init(string GetName, int GetSkillCost, int GetStressCost,int GetHealthCost, int GetMoneyCost,int GetBookCost, int GetFoodCost)
    {
        decisionText = GetName;
        this.name = GetName;
        text.text = GetName;
        
        skillCost = GetSkillCost;
        stressCost = GetStressCost;
        healthCost = GetHealthCost;
        moneyCost = GetMoneyCost;
        bookCost = GetBookCost;
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
        playerStatusData.BookCost(bookCost);
        playerStatusData.FoodCost(foodCost);
        
        //SetSellDecision(obj,baseObject);
        //SetReapairDecision(baseObject);
    }
    void SetSellDecision(GameObject obj,BaseObject baseObject)
    {
        if (decisionText == Constants.Requirments.Sell)
        {
            obj.SetActive(false);
            baseObject.gameObject.SetActive(false);
        }
    }
    void SetReapairDecision(BaseObject baseObject)
    {
        if (decisionText == Constants.Requirments.Repair)
        {
            baseObject.ResetAllDecision();
            baseObject.ResetAmountClick();
            baseObject._objectState = ObjectState.Good;
        }
    }
    public void SetRequirementDecision()
    {
        //LaptopRequirement();
        DoorRequirement();
        RefrigeratorRequirement();
    }
    void LaptopRequirement()
    {
        if (decisionText == Constants.Requirments.TakeCourse)
        {
            if (playerStatusData.stress >= 50)
            {
                GetLockButton().SetActive(true);
                return;
            }
            GetLockButton().SetActive(false);
        }
        if (decisionText == Constants.Requirments.ApplyJob)
        {
            if (playerStatusData.skill >= 100)
            {
                GetLockButton().SetActive(false);
                return;
            }
            GetLockButton().SetActive(true);
        }
        if(decisionText == Constants.Requirments.Repair)
        {
            GetThisObject().SetActive(false);
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
            if (playerStatusData.food < 10)
            {
                GetLockButton().SetActive(true);
            }
            else
            {
                GetLockButton().SetActive(false);
            }
        }
    }
    public void ServiceDecision(DecisionObject obj)
    {
        if (obj.decisionText == Constants.Requirments.Repair)
        {
            GetLockButton().gameObject.SetActive(false);
            GetThisObject().SetActive(true);
        }
    }
    public GameObject GetLockButton()
    {
        return lockButton.gameObject;
    }
    public GameObject GetThisObject()
    {
        return this.gameObject;
    }
    void SetLockButton()
    {
        if (decisionText == Constants.Requirments.TakeCourse)
        {
            Debug.Log(name + " is locked");
        }
        if (decisionText == Constants.Requirments.ApplyJob)
        {
            Debug.Log(name + " is locked");
        }
        Debug.Log(name + " is locked");
    }
}