using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private List<NeutralBlockBehaviour> neutralBlocks;
    private EnemySpawner enemySpawner;
    private TowerFactory towerFactory;
    private FriendlyBase friendlyBase;
    private CanvasHelper canvas;
    private Button startGame;
    private Button towerPlacement;
    private Button resetButton;

    private float currentBaseHealth;
    private int enemiesKilled; 
    private int enemiesDestroyed;
                               
    public enum GameState { PreGame, Tower_Placement, InGame, Game_Over, Game_Win };

    public GameState gameState = GameState.PreGame;

    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject fakeTowerPrefab;
    [SerializeField] private float baseLifePoints;
    [SerializeField] private int enemiesLimit;
    [SerializeField] private int towerLimit;
    [SerializeField] private float enemyDamage;
    [SerializeField] private float enemyBlockMovementTime;
    [SerializeField] private float pointsPerEnemy;


    private void Awake()
    {
        neutralBlocks = FindObjectsOfType<NeutralBlockBehaviour>().ToList();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        towerFactory = FindObjectOfType<TowerFactory>();
        friendlyBase = FindObjectOfType<FriendlyBase>();
        canvas = FindObjectOfType<CanvasHelper>();

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitialSetup();
        ButtonSetup();

        SceneManager.sceneLoaded += (Scene, LoadSceneMode) => { InitialSetup(); } ;
    }

    void ResetGame()
    {
        DestroyEnemies();
        DestroyTowers();
        InitialSetup();
    }

    void ButtonSetup()
    {
        canvas.reset.onClick.AddListener(ResetButton);
        canvas.startGame.onClick.AddListener(StartButton);
        canvas.towerPlacement.onClick.AddListener(TowerPlacementButton);
    }

    void DestroyEnemies()
    {
        var enemies = FindObjectsOfType<EnemyBehaviour>();
        foreach (EnemyBehaviour enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }

    void DestroyTowers()
    {
        var towers = FindObjectsOfType<TowerBehaviour>();
        foreach (TowerBehaviour tower in towers)
        {
            tower.myBlock.hasTower = false;
            Destroy(tower.gameObject);
        }
        towerFactory.towers.Clear();
    }

    void InitialSetup()
    {
        SetState(GameState.PreGame);
        towerFactory.Setup(towerPrefab, fakeTowerPrefab, towerLimit);
        enemySpawner.Setup(GameState.PreGame, enemiesLimit, enemyDamage, enemyBlockMovementTime);
        friendlyBase.Setup(GameState.PreGame, baseLifePoints);

        enemiesKilled = 0;
        enemiesDestroyed = 0;

        canvas.win.gameObject.SetActive(false);

        SetBaseHealth(baseLifePoints);
        ResetScore();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    SetState(GameState.Tower_Placement);
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    SetState(GameState.InGame);
        //}
    }

    public void SetState(GameState state)
    {
        gameState = state;
        enemySpawner.SetGameState(state);
        towerFactory.SetGameState(state);
        friendlyBase.SetGameState(state);

        foreach (NeutralBlockBehaviour neutralBlock in neutralBlocks)
        {
            neutralBlock.SetGameState(state, towerPrefab, fakeTowerPrefab);
        }

        if (state == GameState.Game_Over)
        {
            OnGameOver();
        }

    }

    public void SetBaseHealth(float health)
    {
        currentBaseHealth = health;
        if (currentBaseHealth <= 0) currentBaseHealth = 0;
        canvas.baseHealth.text = currentBaseHealth.ToString();
    }

    public void ResetScore()
    {
        enemiesKilled = 0;
        canvas.score.text = enemiesKilled.ToString();
    }

    public void IncreaseScore()
    {
        enemiesKilled++;
        canvas.score.text = (enemiesKilled * pointsPerEnemy).ToString();
    }

    public void IncreaseDestroyedEnemies()
    {
        enemiesDestroyed++;
        if (enemiesDestroyed == enemiesLimit && gameState == GameState.InGame) OnWin();
    }

    private void ResetButton()
    {
        ResetGame();
        canvas.towerPlacement.gameObject.SetActive(true);
        canvas.startGame.gameObject.SetActive(false);
        canvas.reset.gameObject.SetActive(false);
        canvas.win.gameObject.SetActive(false);
    }

    private void StartButton()
    {
        SetState(GameState.InGame);
        canvas.startGame.gameObject.SetActive(false);
        canvas.towerPlacement.gameObject.SetActive(false);
        canvas.reset.gameObject.SetActive(false);
    }

    private void TowerPlacementButton()
    {
        SetState(GameState.Tower_Placement);
        canvas.startGame.gameObject.SetActive(true);
        canvas.towerPlacement.gameObject.SetActive(false);
        canvas.reset.gameObject.SetActive(false);
    }

    private void OnWin()
    {
        SetState(GameState.Game_Win);
        Debug.Log("Game Win");
        canvas.win.gameObject.SetActive(true);
        canvas.reset.gameObject.SetActive(true);
        canvas.startGame.gameObject.SetActive(false);
        canvas.towerPlacement.gameObject.SetActive(false);
    }

    private void OnGameOver()
    {
        Debug.Log("Game Over");
        canvas.reset.gameObject.SetActive(true);
        canvas.startGame.gameObject.SetActive(false);
        canvas.towerPlacement.gameObject.SetActive(false);
    }

}
