using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    public float spawnRate = 5f;
    public bool spawning = false;

    public List<GridBlock> path = new List<GridBlock>();
        
    public IEnumerator StartSpawning()
    {
        while (spawning)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnRate);
        }
        
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, path[0].transform.position, Quaternion.identity);
        enemy.GetComponent<EnemyBehaviour>().StartMovement(path);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !spawning)
        {
            spawning = true;
            StartCoroutine(StartSpawning());
        }else if (Input.GetKeyDown(KeyCode.S) && spawning)
        {
            spawning = false;
            StopAllCoroutines();
        }
    }
}
