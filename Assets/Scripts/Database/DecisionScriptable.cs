using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DecisionData", menuName = "Database/DecisionData")]
public class DecisionScriptable : ScriptableObject
{
    public List<DecisionStruct> decisionList = new List<DecisionStruct>();
}