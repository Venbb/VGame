using UnityEngine;
using XLua;
using System.Collections.Generic;
using System.Collections;
using System;

public class Coroutine_Runner : MonoBehaviour
{
    private void Start() {
        // StopCoroutine()
    }
}

public class IEnumeratorHolder : XLua.Cast.Any<IEnumerator>
{
    public IEnumeratorHolder(IEnumerator i) : base(i)
    {
    }
}

public static class CoroutineConfig
{
    [LuaCallCSharp]
    public static List<Type> LuaCallCSharp
    {
        get
        {
            return new List<Type>()
            {
                typeof(WaitForSeconds),
                typeof(WWW)
            };
        }
    }
    [CSharpCallLua]
    public static List<Type> CSharpCallLua
    {
        get
        {
            return new List<Type>()
            {
                typeof(System.Collections.IEnumerator)
            };
        }
    }
}
