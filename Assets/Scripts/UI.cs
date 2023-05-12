using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class UI : MonoBehaviour
{
    public Canvas pauseCanvas;
    public Canvas gameOverCanvas;
    public Saver saver;

    public Character character;
    public AudioSource gameBGM;
    public TextMeshProUGUI coinsCollectedText;
    public string gameScene = "Game";
    public string menuScene = "Menu";

    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.enabled = false;
        gameOverCanvas.enabled = false;
        gameBGM.mute = false;
        saver.loadData();
}

    // Update is called once per frame
    void Update()
    {
        if (character.currentHP <= 0)
        {
            Time.timeScale = 0f;
            gameOverCanvas.enabled = true;
            gameBGM.mute = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseCanvas.enabled = !pauseCanvas.enabled;
            if (pauseCanvas.enabled)
            {
                Time.timeScale = 0f;
                gameBGM.mute = true;
            }
            else
            {
                Time.timeScale = 1f;
                gameBGM.mute = false;
            }
        }
    }

    public void Resume()
    {
        pauseCanvas.enabled = false;
        gameBGM.mute = false;
        Time.timeScale = 1f;
    }

    public void ToMenu()
    {
        character.saveProgress();
        saver.saveData();
        SceneManager.LoadScene(menuScene);
    }

    public void Restart()
    {
        character.saveProgress();
        saver.saveData();
        SceneManager.LoadScene(gameScene);
    }

    public void Exit()
    {
        character.saveProgress();
        saver.saveData();
        Application.Quit();
    }
}
