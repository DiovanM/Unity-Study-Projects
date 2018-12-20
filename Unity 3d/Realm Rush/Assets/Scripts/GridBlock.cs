using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBlock : MonoBehaviour
{

    private bool isWall;
    public bool tracked;

    public GridBlock previousBlock;

    public int gValue;
    public int hValue;

    public int fValue { get { return gValue + hValue; } }
    

}
