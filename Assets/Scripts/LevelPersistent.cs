using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPersistent : MonoBehaviour
{


    void Awake()
    {
        int NumLevelPersist = FindObjectsOfType<LevelPersistent>().Length;
        if (NumLevelPersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetLevelPersist()
    {
        Destroy(gameObject);
    }
}
