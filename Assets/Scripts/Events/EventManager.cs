using System;
using System.Collections.Generic;
public delegate void EvtHandler(params object[] param);
public class EventManager : Singleton<EventManager>
{
    static Dictionary<int, EvtHandler> listeners = null;
    public bool IsPuase { get; set; }
    public override EventManager Init()
    {
        if (listeners == null) listeners = new Dictionary<int, EvtHandler>();
        IsPuase = false;
        return this;
    }
    public void AddListener(EventMsg evtId, EvtHandler handler)
    {
        AddListener((int)evtId, handler);
    }
    public void AddListener(int key, EvtHandler handler)
    {
        if (handler == null) return;
        if (!listeners.ContainsKey(key))
        {
            listeners.Add(key, handler);
            return;
        }
        listeners[key] += handler;
    }
    public void DispatchEvent(EventMsg evtId, params object[] args)
    {
        DispatchEvent((int)evtId, args);
    }
    public void DispatchEvent(int key, params object[] args)
    {
        if (IsPuase) return;
        EvtHandler evtHandler = null;
        if (listeners.TryGetValue(key, out evtHandler))
        {
            evtHandler(args);
        }
    }
    public void RemoveListener(EventMsg evtId, EvtHandler handler)
    {
        RemoveListener((int)evtId, handler);
    }
    public void RemoveListener(int key, EvtHandler handler)
    {
        if (handler != null)
        {
            EvtHandler evtHandler = null;
            if (listeners.TryGetValue(key, out evtHandler))
            {
                evtHandler -= handler;
                return;
            }
        }
        if (listeners.ContainsKey(key)) listeners.Remove(key);
    }
    public bool IsContains(EventMsg evtId)
    {
        return IsContains((int)evtId);
    }
    public bool IsContains(int key)
    {
        return listeners.ContainsKey(key);
    }
    public void Clear()
    {
        listeners.Clear();
    }
    public override void Dispose()
    {
        Clear();
        listeners = null;
    }
}
public interface IEventListener
{
    void HandleEvent(EventMsg evtId, params object[] args);
}