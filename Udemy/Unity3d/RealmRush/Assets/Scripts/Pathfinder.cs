using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    [SerializeField] private GridBlock startBlock;
    [SerializeField] private GridBlock endBlock;

    private Vector2Int[] directions = { Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down };
    private GridBlock currentBlock;
    private List<GridBlock> queue = new List<GridBlock>();
    private List<GridBlock> explored = new List<GridBlock>();
    private EnemySpawner enemySpawner;

    public Dictionary<Vector2Int, GridBlock> grid = new Dictionary<Vector2Int, GridBlock>();
    public List<GridBlock> path = new List<GridBlock>();

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();

        explored.Add(startBlock);
        GetGrid();
        currentBlock = startBlock;
        CalculateBlockValues();
        PathFind();
    }

    public void GetGrid()
    {
        GridBlock[] blocks = FindObjectsOfType<GridBlock>();

        for (int i = 0; i < blocks.Length; i++)
        {
            if (grid.ContainsKey(blocks[i].gridPos))
            {
                Debug.LogWarning("Block " + blocks[i].gridPos + " was overlapping, destroying it.");
                Destroy(blocks[i].gameObject);
            }

            if (blocks[i].start && startBlock == null)
            {
                startBlock = blocks[i];
            } else if (blocks[i].end && endBlock == null)
            {
                endBlock = blocks[i];
            }

            if (!blocks[i].blocked)
            {
                grid.Add(blocks[i].gridPos, blocks[i]);
            }

        }

        if (!endBlock || !startBlock)
        {
            throw new System.Exception("No Start or End Block");
        }

    }

    private void CalculateBlockValues()
    {
        foreach (KeyValuePair<Vector2Int, GridBlock> block in grid)
        {
            Vector2Int gVector = block.Value.gridPos - startBlock.gridPos;
            int gValue = Mathf.Abs(gVector.x) + Mathf.Abs(gVector.y);
            block.Value.gValue = gValue;

            Vector2Int hVector = block.Value.gridPos - endBlock.gridPos;
            int hValue = Mathf.Abs(hVector.x) + Mathf.Abs(hVector.y);
            block.Value.hValue = hValue;                              
        }
    }

    private void PathFind()
    {
        while (currentBlock != endBlock)
        {
            FindNeighbour();
        }

        while (currentBlock != startBlock)
        {
            path.Add(currentBlock);
            currentBlock = currentBlock.previousBlock;
        }

        path.Add(startBlock);
        path.Reverse();

        enemySpawner.path = path;

    }

    private void FindNeighbour()
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbour = currentBlock.gridPos + direction;
            if (grid.ContainsKey(neighbour) && !explored.Contains(grid[neighbour]))
            {
                explored.Add(grid[neighbour]);
                grid[neighbour].previousBlock = currentBlock;
                queue.Add(grid[neighbour]);
            }
        }

        foreach (GridBlock block in queue)
        {
            if (block.hValue < currentBlock.hValue)
            {
                currentBlock = block;
                queue.Remove(block);
                return;
            }
        }
        
        queue.OrderBy(o => o.hValue);
        currentBlock = queue[queue.Count-1];
        queue.Remove(queue[queue.Count - 1]);

    }

}
