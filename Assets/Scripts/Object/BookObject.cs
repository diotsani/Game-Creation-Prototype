using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookObject : BaseObject
{
    public override void Start()
    {
        decisionScriptable = Resources.Load<DecisionScriptable>(Constants.GetData.Book);
        base.Start();
    }
}
