using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(GridBlock))]
[ExecuteInEditMode]              
public class CubeEditor : MonoBehaviour
{

    private GridBlock myGridBlock;

    private TextMesh label;
    public Vector3 snapPos;

    private void Start()
    {
        label = GetComponentInChildren<TextMesh>();
        myGridBlock = GetComponent<GridBlock>();
    }

    void Update()
    {
        snapPos.x = Mathf.RoundToInt(transform.position.x / myGridBlock.GetGridSize()) * myGridBlock.GetGridSize();
        snapPos.y = 0;
        snapPos.z = Mathf.RoundToInt(transform.position.z / myGridBlock.GetGridSize()) * myGridBlock.GetGridSize();

        transform.position = snapPos;

        label.text = snapPos.x + "," + snapPos.z;
    }

}
