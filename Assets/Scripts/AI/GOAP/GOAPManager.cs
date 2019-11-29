using System.Collections.Generic;
public class GOAPManager
{
    public GOAPAgent agent { get; }
    List<GOAPGoal> mGoals = new List<GOAPGoal>();
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
            StopCurGoal();
    }
    public GOAPGoal AddGoal()
    {
        return null;
    }
    public void Reset()
    {
        StopCurGoal();
        for (int i = 0; i < mGoals.Count; i++)
            mGoals[i].Reset();
    }
    private void StopCurGoal()
    {
        if (curGoal != null)
        {
            curGoal.Stop();
            curGoal = null;
        }
    }
}
