using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonEffect : MonoBehaviour
{
    public static SingletonEffect instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
}
