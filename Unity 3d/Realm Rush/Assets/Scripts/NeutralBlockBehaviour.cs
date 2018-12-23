using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralBlockBehaviour : MonoBehaviour
{

    [HideInInspector] public GameManager.GameState gameState;
    [HideInInspector] public GameObject towerPrefab;
    [HideInInspector] public GameObject fakeTowerPrefab;
    private GameObject fakeTower;
    private bool hasTower = false;

    private void OnMouseDown()
    {
        if (gameState == GameManager.GameState.Tower_Placement)
        {
            if (!hasTower)
            {
                GameObject tower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
                tower.transform.SetParent(GameObject.Find("TowersParent").transform);
                hasTower = true;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (gameState == GameManager.GameState.Tower_Placement)
        {
            if (!hasTower)
            {
                fakeTower = Instantiate(fakeTowerPrefab, transform.position, Quaternion.identity);
                fakeTower.transform.SetParent(gameObject.transform);
            }
        }
    }

    private void OnMouseExit()
    {
        if (gameState == GameManager.GameState.Tower_Placement)
        {
            if (!hasTower)
            {
                Destroy(fakeTower);
            }
        }
    }

    public void SetGameState(GameManager.GameState state, GameObject towerPrefab, GameObject fakeTowerPrefab)
    {
        gameState = state;
        this.towerPrefab = towerPrefab;
        this.fakeTowerPrefab = fakeTowerPrefab;

        if (fakeTower) Destroy(fakeTower);
    }

}
