using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour {    

    private void Awake()
    {                                                                    
        int scenePersists = FindObjectsOfType<ScenePersist>().Length;
        if (scenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
            DontDestroyOnLoad(gameObject);
    }   

}
