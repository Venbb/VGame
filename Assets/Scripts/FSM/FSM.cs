using System.Collections.Generic;
namespace FSM
{
    public enum Transition
    {
        NULL,
    }
    public enum StateID
    {
        NULL,
    }
    public abstract class FSMState
    {
        public StateID id { get; protected set; }
        protected Dictionary<Transition, StateID> states = new Dictionary<Transition, StateID>();
        protected FSMController ctrl;
        public FSMState(FSMController ctrl)
        {
            this.ctrl = ctrl;
        }
        public void Add(Transition tran, StateID id)
        {
            if (tran != Transition.NULL && id != StateID.NULL)
            {
                if (!states.ContainsKey(tran)) states.Add(tran, id);
            }
        }
        public void Remove(Transition tran)
        {
            if (states.ContainsKey(tran)) states.Remove(tran);
        }
        public StateID GetState(Transition tran)
        {
            if (states.ContainsKey(tran)) return states[tran];
            return StateID.NULL;
        }
        public abstract void Enter(params object[] args);
        public abstract void Handle(params object[] args);
        public abstract void CheckState();
        public abstract void Exit();
    }
    public class FSMController
    {
        Dictionary<StateID, FSMState> states = new Dictionary<StateID, FSMState>();
        FSMState curSate;
        public void Update(params object[] args)
        {
            if (curSate != null) curSate.Handle(args);
        }
        public void SetTransition(Transition tran, params object[] args)
        {
            if (tran == Transition.NULL || curSate == null) return;
            StateID id = curSate.GetState(tran);
            if (!states.ContainsKey(id)) return;
            curSate.Exit();
            curSate = states[id];
            curSate.Enter(args);
        }
        public void AddState(params FSMState[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (!states.ContainsKey(args[i].id)) states.Add(args[i].id, args[i]);
            }
        }
        public void RemoveSate(FSMState state)
        {
            if (states.ContainsKey(state.id)) states.Remove(state.id);
        }
    }
}
