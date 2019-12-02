using System.Collections.Generic;
public class GOAPManager
{
    public GOAPAgent agent { get; }
    private List<GOAPGoal> mGoals = new List<GOAPGoal>();
    public GOAPGoal curGoal { get; private set; }
    public GOAPManager(GOAPAgent agent)
    {
        this.agent = agent;
    }
    public void Update()
    {
        if (curGoal == null) return;
        if (curGoal.Update())
        {

        }
        else
        {
            curGoal.Deactivate();
            curGoal = null;
        }
    }
    private void FindGoal()
    {
        
    }
    public GOAPPlan GetPlan(GOAPGoal goal)
    {
        return null;
    }
    public void AddGoal(GOAPGoal goal)
    {
        if (!mGoals.Contains(goal)) mGoals.Add(goal);
    }
    public void Reset()
    {
        if (curGoal != null)
        {
            curGoal.Deactivate();
            curGoal = null;
        }
        for (int i = 0; i < mGoals.Count; i++)
            mGoals[i].Reset();
    }
}
