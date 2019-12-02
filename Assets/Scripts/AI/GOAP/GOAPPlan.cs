using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPPlan
{
    private List<GOAPAction> m_Actions = new List<GOAPAction>();
    public int curStep { get; private set; }
    public bool isFinished { get { return curStep < m_Actions.Count == false; } }
    public GOAPAction curAction { get; private set; }
    public void AddAction(GOAPAction action)
    {
        m_Actions.Add(action);
    }
    public bool Activate()
    {
        curStep = 0;
        return Execute();
    }
    public void Update()
    {
        if (curAction != null)
        {
            curAction.Update();
            if (curAction.IsComplete())
            {
                curAction.Deactivate();
                curStep++;
                Execute();
            }
        }
    }
    public bool Execute()
    {
        curAction = isFinished ? null : m_Actions[curStep];
        if (curAction != null)
        {
            if (!curAction.CheckPreconditions()) return false;
            curAction.Activate();
        }
        return true;
    }
    public void Deactivate()
    {
        if (curAction != null) curAction.Deactivate();
        m_Actions.Clear();
        curStep = 0;
    }
}
