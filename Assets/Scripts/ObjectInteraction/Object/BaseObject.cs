using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
public enum ObjectState
{
    Good,
    Damaged
}
public abstract class BaseObject : MonoBehaviour
{
    public delegate void EventName(string monologue);
    public static event EventName OnShowMonologue;
    
    public ObjectState _objectState;
    
    [Header("Config")]
    [SerializeField] protected int amountClicked;
    [SerializeField] protected int dailyClicks;
    [SerializeField] protected int consecutiveDay;
    
    [Header("Dependency")]
    [SerializeField] protected DayManager dayManager;
    [SerializeField] protected GameFlow gameFlow;
    [SerializeField] protected GameObject decisionParent;
    [SerializeField] protected GameObject objectInteract;
    [Header("Prefab")]
    [SerializeField] protected DecisionObject decisionPrefabs;
    
    protected Vector3 _getPosition;
    
    [SerializeField] protected List<DecisionObject> _decisionObjects;
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
            var getAction = _decisionScriptable.decisionList[index].actionCost;
            
            decisionParent.transform.position = pos;
            var decisionButton = Instantiate(decisionPrefabs, decisionParent.transform);
            _decisionObjects.Add(decisionButton.GetComponent<DecisionObject>());
            decisionButton.Init(getNameDecision,getSkill,getStress,getHealth,getMoney,getBook,getFood,getAction);
            
            decisionButton.OnClick(decisionButton,this);
            //obj.button.onClick.AddListener(AddAmountClick);
        }
        ObjectNeedRequirement(_decisionObjects);
    }

    protected virtual void Update()
    {
        if(_isClicked)
        {
            ObjectNeedRequirement(_decisionObjects);
            _isClicked = false;
        }
    }

    public void AddAmountClick()
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
    public void GetDecisionParent(bool active)
    {
        objectInteract.SetActive(active);
        decisionParent.SetActive(active);
    }

    protected virtual void ObjectNeedRequirement(List<DecisionObject> decisionObjects)
    {
        
    }
    public virtual void ResetConsecutiveDay()
    {
        consecutiveDay = 0;
    }
    public abstract void ResetAllDecision();
    protected abstract void OnDamagedObject();

    protected virtual void CheckObjectState()
    {
        
    }
    protected virtual void OnShowMonologueText(string monologue)
    {
        OnShowMonologue?.Invoke(monologue);
    }
    public void DeactivateDecisionButton()
    {
        foreach (var objs in _decisionObjects)
        {
            objs.button.interactable = false;
        }
    }
}
