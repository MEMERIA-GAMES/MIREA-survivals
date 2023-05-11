using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class UI : MonoBehaviour
{
    public Canvas pauseCanvas;
    public Canvas menuCanvas;
    public Canvas gameOverCanvas;
    public Saver saver;

    public Character character;
    public AudioSource gameBGM;
    public TextMeshProUGUI coinsCollectedText;
    public TextMeshProUGUI coinsTotalText;
    public bool gameIsOn = false;

    // Start is called before the first frame update
    void Start()
    {
        menuCanvas.enabled = true;
        pauseCanvas.enabled = false;
        gameOverCanvas.enabled = false;
        gameBGM.mute = true;
        Time.timeScale = 0f;
        saver.loadData();
        coinsTotalText.text = "COINS: " + saver.getCoins().ToString();
}

    // Update is called once per frame
    void Update()
    {
        if (gameIsOn)
        {
            if (character.currentHP <= 0)
            {
                gameIsOn = false;
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
                }
            }
        }
    }

    public void Resume()
    {
        pauseCanvas.enabled = false;
        Time.timeScale = 1f;
        gameIsOn = true;
        gameBGM.mute = false;
    }

    public void ToMenu()
    {
        character.saveProgress();
        saver.saveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Play()
    {
        Time.timeScale = 1f;
        menuCanvas.enabled = false;
        gameIsOn = true;
        gameBGM.mute = false;
    }

    public void Exit()
    {
        character.saveProgress();
        saver.saveData();
        Application.Quit();
    }
}
