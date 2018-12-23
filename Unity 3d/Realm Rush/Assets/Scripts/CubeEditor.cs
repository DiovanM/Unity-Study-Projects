using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[ExecuteInEditMode]              
public class CubeEditor : MonoBehaviour
{
    public Vector3 snapPos;

    void Update()
    {
        snapPos.x = Mathf.RoundToInt(transform.position.x);
        snapPos.y = 0;
        snapPos.z = Mathf.RoundToInt(transform.position.z);

        transform.position = snapPos;

    }

}
