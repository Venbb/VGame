using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    void Awake()
    {
        LuaManager.Instance.Init().DoString("require 'GameStart'");
        EventManager.Instance.AddListener((int)CommEvt.COMM_001,OnCustomEvt);
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnGUI()
    {
        if(GUI.Button(new Rect(10,10,100,50),""))
        {
            EventManager.Instance.DispatchEvent((int)CommEvt.COMM_001, "kkkkkkkk????",123);
        }
    }
    void OnCustomEvt(int type,params object[] param)
    {
        // Debug.Log("GameStart OnCustomEvt" + evt.type + evt.Params);
    }
    // Update is called once per frame
    void Update()
    {
        LuaManager.Instance.luaEnv.Tick();
    }
}
