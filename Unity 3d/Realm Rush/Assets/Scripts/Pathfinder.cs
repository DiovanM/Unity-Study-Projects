using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    [SerializeField] private Grid grid;

    [SerializeField] private GridBlock startBlock;
    [SerializeField] private GridBlock endBlock;
    private Vector2Int[] directions = { Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down };

    private GridBlock currentBlock;

    private void Start()
    {
        SetStartEndColor();
        currentBlock = startBlock;
        Invoke("CalculateBlockValues", 0.5f);
    }

    private void SetStartEndColor()
    {
        startBlock.gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        endBlock.gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void CalculateBlockValues()
    {
        foreach (KeyValuePair<Vector2Int, GridBlock> block in grid.blocks)
        {
            Vector2Int gVector = block.Value.gridPos - startBlock.gridPos;
            int gValue = Mathf.Abs(gVector.x) + Mathf.Abs(gVector.y);
            block.Value.gValue = gValue;

            Vector2Int hVector = block.Value.gridPos - endBlock.gridPos;
            int hValue = Mathf.Abs(hVector.x) + Mathf.Abs(hVector.y);
            block.Value.hValue = hValue;
        }

        FindNeighbour();
    }

    private void FindNeighbour()
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbour = currentBlock.gridPos + direction;
            if (grid.blocks.ContainsKey(neighbour))
            {
                grid.blocks[neighbour].SetColor();
            }
        }
    }
    

}
