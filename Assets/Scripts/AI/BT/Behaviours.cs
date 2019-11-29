using System.Collections;
using System.Collections.Generic;

public enum BehaviourState
{
    Failure,
    Success,
    Running
}
// 节点基类
public abstract class BTBehaviour
{
    public BehaviourState mState { get; protected set; }
    public abstract BehaviourState Handle();
}
// 复合节点，包括Sequence，Selector，Parallel节点
public abstract class CompositeBehaviour : BTBehaviour
{
    // 当前执行节点索引
    public int childIndex { get; protected set; }
    // 所有的子节点
    protected List<BTBehaviour> children = new List<BTBehaviour>();
    // 添加子节点
    public virtual CompositeBehaviour AddChild(params BTBehaviour[] args)
    {
        for (int i = 0; i < args.Length; i++)
            children.Add(args[i]);
        return this;
    }
    // 重置子节点
    public virtual void Reset()
    {
        childIndex = 0;
        CompositeBehaviour node = null;
        for (int i = 0; i < children.Count; i++)
        {
            node = children[i] as CompositeBehaviour;
            if (node != null) node.Reset();
        }
    }
    public virtual void Clear()
    {
        childIndex = 0;
        CompositeBehaviour node = null;
        for (int i = 0; i < children.Count; i++)
        {
            node = children[i] as CompositeBehaviour;
            if (node != null) node.Clear();
        }
        children.Clear();
    }
}
// Sequence 串行的AND
// Sequence 类似于编程语言中的"&&"符号，它从左到右，每帧只执行一个子节点。
// 1、如果当前子节点返回Running，那么Sequence也返回Running。下一帧继续执行当前这个子节点。
// 2、如果当前子节点返回失败，那么Sequence节点本身返回失败。
// 3、如果当前子节点返回成功，如果还有下一个子节点，那么Sequence本身返回Running，下一帧会切换到下一个子节点； 如果所有子节点都完毕了，则Sequence节点返回成功，整个节点结束。
public abstract class SequenceBehaviour : CompositeBehaviour
{
    public override BehaviourState Handle()
    {
        mState = children[childIndex].Handle();
        switch (mState)
        {
            case BehaviourState.Success:
                childIndex++;
                if (childIndex == children.Count)
                {
                    childIndex = 0;
                    return BehaviourState.Success;
                }
                mState = BehaviourState.Running;
                return mState;
            case BehaviourState.Failure:
                childIndex = 0;
                return BehaviourState.Failure;
            case BehaviourState.Running:
                return BehaviourState.Running;
        }
        throw new System.Exception("what's wrong with you ?" + GetType().Namespace);
    }
}
// Selector 串行的OR
// Selector与Sequence执行顺序相同，逻辑正巧是“||”的逻辑。它也是从左到右，每帧只执行一个子节点。
// 1、如果当前子节点返回Running，那么Selector也返回Running。下一帧继续执行当前这个子节点。
// 2、如果当前子节点返回失败，那么Selector节点本身返回Running，下一帧执行下一个子节点；如果所有子节点都失败了，就返回失败。
// 3、如果当前子节点返回成功，那么Selector返回成功。
public abstract class SelectorBehaviour : CompositeBehaviour
{
    public override BehaviourState Handle()
    {
        mState = children[childIndex].Handle();
        switch (mState)
        {
            case BehaviourState.Success:
                childIndex = 0;
                return BehaviourState.Success;
            case BehaviourState.Failure:
                childIndex++;
                if (childIndex == children.Count)
                {
                    childIndex = 0;
                    return BehaviourState.Failure;
                }
                mState = BehaviourState.Running;
                return mState;
            case BehaviourState.Running:
                return BehaviourState.Running;
        }
        throw new System.Exception("what's wrong with you ?" + GetType().Namespace);
    }
}
// Parallel 并行的AND
// Parallel 返回值是 “&&” 逻辑。与Sequence的区别是，在每一帧，它都执行所有子节点一次~~。
// 1、所有子节点都Running，那么Parallel节点也返回Running。
// 2、有任何一个节点返回失败，那么Parallel立刻结束，返回失败。还处于Running的子节点也会终止（正在Running的被假设为失败）。
// 3、有任何一个节点返回成功，那么该子节点下一帧就不会被调用了，但是Parallel本身仍然返回Running，直到所有子节点都返回成功，Parallel才返回成功
public abstract class ParallelSequenceBehaviour : CompositeBehaviour
{
    public override BehaviourState Handle()
    {
        mState = BehaviourState.Success;
        for (int i = 0; i < children.Count; i++)
        {
            mState = children[i].mState;
            if (mState != BehaviourState.Success)
                mState = children[i].Handle();
            switch (mState)
            {
                case BehaviourState.Success:
                    childIndex++;
                    break;
                case BehaviourState.Failure:
                    return BehaviourState.Failure;
            }
        }
        mState = childIndex != children.Count ? BehaviourState.Running : BehaviourState.Success;
        childIndex = 0;
        return mState;
    }
}
// Parallel Selector 并行的OR
// Parallel Selector 返回值是 “||” 逻辑。它是并行的，每一帧执行所有子节点一次~~。
// 1、所有子节点都Running，那么Parallel Selector节点也返回Running。
// 2、有任何一个节点返回失败，那么Parallel Selector 本身返回Running，直到所有子节点都失败了，它才返回失败。
// 3、有任何一个节点返回成功，Parallel Selector 直接返回成功。
public abstract class ParallelSelectorBehaviour : CompositeBehaviour
{
    public override BehaviourState Handle()
    {
        mState = BehaviourState.Failure;
        for (int i = 0; i < children.Count; i++)
        {
            mState = children[i].Handle();
            switch (mState)
            {
                case BehaviourState.Success:
                    return BehaviourState.Success;
                case BehaviourState.Failure:
                    childIndex++;
                    break;
            }
        }
        mState = childIndex != children.Count ? BehaviourState.Running : BehaviourState.Failure;
        childIndex = 0;
        return mState;
    }
}
// 装饰节点
public abstract class DecoratorBehaviour : BTBehaviour
{
    protected BTBehaviour child;
    public DecoratorBehaviour SetChild(BTBehaviour child)
    {
        this.child = child;
        return this;
    }
    public override BehaviourState Handle()
    {
        if (child == null) mState = BehaviourState.Success;
        return mState;
    }
}
// 条件节点
public abstract class ConditinalBehaviour : BTBehaviour
{
    System.Func<bool> callBack;
    public ConditinalBehaviour(System.Func<bool> callBack)
    {
        this.callBack = callBack;
    }
    public override BehaviourState Handle()
    {
        return callBack() ? BehaviourState.Success : BehaviourState.Failure;
    }
}
// 行为节点，执行结果（调用方法或协程）
public abstract class ActionBehaviour : BTBehaviour
{
    System.Action callBack;
    public ActionBehaviour(System.Action callBack)
    {
        this.callBack = callBack;
    }
    public override BehaviourState Handle()
    {
        if (callBack != null) callBack();
        return BehaviourState.Success;
    }
}