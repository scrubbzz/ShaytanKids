using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverScreen : MonoBehaviour
{
    Scene prevGameScene;
    
    void Update()
    {
        // if (restartButtonClicked) { RestartLevel ]

        // else if click main menu, load main menu etc.
    }

    public void GameOverSceneSwitch()
    {
        DontDestroyOnLoad(gameObject);
        prevGameScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene("game over scene");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(prevGameScene.name);

        CheckpointManager.player = GameObject.FindGameObjectWithTag("Player");
        CheckpointManager.Respawn();
    }

}
