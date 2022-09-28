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
    public int moneyCost;
    [SerializeField] private int bookCost;
    [SerializeField] private int foodCost;
    private PlayerStatusData playerStatusData = PlayerStatusData.instance;

    private void Start()
    {
        //playerStatusData = PlayerStatusData.instance;
    }

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
    public void OnClick(DecisionObject decisionObject, BaseObject baseObject)
    {
        decisionObject.button.onClick.RemoveAllListeners();
        decisionObject.button.onClick.AddListener(()=> DecisionEffect(baseObject));
    }
    void DecisionEffect(BaseObject baseObject)
    {
        OnClickInteracted?.Invoke();
        
        playerStatusData.SkillCost(skillCost);
        playerStatusData.StressCost(stressCost);
        playerStatusData.HealthCost(healthCost);
        playerStatusData.MoneyCost(moneyCost);
        playerStatusData.BookCost(bookCost);
        playerStatusData.FoodCost(foodCost);
        
        baseObject.AddAmountClick();
        baseObject.GetDecisionParent(true); // need false when clicked
    }
    void SetSellDecision(GameObject obj,BaseObject baseObject)
    {
        var PositiveValue = Mathf.Abs(moneyCost);
        if (PositiveValue >= playerStatusData.money)
        {
            playerStatusData.MoneyCost(moneyCost);
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
        //DoorRequirement();
        //RefrigeratorRequirement();
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
        if (decisionText == Constants.Requirments.Jogging)
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
        if (decisionText == Constants.Requirments.BuyFood)
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
    public void SetLockButton()
    { 
        lockButton.onClick.RemoveAllListeners();
        lockButton.onClick.AddListener(OnClickLockButton);
    }
    void OnClickLockButton()
    {
        Debug.Log(name + " Lock");
    }
    void CheckMoneyCost()
    {
        var PositiveValue = Mathf.Abs(moneyCost);
        Debug.Log(PositiveValue);

        if (playerStatusData.money < PositiveValue)
        {
            Debug.Log("Not enough money");
        }
    }
}