using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private List<NeutralBlockBehaviour> neutralBlocks;
    private EnemySpawner enemySpawner;

    public enum GameState { PreGame, Tower_Placement, InGame, Game_Over, Game_Win };

    public GameState gameState = GameState.PreGame;
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject fakeTowerPrefab;

    private void Awake()
    {
        neutralBlocks = FindObjectsOfType<NeutralBlockBehaviour>().ToList();
        enemySpawner = FindObjectOfType<EnemySpawner>();

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SetState(GameState.PreGame);

        SceneManager.sceneLoaded += (Scene, LoadSceneMode) => { SetState(GameState.PreGame); } ;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetState(GameState.Tower_Placement);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SetState(GameState.InGame);
        }
    }

    public void SetState(GameState state)
    {
        gameState = state;

        enemySpawner.gameState = state;

        foreach (NeutralBlockBehaviour neutralBlock in neutralBlocks)
        {
            neutralBlock.SetGameState(state, towerPrefab, fakeTowerPrefab);
        }
    }

}
