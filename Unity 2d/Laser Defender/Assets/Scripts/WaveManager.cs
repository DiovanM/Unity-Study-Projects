using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    [SerializeField] List<Wave> waves;
    [SerializeField] private bool loop = true;
    
    private int currentWave = 0;

	// Use this for initialization
	IEnumerator Start () {
        while (loop)
        {
            yield return StartCoroutine(SpawnAllWaves(currentWave));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator SpawnAllWaves(int waveIndex)
    {
       foreach(Wave waves in waves)
        {
            yield return StartCoroutine(SpawnWave(waveIndex));
            waveIndex++;
        }
    }

    private IEnumerator SpawnWave(int waveIndex)
    {                           
        GameObject enemy = waves[waveIndex].GetEnemyPrefab();
        bool overrideDefaultPath = waves[waveIndex].GetOverrideDafultPath();
        Path path = waves[waveIndex].GetPath();
        float timeBetweenSpawns = waves[waveIndex].GetTimeBetweenSpawns();
        int enemyAmount = waves[waveIndex].GetEnemyAmount();
        float enemySpeed = waves[waveIndex].GetEnemySpeed();

        int currentEnemy = 0;


        while (currentEnemy < enemyAmount)
        {
            var curEnemy = Instantiate(enemy) as GameObject;
            if (overrideDefaultPath) { curEnemy.GetComponent<EnemyBehaviour>().SetPath(path); }
            curEnemy.GetComponent<EnemyBehaviour>().SetSpeed(enemySpeed);
            curEnemy.GetComponent<EnemyBehaviour>().CanMove();
            currentEnemy++;
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        yield return null;
    }     

}
