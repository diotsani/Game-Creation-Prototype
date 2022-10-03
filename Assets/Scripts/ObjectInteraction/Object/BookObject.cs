using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookObject : BaseObject
{
    protected override void Start()
    {
        _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.Path.Book);
        base.Start();
        
        OnClickSpecificDecision();
    }
    protected override void ObjectNeedRequirement(List<DecisionObject> decisionObjects)
    {
        foreach (var obj in decisionObjects)
        {
            bool set = !(playerStatusData.book <= 0);
            obj.GetThisObject().SetActive(set);
        }
    }
    void OnClickSpecificDecision()
    {
        for (int i = 0; i < _decisionObjects.Count; i++)
        {
            var obj = _decisionObjects[i];
            obj.button.onClick.AddListener(()=>SpecificDecision(obj));
        }
    }
    void SpecificDecision(DecisionObject obj)
    {
        if (obj.decisionText == Constants.Requirments.CheckBookShelf)
        {
            OnShowMonologueText(Constants.Monologue.BookStockMonolog(playerStatusData.book));
        }
    }

    public override void ResetAllDecision()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnDamagedObject()
    {
        throw new System.NotImplementedException();
    }

    protected override void CheckObjectState()
    {
        //throw new System.NotImplementedException();
    }
}
