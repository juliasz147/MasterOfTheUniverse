using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOrLoseScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnGoToLevel2ButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        //GameSettings.Instance.numberOfEnemyKilled = 0;
    }

    public void RestartLevel1ButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        //GameSettings.Instance.numberOfEnemyKilled = 0;
    }

    public void OnGoToLevel3ButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }

    public void RestartLevel2ButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void RestartLevel3ButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }

    public void StartGameButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void RestartLevel4ButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(11);
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }
}
