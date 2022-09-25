using UnityEngine;

public class HandphoneObject : BaseObject
{
    public override void Start()
    {
        decisionScriptable = Resources.Load<DecisionScriptable>(Constants.GetData.Handphone);
        base.Start();
    }
}