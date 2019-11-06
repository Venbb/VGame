using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    void Awake()
    {
        EventManager.Instance.Init();
        LuaManager.Instance.Init().DoString("require 'GameStart'");

        EventManager.Instance.AddListener(EventMsg.COMM_001, OnCustomEvt1);
        EventManager.Instance.AddListener(EventMsg.COMM_002, OnCustomEvt2);
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Dispatch"))
        {
            EventManager.Instance.DispatchEvent(EventMsg.COMM_001);
        }
        if (GUI.Button(new Rect(10, 100, 100, 50), "Remoce"))
        {
            // Debug.Log(EventManager.GetCount(EventMsg.COMM_001));
            EventManager.Instance.RemoveListener(EventMsg.COMM_001, OnCustomEvt1);
            // Debug.Log(EventManager.GetCount(EventMsg.COMM_001));
        }
    }
    void OnCustomEvt1(object[] args)
    {
        Debug.Log("GameStart OnCustomEvt1" + args.Length);
    }
    void OnCustomEvt2(object[] args)
    {
        Debug.Log("GameStart OnCustomEvt2");
    }
    // Update is called once per frame
    void Update()
    {
        // LuaManager.Instance.luaEnv.Tick();
    }
    void OnApplicationQuit()
    {
        EventManager.Instance.Dispose();
        LuaManager.Instance.Dispose();
    }
}
