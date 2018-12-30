using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBlock : MonoBehaviour
{
    private bool isWall;
    public Vector2Int gridPos;
    public bool blocked;
    public bool start;
    public bool end;

    public GridBlock previousBlock;

    public int gValue; //Distance to the start
    public int hValue; //Distance to the end

    public int fValue { get { return gValue + hValue; } }

    private void Awake()
    {
        gridPos = new Vector2Int((int)transform.position.x, (int)transform.position.z);
    }

}
