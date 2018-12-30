using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{

    [HideInInspector] public GameManager.GameState gameState = GameManager.GameState.PreGame;

    public GameObject towerPrefab;
    public GameObject fakeTowerPrefab;

    [HideInInspector] public Queue<TowerBehaviour> towers = new Queue<TowerBehaviour>();
    private GameObject fakeTower;
    private int towerLimit;

    public void Setup(GameObject tower, GameObject fakeTower, int limit)
    {
        towerPrefab = tower;
        fakeTowerPrefab = fakeTower;
        towerLimit = limit;
    }

    public void SetGameState(GameManager.GameState state)
    {
        gameState = state;
        if (fakeTower) fakeTower.SetActive(false);
    }

    public void AddTower(NeutralBlockBehaviour block)
    {
        if (towers.Count < towerLimit)
        {
            GameObject tower = Instantiate(towerPrefab, block.transform.position, Quaternion.identity);
            tower.GetComponent<TowerBehaviour>().myBlock = block;
            towers.Enqueue(tower.GetComponent<TowerBehaviour>());
            tower.transform.SetParent(GameObject.Find("TowersParent").transform);
        }
        else
        {
            GameObject tower = towers.Dequeue().gameObject;
            tower.GetComponent<TowerBehaviour>().myBlock.hasTower = false;
            towers.Enqueue(tower.GetComponent<TowerBehaviour>());
            tower.GetComponent<TowerBehaviour>().myBlock = block;
            tower.transform.position = block.transform.position;
        }
    }

    public void AddFakeTower(NeutralBlockBehaviour block)
    {
        if (!fakeTower)
        {
            fakeTower = Instantiate(fakeTowerPrefab, block.transform.position, Quaternion.identity);
            fakeTower.transform.SetParent(gameObject.transform);
        }
        else
        {
            fakeTower.transform.position = block.transform.position;
            fakeTower.SetActive(true);
        }
    }

    public void RemoveFakeTower(NeutralBlockBehaviour block)
    {
        if (fakeTower)
        {
            fakeTower.SetActive(false);
        }
    }
}
