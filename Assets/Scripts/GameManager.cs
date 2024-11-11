using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameOverScreen gameOverScreen;

    public void LoadNextLevel(){
        int currentSceneIndex=SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex=currentSceneIndex+1;
        if (nextSceneIndex<SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No hay más niveles");
        }
    }

    public void RestartLevel(){
        int currentSceneIndex=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void HandleLoss(){
        gameOverScreen.ShowGameOverScreen();
    }
}
