using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [HideInInspector] public GameManager.GameState gameState;

    [SerializeField] private GameObject enemyPrefab;
    public float spawnRate = 5f;
    public bool spawning = false;

    public List<GridBlock> path = new List<GridBlock>();

    private void Start()
    {
        StartCoroutine(StartSpawning());
    }

    public IEnumerator StartSpawning()
    {
        while (true)
        {
            while (gameState == GameManager.GameState.InGame)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnRate);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, path[0].transform.position, Quaternion.identity);
        enemy.transform.SetParent(GameObject.Find("EnemiesParent").transform);
        enemy.GetComponent<EnemyBehaviour>().StartMovement(path);
    }

    void Update()
    {
        
    }

}
