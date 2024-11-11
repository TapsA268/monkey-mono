using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameManager gameManager;

    public void ShowGameOverScreen(){
        gameOverPanel.SetActive(true);
    }

    public void RestartGame(){
        gameOverPanel.SetActive(false);
        gameManager.RestartLevel();
    }

}
