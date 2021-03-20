using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton;
    [SerializeField] private float delay;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject winScreen;
    private void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;
        loseScreen.SetActive(false);
        //вин скрин сет актив фалсе
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("main menu");
    }
    public void ExitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
             Application.Quit();
#endif
    }

    public void WinGame()
    {
        
        winScreen.SetActive(true);
    }

    public void LoseGame()
    {
        StartCoroutine(ShowWithDelay(delay));
    }

    private IEnumerator ShowWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        loseScreen.SetActive(true);

    } 
}
