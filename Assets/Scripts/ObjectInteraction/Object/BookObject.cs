using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookObject : BaseObject
{
    protected override void Start()
    {
        _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.GetData.Book);
        base.Start();
        OnClickSellBook();
    }

    void OnClickSellBook()
    {
        for (int i = 0; i < _decisionObjects.Count; i++)
        {
            var obj = _decisionObjects[i];
            obj.button.onClick.AddListener(() => SetSellBook(obj));
        }
    }

    void SetSellBook(DecisionObject obj)
    {
        if (obj.decisionText == Constants.Requirments.Sell)
        {
            Debug.Log(obj.name);
        }
        if (playerStatusData.books <= 0)
        {
            obj.GetThisObject().SetActive(false);
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
