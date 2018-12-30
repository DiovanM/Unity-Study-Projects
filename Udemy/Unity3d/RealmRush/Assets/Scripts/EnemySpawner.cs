using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [HideInInspector] public GameManager.GameState gameState;

    [SerializeField] private GameObject enemyPrefab;
    private int enemiesSpawned = 0;
    private int enemiesLimit;
    private float enemyDamage;
    private float enemyBlockMovementTime;
    public float spawnRate = 5f;
    public bool spawning = false;

    public List<GridBlock> path = new List<GridBlock>();

    public void Setup(GameManager.GameState initialState, int enemiesLimit, float enemyDamage, float enemyBlockMovementTime)
    {
        this.enemiesLimit = enemiesLimit;
        this.enemyDamage = enemyDamage;
        this.enemyBlockMovementTime = enemyBlockMovementTime;
    }

    public void SetGameState(GameManager.GameState state)
    {
        gameState = state;
        if (state == GameManager.GameState.InGame)
        {
            enemiesSpawned = 0;
            StartCoroutine(StartSpawning());
        }
    }

    public IEnumerator StartSpawning()
    {
        while (gameState == GameManager.GameState.InGame && enemiesSpawned < enemiesLimit)
        {
            SpawnEnemy();
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnRate);
        }

        StopCoroutine(StartSpawning());
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, path[0].transform.position, Quaternion.identity);
        enemy.transform.SetParent(GameObject.Find("EnemiesParent").transform);
        enemy.GetComponent<EnemyBehaviour>().StartMovement(path, enemyDamage, enemyBlockMovementTime);
    }

    void Update()
    {
        
    }

}
