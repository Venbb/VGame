using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ICustomEvt
{
    int type { get; set; }
    object[] Params { get; set; }

    string ToString();
}
public class CustomEvt : ICustomEvt
{
    public int type { get; set; }
    public object[] Params { get; set; }
    public CustomEvt() { }
    public CustomEvt(int type, params object[] param)
    {
        this.type = type;
        this.Params = param;
    }
    public override string ToString()
    {
        string arg = null;
        if (Params != null)
        {
            for (int i = 0; i < Params.Length; i++)
            {
                if ((Params.Length > 1 && Params.Length - 1 == i) || Params.Length == 1)
                {
                    arg += Params[i];
                }
                else
                {
                    arg += Params[i] + " , ";
                }
            }
        }

        return type + " [ " + ((arg == null) ? "null" : arg.ToString()) + " ] ";
    }
}
// public delegate void CustomEvtHandler(params object[] param);
public delegate void Action<T>();
public interface IEventManager
{
    bool IsPuase { get; set; }
    // void AddListener(int type, CustomEvtHandler handler);
    void AddListener<T>(int type, Action<T> handler);
    // void RemoveListener(int type, CustomEvtHandler handler);
    void RemoveListener<T>(int type, Action<T> handler);
    void Dispatch<T>(int type, T arg);
    void Dispatch(int type, params object[] param);
    void Clear();
}
public class EventManager : Singleton<EventManager>, IEventManager
{
    readonly Dictionary<int, Delegate> listeners = new Dictionary<int, Delegate>();

    public bool IsPuase { get; set; }

    // public void AddListener(int type, CustomEvtHandler handler)
    // {
    //     AddListener(type, handler);
    // }

    public void AddListener<T>(int type, Action<T> handler)
    {
        AddListener(type, handler);
    }
    void AddListener(int type, Delegate handler)
    {
        if (handler == null) return;
        Delegate evtHandler = null;
        listeners.TryGetValue(type, out evtHandler);
        listeners[type] = Delegate.Combine(evtHandler, handler);
    }
    public void Clear()
    {
        listeners.Clear();
    }

    public void Dispatch<T>(int type, T arg)
    {
        Delegate evtHandler = null;
        if (listeners.TryGetValue(type, out evtHandler))
        {
            evtHandler.DynamicInvoke(arg);
        }
    }

    public void Dispatch(int type, params object[] args)
    {
        Delegate evtHandler = null;
        if (listeners.TryGetValue(type, out evtHandler))
        {
            evtHandler.DynamicInvoke(args);
        }
    }

    // public void RemoveListener(int type, CustomEvtHandler handler)
    // {
    //     if (handler == null) return;
    //     Delegate evtHandler = null;
    //     if (listeners.TryGetValue(type, out evtHandler))
    //     {
    //         listeners[type] = Delegate.Remove(evtHandler, handler);
    //     }
    // }

    public void RemoveListener<T>(int type, Action<T> handler)
    {
           if (handler == null) return;
        Delegate evtHandler = null;
        if (listeners.TryGetValue(type, out evtHandler))
        {
            listeners[type] = Delegate.Remove(evtHandler, handler);
        }
    }
}