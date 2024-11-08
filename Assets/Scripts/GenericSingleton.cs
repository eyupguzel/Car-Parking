using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : GenericSingleton<T>
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T)) as T;
                if (instance == null)
                {
                    Debug.Log("instance is null");
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("1");
            instance = this as T;
            DontDestroyOnLoad(instance);
        }
        else if (instance != this)
        {
            Debug.Log("2");

            Destroy(instance);
            return;
        }
    }
}