using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptables/Wave")]
public class Wave : ScriptableObject {

    [SerializeField] private GameObject enemy;       
    [SerializeField] private bool overrideDefaultPath;
    [SerializeField] private Path path;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private int enemyAmount;
    [SerializeField] private float enemySpeed;

    public GameObject GetEnemyPrefab() { return enemy; }
    public bool GetOverrideDafultPath() { return overrideDefaultPath; }
    public Path GetPath() { return path; }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public int GetEnemyAmount() { return enemyAmount; }
    public float GetEnemySpeed() { return enemySpeed; }



}
