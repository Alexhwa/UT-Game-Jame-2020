using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string nextGameScene;

    public void PlayGame()
    {
        SceneManager.LoadScene(nextGameScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quit successful.");
        Application.Quit();
    }
}
