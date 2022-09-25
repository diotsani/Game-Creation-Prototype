using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeratorObject : BaseObject
{
    public override void Start()
    {
        decisionScriptable = Resources.Load<DecisionScriptable>(Constants.GetData.Refrigerator);
        base.Start();
    }
}
