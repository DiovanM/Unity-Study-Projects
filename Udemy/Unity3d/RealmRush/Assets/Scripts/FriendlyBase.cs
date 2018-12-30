using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBase : MonoBehaviour
{
    private GameManager.GameState gameState;

    [HideInInspector] public float lifePoints = 50;
    
    public void Setup(GameManager.GameState state, float life)
    {
        SetGameState(state);
        lifePoints = life;
    }

    public void SetGameState(GameManager.GameState state)
    {
        gameState = state;
    }

    public void TakeDamage(float damage)
    {
        lifePoints -= damage;
        FindObjectOfType<GameManager>().SetBaseHealth(lifePoints);
        if (lifePoints <= 0)
        {
            if (gameState == GameManager.GameState.InGame) Lose();
        }
    }

    public void Lose()
    {
        FindObjectOfType<GameManager>().SetState(GameManager.GameState.Game_Over);
    }

}
