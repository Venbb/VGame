using System.Collections.Generic;
public abstract class GOAPGoal
{
    public virtual void Execute(GOAPAgent agent)
    {

    }

    public virtual bool Update()
    {
        return true;
    }
    public virtual void Reset()
    {

    }
    public virtual void Stop()
    {

    }
}
