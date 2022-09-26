using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCondition : MonoBehaviour
{
    public GameConditions gameConditions;
}
public enum GameConditions
{
    None,
    Thief,
    PowerFailure,
    Rain,
    Football
}
