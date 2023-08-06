using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunctions : MonoBehaviour
{

    /// <summary>
    /// Script for resume button on pause menu
    /// </summary>
    public void resume()
    {
        // if game is paused
        if (gameManager.instance.isPaused)
        {
            // toggle bool
            gameManager.instance.isPaused = !gameManager.instance.isPaused;

            //Unpause actions
            gameManager.instance.cursorUnlockUnpause();
        }
    }

    public void playGame()
    {
        SceneManager.LoadScene("Test", LoadSceneMode.Single);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }

    public void restart()
    {
        gameManager.instance.cursorUnlockUnpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // load whatever scene we are currently in (basically reloads)
    }


    public void quit()
    {
        Application.Quit();
    }
}
