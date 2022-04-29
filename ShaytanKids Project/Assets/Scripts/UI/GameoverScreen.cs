using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverScreen : MonoBehaviour
{
    static string previousSceneName; // string to load the last scene by name from.
                                     // (static vars are constant between scenes.)

    public void GameOverSceneSwitch()
    {
        previousSceneName = SceneManager.GetActiveScene().name;
        //Debug.Log("Scene name is " + previousSceneName);

        SceneManager.LoadScene("deathScene");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(previousSceneName);
        // the respawn function is called in the CheckpointManager's Start function.
    }

}
