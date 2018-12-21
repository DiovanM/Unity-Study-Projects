using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public Dictionary<Vector2Int, GridBlock> blocks = new Dictionary<Vector2Int, GridBlock>();
    private void Start()
    {
        GetGrid();
    }

    private void GetGrid()
    {
        GridBlock[] blocks = FindObjectsOfType<GridBlock>();

        for (int i = 0; i < blocks.Length; i++)
        {
            if (this.blocks.ContainsKey(blocks[i].gridPos))
            {
                Debug.LogWarning("Block " + blocks[i].gridPos + " was overlapping, destroying it.");
                Destroy(blocks[i].gameObject);
            }
            else if(!blocks[i].blocked)
                this.blocks.Add(blocks[i].gridPos, blocks[i]);
        }

    }
     
}
