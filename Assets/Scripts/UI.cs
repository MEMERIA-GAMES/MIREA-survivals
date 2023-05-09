using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UI : MonoBehaviour
{
    public Canvas pauseCanvas;
    public Canvas menuCanvas;
    public Canvas gameOverCanvas;
    public Character character;
    public bool gameIsOn = false;
    public AudioSource gameBGM;

    // Start is called before the first frame update
    void Start()
    {
        menuCanvas.enabled = true;
        pauseCanvas.enabled = false;
        gameOverCanvas.enabled = false;
        gameBGM.mute = true;
        Time.timeScale = 0f;
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

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMenu()
    {
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
        Application.Quit();
    }
}
