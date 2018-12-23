using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private List<NeutralBlockBehaviour> neutralBlocks;
    private EnemySpawner enemySpawner;
    private TowerFactory towerFactory;

    public enum GameState { PreGame, Tower_Placement, InGame, Game_Over, Game_Win };

    public GameState gameState = GameState.PreGame;

    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject fakeTowerPrefab;
    [SerializeField] private int enemiesLimit;
    [SerializeField] private int towerLimit;

    private void Awake()
    {
        neutralBlocks = FindObjectsOfType<NeutralBlockBehaviour>().ToList();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        towerFactory = FindObjectOfType<TowerFactory>();

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitialSetup();

        SceneManager.sceneLoaded += (Scene, LoadSceneMode) => { InitialSetup(); } ;
    }

    void InitialSetup()
    {
        SetState(GameState.PreGame);
        towerFactory.Setup(towerPrefab, fakeTowerPrefab, towerLimit);
        enemySpawner.Setup(GameState.PreGame, enemiesLimit);
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
        enemySpawner.SetGameState(state);
        towerFactory.SetGameState(state);

        foreach (NeutralBlockBehaviour neutralBlock in neutralBlocks)
        {
            neutralBlock.SetGameState(state, towerPrefab, fakeTowerPrefab);
        }
    }

}
