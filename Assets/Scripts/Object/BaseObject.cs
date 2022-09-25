using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public abstract class BaseObject : MonoBehaviour
{
    
    public ObjectState objectState;
    [SerializeField] private int amountClicked;
    [SerializeField] private int dailyClicks;
    [SerializeField] private DayManager dayManager;
    [SerializeField] private DecisionObject decisionPrefabs;
    [SerializeField] private GameObject decisionParent;
    [SerializeField] private Vector3 getPosition;
    public List<DecisionObject> decisionObjects;
    public DecisionScriptable decisionScriptable;

    private void OnEnable()
    {
        GameFlow.OnChangeState += CheckObjectState;
        GameFlow.OnResetClick += ResetDailyClicks;
    }

    private void OnDisable()
    {
       GameFlow.OnChangeState -= CheckObjectState;
       GameFlow.OnResetClick += ResetDailyClicks;
    }

    public virtual void Start()
    {
        decisionPrefabs = Resources.Load<DecisionObject>("Prefabs/DecisionButton");
        getPosition = new Vector3(0, 1, 0);
        var pos = Camera.main.WorldToScreenPoint(transform.position + getPosition);
        for (int i = 0; i < decisionScriptable.decisionList.Count; i++)
        {
            var index = i;
            var getNameDecision = decisionScriptable.decisionList[index].decision;
            var getSkill = decisionScriptable.decisionList[index].skillCost;
            var getStress = decisionScriptable.decisionList[index].stressCost;
            var getHealth = decisionScriptable.decisionList[index].healthCost;
            var getMoney = decisionScriptable.decisionList[index].moneyCost;
            var getFood = decisionScriptable.decisionList[index].foodCost;
            
            decisionParent.transform.position = pos;
            var obj = Instantiate(decisionPrefabs, decisionParent.transform);
            decisionObjects.Add(obj.GetComponent<DecisionObject>());
            obj.Init(getNameDecision,getSkill,getStress,getHealth,getMoney,getFood);
            
            obj.GetComponent<Button>().onClick.RemoveAllListeners();
            obj.OnClick(obj,decisionParent,this);
            obj.GetComponent<Button>().onClick.AddListener(AddAmountClick);
        }
        SetRequirment();
    }
    void AddAmountClick()
    {
        SetRequirment();
        amountClicked++;
        dailyClicks++;
    }
    public virtual void ResetDailyClicks()
    {
        dailyClicks = 0;
    }
    public virtual void SetRequirment()
    {
        for (int i = 0; i < decisionObjects.Count; i++)
        {
            decisionObjects[i].SetRequirementDecision();
        }
    }

    public virtual void OnCheckObject()
    {
        
    }
    public virtual void CheckObjectState()
    {
        if(dayManager.amountDay == 1)
        {
            objectState = ObjectState.VeryGood;
        }
        else if(dayManager.amountDay == 2 && amountClicked == 0)
        {
            objectState = ObjectState.Good;
        }
        else if(dayManager.amountDay == 3 && amountClicked <= 1)
        {
            objectState = ObjectState.LessGood;
        }
        else if(dayManager.amountDay == 6 && amountClicked <= 4)
        {
            objectState = ObjectState.NotGood;
        }
    }
}
