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
        characterImage.sprite = characterSprites[currentCharacterId];
        UpdateWeaponInfo();
        UpdateSelectedCharacter();
    }

    // �������� ��� ����
    private void Update()
    {

    }

    void UpdateWeaponInfo()
    {
        saver.loadData();
        coins = saver.getCoins();
        weaponLvl = saver.getWeaponLvl();
        weaponCost = saver.getWeaponCost(weaponLvl);
        weaponAttackText.text = $"���� {weaponLvl}";
        weaponCostText.text = $"���� {weaponCost}";
        coinsTotalText.text = $"������: {coins}";
        upgradeWeaponButton.gameObject.SetActive(weaponCost <= coins);
        downgradeWeaponButton.gameObject.SetActive(weaponLvl > 1);
    }

    public void UpgradeWeapon()
    {
        saver.removeCoins(saver.getWeaponCost(weaponLvl));
        saver.upgradeWeapon();
        saver.saveData();
        UpdateWeaponInfo();
    }

    public void DowngradeWeapon()
    {
        saver.addCoins(saver.getWeaponCost(weaponLvl - 1));
        saver.downgradeWeapon();
        saver.saveData();
        UpdateWeaponInfo();
    }

    void UpdateSelectedCharacter()
    {
        purchasedCharacterIds = saver.getPurchasedCharacterIds();
        if (purchasedCharacterIds.Contains(currentCharacterId))
        {
            saver.setSelectedCharacterId(currentCharacterId);
            saver.saveData();
        }
        coins = saver.getCoins();
        characterImage.sprite = characterSprites[currentCharacterId];
        currentCharacterCost = saver.getCharacterCost(currentCharacterId);
        characterCostText.text = !purchasedCharacterIds.Contains(currentCharacterId) ? $"����: {currentCharacterCost}" : "";
        buyCharacterButton.gameObject.SetActive(currentCharacterCost <= coins && !purchasedCharacterIds.Contains(currentCharacterId));
        coinsTotalText.text = $"������: {coins}";
        characterStatsText.text = $"�������� {saver.getHealth(currentCharacterId)}\n�������� {saver.getSpeed(currentCharacterId)}";
    }

    public void BuyCharacter()
    {
        saver.buyCharacter(currentCharacterId);
        saver.removeCoins(currentCharacterCost);
        saver.saveData();
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
