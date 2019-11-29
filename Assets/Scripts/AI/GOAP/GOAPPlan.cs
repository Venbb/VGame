using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPPlan
{
    private List<GOAPAction> m_Actions = new List<GOAPAction>();
    public int curStep { get; private set; }
    public bool isFinished { get { return curStep < m_Actions.Count == false; } }
    public GOAPAction curAction { get { return isFinished ? null : m_Actions[curStep]; } }
    public void AddAction(GOAPAction action)
    {
        m_Actions.Add(action);
    }
    public bool IsCurActComplete()
    {
        return isFinished ? true : m_Actions[curStep].IsComplete();
    }
}
