using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

    [SerializeField] private float secondsToLoad = 0.5f;   

    public void LoadLevel()
    {
        Time.timeScale = 0.5f;
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        yield return new WaitForSecondsRealtime(secondsToLoad);
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        Destroy(FindObjectOfType<ScenePersist>().gameObject);
        SceneManager.LoadScene(nextScene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LoadLevel();
    }

}
