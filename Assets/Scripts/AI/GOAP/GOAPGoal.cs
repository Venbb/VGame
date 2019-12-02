using System.Collections.Generic;
public abstract class GOAPGoal
{
    public GOAPAgent agent { get; private set; }
    public GOAPPlan curPlan { get; private set; }
    public bool isActive { get; private set; }
    public GOAPGoal(GOAPAgent agent)
    {
        this.agent = agent;
    }
    public virtual void Activate(GOAPPlan plan)
    {
        curPlan = plan;
    }

    public virtual bool Update()
    {
        if (curPlan == null) return false;
        curPlan.Update();
        return true;
    }
    public virtual void Reset()
    {

    }
    public virtual void Deactivate()
    {

    }
}
