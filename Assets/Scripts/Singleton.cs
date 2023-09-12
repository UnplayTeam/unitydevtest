using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wrapper for a singleton 
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<T>();
        }
        else
            Destroy(this.gameObject);
    }


    public static T Instance
    {
        get
        {
            if (instance == null)
                print("Instance of GameObject does not exist!");

            return instance;
        }
    }
}
