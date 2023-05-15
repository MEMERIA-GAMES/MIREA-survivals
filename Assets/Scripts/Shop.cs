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
    public TextMeshProUGUI weaponCostText;
    public TextMeshProUGUI characterCostText;
    public TextMeshProUGUI characterStatsText;
    public Image characterImage;
    public Button upgradeWeaponButton;
    public Button downgradeWeaponButton;
    public Button buyCharacterButton;
    public List<Sprite> characterSprites;
    public int currentCharacterId;
    public int currentCharacterCost;
    public List<int> purchasedCharacterIds;
    public int weaponLvl;
    public int weaponCost;
    public int coins;

    // Start is called before the first frame update
    void Start()
    {
        saver.loadData();
        currentCharacterId = saver.getSelectedCharacterId();
        purchasedCharacterIds = saver.getPurchasedCharacterIds();
        Debug.Log($"{purchasedCharacterIds}!");
        characterImage.sprite = characterSprites[currentCharacterId];
        UpdateWeaponInfo();
        UpdateSelectedCharacter();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateWeaponInfo()
    {
        coins = saver.getCoins();
        weaponLvl = saver.getWeaponLvl();
        weaponCost = saver.getWeaponCost(weaponLvl);
        weaponAttackText.text = $"”–ŒÕ {weaponLvl}";
        weaponCostText.text = $"÷≈Õ¿ {weaponCost}";
        coinsTotalText.text = $"ÃŒÕ≈“€: {coins}";
        upgradeWeaponButton.gameObject.SetActive(weaponCost <= coins);
        downgradeWeaponButton.gameObject.SetActive(weaponLvl > 1);
    }

    public void UpgradeWeapon()
    {
        saver.removeCoins(saver.getWeaponCost(weaponLvl));
        saver.upgradeWeapon();
        UpdateWeaponInfo();
    }

    public void DowngradeWeapon()
    {
        saver.addCoins(saver.getWeaponCost(weaponLvl - 1));
        saver.downgradeWeapon();
        UpdateWeaponInfo();
    }

    void UpdateSelectedCharacter()
    {
        Debug.Log($"{this.purchasedCharacterIds.Count}");
        if (purchasedCharacterIds.Contains(currentCharacterId))
        {
            saver.setSelectedCharacterId(currentCharacterId);
        }
        coins = saver.getCoins();
        characterImage.sprite = characterSprites[currentCharacterId];
        currentCharacterCost = saver.getCharacterCost(currentCharacterId);
        characterCostText.text = !purchasedCharacterIds.Contains(currentCharacterId) ? $"÷≈Õ¿: {currentCharacterCost}" : "";
        buyCharacterButton.gameObject.SetActive(currentCharacterCost <= coins && !purchasedCharacterIds.Contains(currentCharacterId));
        coinsTotalText.text = $"ÃŒÕ≈“€: {coins}";
        characterStatsText.text = $"«ƒŒ–Œ¬‹≈ {saver.getHealth(currentCharacterId)}\n— Œ–Œ—“‹ {saver.getSpeed(currentCharacterId)}";
        if (currentCharacterId == 1)
        {
            characterImage.color = new Color(0.7f, 0.7f, 0, 1);
        }
        else
        {
            characterImage.color = new Color(1, 1, 1, 1);
        }
    }

    public void BuyCharacter()
    {
        saver.buyCharacter(currentCharacterId);
        saver.removeCoins(currentCharacterCost);
        UpdateSelectedCharacter();
        UpdateWeaponInfo();
    }

    public void ScrollCharactersLeft()
    {
        if (currentCharacterId <= 0)
        {
            currentCharacterId = characterSprites.Count - 1;
        }
        else
        {
            currentCharacterId -= 1;
        }
        UpdateSelectedCharacter();
    }

    public void ScrollCharactersRight()
    {
        if (currentCharacterId >= characterSprites.Count - 1)
        {
            currentCharacterId = 0;
        }
        else
        {
            currentCharacterId += 1;
        }
        UpdateSelectedCharacter();
    }
}
