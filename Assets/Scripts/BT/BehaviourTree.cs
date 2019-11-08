using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BTState
{
    Failure,
    Success,
    Running
}
public abstract class BTNode
{
    public BTState state { get; protected set; }
    public abstract BTState Handle();
}