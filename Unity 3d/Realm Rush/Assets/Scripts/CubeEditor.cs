using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[ExecuteInEditMode]              
public class CubeEditor : MonoBehaviour
{

    private TextMesh label;
    public Vector3 snapPos;

    private void Start()
    {
        label = GetComponentInChildren<TextMesh>();
    }

    void Update()
    {
        snapPos.x = Mathf.RoundToInt(transform.position.x);
        snapPos.y = 0;
        snapPos.z = Mathf.RoundToInt(transform.position.z);

        transform.position = snapPos;

        label.text = snapPos.x + "," + snapPos.z;
    }

}
