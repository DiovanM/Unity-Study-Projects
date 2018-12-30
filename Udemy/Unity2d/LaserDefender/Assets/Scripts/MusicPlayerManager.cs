using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerManager : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
