using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralBlockBehaviour : MonoBehaviour
{

    [HideInInspector] public GameManager.GameState gameState;
    [HideInInspector] public GameObject towerPrefab;
    [HideInInspector] public GameObject fakeTowerPrefab;
    [HideInInspector] public bool hasTower = false;
    private GameObject fakeTower;

    private void OnMouseDown()
    {
        if (gameState == GameManager.GameState.Tower_Placement)
        {
            if (!hasTower)
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
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
                FindObjectOfType<TowerFactory>().AddFakeTower(this);
            }
        }
    }

    private void OnMouseExit()
    {
        if (gameState == GameManager.GameState.Tower_Placement)
        {
            if (!hasTower)
            {
                FindObjectOfType<TowerFactory>().RemoveFakeTower(this);
            }
        }
    }

    public void SetGameState(GameManager.GameState state, GameObject towerPrefab, GameObject fakeTowerPrefab)
    {
        gameState = state;
        this.towerPrefab = towerPrefab;
        this.fakeTowerPrefab = fakeTowerPrefab;
    }

}
