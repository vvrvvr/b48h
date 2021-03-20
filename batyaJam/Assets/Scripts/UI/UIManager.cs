using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton;
    private void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;
        
    }

    public void RestartGame()
    {

    }
    public void MainMenu()
    {

    }
    public void ExitGame()
    {

    }

    public void WinGame()
    {

    }

}
