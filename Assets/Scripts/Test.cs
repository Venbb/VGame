using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), ""))
        {
            LuaManager.Instance.DoString("print('????????????')");
        }
    }
}
