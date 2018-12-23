using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    [SerializeField] float shotInterval = 3f;
    [SerializeField] float towerRange = 3f;
    [SerializeField] Transform shotHolder;
    [SerializeField] GameObject shotParticle;

    public EnemyBehaviour[] enemies;
    private GameObject top;
    private bool isAnyEnemyClose = false;

    private void Awake()
    {
        isAnyEnemyClose = false;
        top = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        StartCoroutine(StartShooting());
    }

    void FixedUpdate()
    {
        enemies = FindObjectsOfType<EnemyBehaviour>();

        if (enemies.Length > 0)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (Vector3.Distance(enemies[i].transform.position, transform.position) < towerRange)
                {
                    isAnyEnemyClose = true;
                    break;
                }
                else
                {
                    isAnyEnemyClose = false;
                }

            }

            if (isAnyEnemyClose)
                top.transform.LookAt(GetClosestEnemy(enemies).transform);
        }
        else
        {
            isAnyEnemyClose = false;
            top.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    EnemyBehaviour GetClosestEnemy(EnemyBehaviour[] enemies)
    {
        EnemyBehaviour bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (EnemyBehaviour potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

    private IEnumerator StartShooting()
    {
        while (true)
        {
            if (isAnyEnemyClose)
            {
                Shoot();
            }
            yield return new WaitForSeconds(shotInterval);
        }
    }

    private void Shoot()
    {
        GameObject shot = Instantiate(shotParticle, shotHolder.position, Quaternion.identity);
        shot.GetComponent<TowerShot>().OnSpawn(GetClosestEnemy(enemies).transform);
    }

}
