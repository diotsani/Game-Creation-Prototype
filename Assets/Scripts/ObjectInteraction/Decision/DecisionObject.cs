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
    [SerializeField] private int actionCost;
    private PlayerStatusData playerStatusData = PlayerStatusData.instance;

    private void Start()
    {
        //playerStatusData = PlayerStatusData.instance;
    }

    public void Init(string GetName, int GetSkillCost, int GetStressCost,int GetHealthCost, int GetMoneyCost,int GetBookCost, int GetFoodCost, int GetActionCost)
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
        actionCost = GetActionCost;
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
        playerStatusData.ActionCost(actionCost);
        
        baseObject.AddAmountClick();
        baseObject.GetDecisionParent(true); // need false when clicked
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
        var positiveMoney = Mathf.Abs(moneyCost);
        var message = $"Not Enough Money, You need {positiveMoney} money to unlock this decision";
        if (decisionText == Constants.Requirments.Repair)
        {
            Debug.Log(message);
        }
    }
    
}