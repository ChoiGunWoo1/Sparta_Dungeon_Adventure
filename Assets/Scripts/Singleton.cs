using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour // ΩÃ±€≈Ê ªÛº”
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("error");
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null) {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
}
