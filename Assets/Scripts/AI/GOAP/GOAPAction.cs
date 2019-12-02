
public abstract class GOAPAction
{
    public abstract void Update();
    public virtual bool IsComplete() { return false; }
    public virtual bool CheckPreconditions() { return true; }
    public virtual void Activate() { }
    public virtual void Deactivate() { }
}
