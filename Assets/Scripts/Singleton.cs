using UnityEngine;

public abstract class Singleton<T> where T : class, new()//使用关键字 new() 限定，必须含有无参构造函数的单例 
{
    protected static T instance;
    // 用于lock块的对象,使用 双重锁确保单例在多线程初始化时的线程安全性
    private static readonly object _synclock = new object();
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (_synclock)
                {
                    instance = new T();
                    Debug.Log("创建了单例对象:" + typeof(T).Name);
                }
            }
            return instance;
        }
    }
    public virtual T Init() { return instance; }
}
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<T>();
            if (instance == null)
            {
                var gameObject = new GameObject(typeof(T).Name);
                instance = gameObject.AddComponent<T>();
                DontDestroyOnLoad(gameObject);
            }
            return instance;
        }
    }
    protected virtual void OnDestroy()
    {
        instance = null;
    }
}