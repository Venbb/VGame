using System;
using System.Collections.Generic;
using XLua;
public static class XLuaGenConfig
{
    [CSharpCallLua]
    public static List<Type> CSharpCallLua = new List<Type>() {
                typeof(EvtHandler),
                typeof(LuaEnv.CustomLoader)
            };
}