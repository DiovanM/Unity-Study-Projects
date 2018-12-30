using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void StartGame()
    {
        FindObjectOfType<GameManager>().ResetLifePoints();
        FindObjectOfType<GameManager>().ResetScore();
        SceneManager.LoadScene(1);
    }

}
