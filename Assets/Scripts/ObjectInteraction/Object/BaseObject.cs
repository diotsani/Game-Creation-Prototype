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
    
    public ObjectState _objectState;
    
    [Header("Config")]
    [SerializeField] protected int amountClicked;
    [SerializeField] protected int dailyClicks;
    
    [Header("Dependency")]
    [SerializeField] protected DayManager dayManager;
    [SerializeField] protected GameObject decisionParent;
    [Header("Prefab")]
    [SerializeField] protected DecisionObject decisionPrefabs;
    
    protected Vector3 _getPosition;
    
    protected List<DecisionObject> _decisionObjects;
    protected DecisionScriptable _decisionScriptable;
    public bool _isClicked;
    public bool _isDamaged;

    protected PlayerStatusData playerStatusData;
    protected virtual void OnEnable()
    {
        GameFlow.OnChangeState += CheckObjectState;
        GameFlow.OnResetClick += ResetDailyClicks;
    }

    protected virtual void OnDisable()
    {
       GameFlow.OnChangeState -= CheckObjectState;
       GameFlow.OnResetClick -= ResetDailyClicks;
    }

    protected virtual void Start()
    {
        playerStatusData = PlayerStatusData.instance;
        _decisionObjects = new List<DecisionObject>();
        
        decisionPrefabs = Resources.Load<DecisionObject>("Prefabs/DecisionButton");
        _getPosition = new Vector3(0, 1, 0);
        var pos = Camera.main.WorldToScreenPoint(transform.position + _getPosition);
        for (int i = 0; i < _decisionScriptable.decisionList.Count; i++)
        {
            var index = i;
            var getNameDecision = _decisionScriptable.decisionList[index].decision;
            var getSkill = _decisionScriptable.decisionList[index].skillCost;
            var getStress = _decisionScriptable.decisionList[index].stressCost;
            var getHealth = _decisionScriptable.decisionList[index].healthCost;
            var getMoney = _decisionScriptable.decisionList[index].moneyCost;
            var getBook = _decisionScriptable.decisionList[index].bookCost;
            var getFood = _decisionScriptable.decisionList[index].foodCost;
            
            decisionParent.transform.position = pos;
            var obj = Instantiate(decisionPrefabs, decisionParent.transform);
            _decisionObjects.Add(obj.GetComponent<DecisionObject>());
            obj.Init(getNameDecision,getSkill,getStress,getHealth,getMoney,getBook,getFood);
            
            obj.GetComponent<Button>().onClick.RemoveAllListeners();
            obj.OnClick(obj,decisionParent,this);
            obj.GetComponent<Button>().onClick.AddListener(AddAmountClick);
        }
        ObjectNeedRequirement(_decisionObjects);
        SetRequirment();
    }

    protected virtual void Update()
    {
        if(_isClicked)
        {
            ObjectNeedRequirement(_decisionObjects);
            SetRequirment();
            _isClicked = false;
        }
    }

    protected virtual void AddAmountClick()
    {
        amountClicked++;
        dailyClicks++;
        
    }
    public void ResetAmountClick()
    {
        amountClicked = 0;
    }
    public void ResetDailyClicks()
    {
        dailyClicks = 0;
    }
    public void SetRequirment()
    {
        for (int i = 0; i < _decisionObjects.Count; i++)
        {
            _decisionObjects[i].SetRequirementDecision();
        }
    }

    protected virtual void ObjectNeedRequirement(List<DecisionObject> obj)
    {
        
    }
    public abstract void ResetAllDecision();
    protected abstract void OnDamagedObject();

    protected virtual void CheckObjectState()
    {
        
    }
}
