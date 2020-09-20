using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : Menu
{


    public void Retry()
    {
        SceneManager.LoadScene(nextGameScene);

    }
}
