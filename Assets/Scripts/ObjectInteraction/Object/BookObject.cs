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
    }
    protected override void ObjectNeedRequirement(List<DecisionObject> decisionObjects)
    {
        foreach (var obj in decisionObjects)
        {
            bool set = !(playerStatusData.books <= 0);
            obj.GetThisObject().SetActive(set);
            // if (playerStatusData.books <= 0)
            // {
            //     obj.GetThisObject().SetActive(false);
            // }
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
