using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : BaseObject
{
    public override void Start()
    {
        decisionScriptable = Resources.Load<DecisionScriptable>(Constants.GetData.Door);
        base.Start();
    }
}
