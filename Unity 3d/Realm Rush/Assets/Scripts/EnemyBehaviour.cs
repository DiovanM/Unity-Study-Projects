using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    private List<GridBlock> path = new List<GridBlock>();

    private Vector2Int gridPos;

    public float timeToMoveToBlock = 2f;

    private void Awake()
    {
        gridPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }

    public void StartMovement(List<GridBlock> path)
    {
        this.path = path;

        StartCoroutine(Move(1));

    }

    private IEnumerator Move(int blockIndex)
    {
        StopCoroutine(Move(blockIndex));
        if (blockIndex == path.Count)
        {
            Debug.Log("Reached End");
            Destroy(gameObject);
            yield break;
        }

        var startPos = transform.position;
        float lerpTime = 0;

        while (lerpTime < timeToMoveToBlock)
        {
            lerpTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, path[blockIndex].transform.position, lerpTime/timeToMoveToBlock);
            transform.LookAt(path[blockIndex].transform);
            yield return null;
        }

        gridPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        blockIndex++;

        StartCoroutine(Move(blockIndex));
    }

}
