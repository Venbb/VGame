using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LuaManager : Singleton<LuaManager>
{
    public LuaEnv luaEnv { get; private set; }
    public void Init()
    {
        if (luaEnv == null) luaEnv = new LuaEnv();
    }
    public LuaTable CreateTable()
    {
        LuaTable table = luaEnv.NewTable();

        // 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        table.SetMetaTable(meta);
        meta.Dispose();

        return table;
    }
    public void DoString(string chunk)
    {
        luaEnv.DoString(chunk);
    }
    public void Dispose()
    {
        luaEnv.Dispose();

        luaEnv = null;
    }
}
