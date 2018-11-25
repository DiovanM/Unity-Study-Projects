using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    [SerializeField] private float delayToGameOver = 2f;

   public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }	

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadGameOverCR());
    }

    public IEnumerator LoadGameOverCR()
    {
        yield return new WaitForSeconds(delayToGameOver);
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
