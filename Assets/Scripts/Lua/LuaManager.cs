using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XLua;

public class LuaManager : Singleton<LuaManager>
{
    // Lua 环境
    public LuaEnv luaEnv { get; private set; }
    // 初始化
    public override LuaManager Init()
    {
        if (luaEnv == null) luaEnv = new LuaEnv();
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
    // 添加LuaLoader
    // xlua 通过require加载lua脚本，默认只能加载Resources里面txt格式的lua文件
    // 这里扩展了xlua的自定义Loader，用来满足加载不同位置和格式以及特殊处理的lua文件
    // 自定义的loader方法必须返回byte[]数组
    public void AddLoader(LuaEnv.CustomLoader loader)
    {
        luaEnv.AddLoader(loader);
    }
    // 执行Lua 脚本
    public void DoString(string chunk)
    {
        luaEnv.DoString(chunk);
    }
    // 销毁Lua
    public override void Dispose()
    {
        luaEnv.Dispose();
        luaEnv = null;
        base.Dispose();
    }
}
