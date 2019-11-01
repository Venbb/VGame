using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LuaManager : Singleton<LuaManager>
{
    // Lua 环境
    public LuaEnv luaEnv { get; private set; }
    // 是否初始化
    public bool isInit { get; private set; }
    // 初始化
    public LuaManager Init()
    {
        if (luaEnv == null) luaEnv = new LuaEnv();
        isInit = true;
        return this;
    }
    // 创建一个LuaTable
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
    // 执行Lua 脚本
    public void DoString(string chunk)
    {
        luaEnv.DoString(chunk);
    }
    // 销毁Lua
    public void Dispose()
    {
        luaEnv.Dispose();
        luaEnv = null;
        isInit = false;
    }
}
