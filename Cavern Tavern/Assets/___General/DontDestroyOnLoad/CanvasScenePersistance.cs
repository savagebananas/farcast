using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScenePersistance : MonoBehaviour
{
    public static CanvasScenePersistance instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
