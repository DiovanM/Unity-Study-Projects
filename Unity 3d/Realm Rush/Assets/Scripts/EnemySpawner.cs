using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [HideInInspector] public GameManager.GameState gameState;

    [SerializeField] private GameObject enemyPrefab;
    private int enemiesSpawned = 0;
    private int enemiesLimit;
    public float spawnRate = 5f;
    public bool spawning = false;

    public List<GridBlock> path = new List<GridBlock>();

    private void Start()
    {
        StartCoroutine(StartSpawning());
    }

    public void Setup(GameManager.GameState initialState, int enemiesLimit)
    {
        SetGameState(initialState);
        this.enemiesLimit = enemiesLimit;
    }

    public void SetGameState(GameManager.GameState state)
    {
        gameState = state;
        if (state == GameManager.GameState.InGame)
        {
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
