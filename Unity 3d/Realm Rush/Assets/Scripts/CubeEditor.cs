using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(GridBlock))]
[ExecuteInEditMode]              
public class CubeEditor : MonoBehaviour
{

    private GridBlock gridBlock;
    private TextMesh label;
    public Vector3 snapPos;

    private void Start()
    {
        label = GetComponentInChildren<TextMesh>();
        gridBlock = GetComponent<GridBlock>();
    }

    void Update()
    {
        snapPos.x = Mathf.RoundToInt(transform.position.x);
        snapPos.y = 0;
        snapPos.z = Mathf.RoundToInt(transform.position.z);

        transform.position = snapPos;

        label.text = snapPos.x + "," + snapPos.z;

        if (gridBlock.blocked)
        {
            SetBlockedColor(Color.black);
        }
        else
        {
            SetBlockedColor(Color.white);
        }

    }

    private void SetBlockedColor(Color color)
    {
      //  transform.GetChild(1).transform.GetComponent<MeshRenderer>().material.color = color;
        transform.GetChild(1).transform.GetComponent<MeshRenderer>().sharedMaterial.color = color;
    }

}
