using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupScenePersistance : MonoBehaviour
{
    public static SetupScenePersistance instance;

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
