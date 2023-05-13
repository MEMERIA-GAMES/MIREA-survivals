using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public Saver saver;
    public TextMeshProUGUI coinsTotalText;
    public TextMeshProUGUI weaponAttackText;
    public TextMeshProUGUI weaponcostText;
    public List<GameObject> characters;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpgradeWeapon()
    {
        int weaponLvl = saver.getWeaponLvl();
        saver.removeCoins(weaponLvl * 100);
        saver.upgradeWeapon();
        coinsTotalText.text = "лнмерш: " + saver.getCoins().ToString();
    }

    void DowngradeWeapon()
    {
        int weaponLvl = saver.getWeaponLvl();
        saver.addCoins(weaponLvl * 100);
        saver.downgradeWeapon();
        coinsTotalText.text = "лнмерш: " + saver.getCoins().ToString();
    }
}
