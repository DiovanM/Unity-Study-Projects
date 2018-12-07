using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] private int lives = 5;
    [SerializeField] private Text livesText;
    [SerializeField] private Text scoreText;

    private int initialLives; 
    private int score = 0;

    private void Awake()
    {
        initialLives = lives;
        int gameManagers = FindObjectsOfType<GameManager>().Length;
        if (gameManagers > 1)
        {
            Destroy(gameObject);
        }
        else
            DontDestroyOnLoad(gameObject);

        livesText.text = lives.ToString();
        scoreText.text = score.ToString();
    }                              
	
	public void OnPlayerDeath()
    {
        lives--;
        livesText.text = lives.ToString();
        if (lives <= 0)
        {
            SceneManager.LoadScene(5);
            ResetLifePoints(); 
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }            
    }

    public void ResetLifePoints()
    {
        lives = initialLives;
        livesText.text = lives.ToString();
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

}
