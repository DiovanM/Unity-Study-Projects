using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[ExecuteInEditMode]              
public class CubeEditor : MonoBehaviour
{

    [SerializeField] private float gridSize = 10f;

    private TextMesh label;

    private void Start()
    {
        label = GetComponentInChildren<TextMesh>();
    }

    void Update()
    {
        Vector3 snapPos;
        snapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        snapPos.y = Mathf.RoundToInt(transform.position.y / gridSize) * gridSize;
        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;

        transform.position = snapPos;

        label.text = snapPos.x + "," + snapPos.y + "," + snapPos.z;
    }

}
