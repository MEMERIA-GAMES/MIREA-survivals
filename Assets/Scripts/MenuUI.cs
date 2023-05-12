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
    public string gameScene = "Game";

    // Start is called before the first frame update
    void Start()
    {
       Debug.Log("Menu is loading...");
       saver.loadData();
       coinsTotalText.text = "COINS: " + saver.getCoins().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void Exit()
    {
        saver.saveData();
        Application.Quit();
    }
}
