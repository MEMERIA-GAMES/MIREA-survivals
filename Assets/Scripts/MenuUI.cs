using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MenuUI : MonoBehaviour
{
    public Saver saver;
    public TextMeshProUGUI coinsTotalText;
    public List<string> gameSceneName = new List<string>(){"Level1", "Level2", "Level3"};
    public Canvas menuCanvas;
    public Canvas shopCanvas;
    public Canvas levelSelectCanvas;

    // Start is called before the first frame update
    void Start()
    {
       Debug.Log("Menu is loading...");
       saver.loadData();
       coinsTotalText.text = "COINS: " + saver.getCoins().ToString();
       menuCanvas.enabled = true;
       shopCanvas.enabled = false;
       levelSelectCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLevel1()
    {
        SceneManager.LoadScene(gameSceneName[0]);
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene(gameSceneName[1]);
    }


    public void PlayLevel3()
    {
        Debug.Log("Третьего уровня не существует. Это все выдумки. Оставь эту затею.");
        //SceneManager.LoadScene(gameSceneName[0]);
    }


    public void Exit()
    {
        saver.saveData();
        Application.Quit();
    }

    public void ToShop()
    {
        menuCanvas.enabled = false;
        levelSelectCanvas.enabled = false;
        shopCanvas.enabled = true;
    }

    public void ToMenu()
    {
        menuCanvas.enabled = true;
        shopCanvas.enabled = false;
        levelSelectCanvas.enabled = false;
        saver.saveData();
    }

    public void ToLevelSelections()
    {
        menuCanvas.enabled = false;
        shopCanvas.enabled = false;
        levelSelectCanvas.enabled = true;
    }
}
