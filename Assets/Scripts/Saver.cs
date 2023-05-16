using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections.Generic;


public class Saver : MonoBehaviour
{
    int coins;
    int weaponLvl;
    List<int> purchasedCharacterIds;
    int selectedCharacterId;

    [Serializable]
    class Data
    {
        public int coins;
        public int weaponLvl;
        public List<int> purchasedCharacterIds;
        public int selectedCharacterId;
    }

    void Start()
    {
        //resetLevel();
        //deleteData();
        //loadData();
    }

    public void saveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        Data data = new Data();
        data.coins = this.coins;
        data.weaponLvl = this.weaponLvl;
        data.purchasedCharacterIds = this.purchasedCharacterIds;
        data.selectedCharacterId = this.selectedCharacterId;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public void loadData()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            DateTime startTime = DateTime.Now; 
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath
              + "/MySaveData.dat", FileMode.Open);
            Data data = (Data)bf.Deserialize(file);
            file.Close();
            this.coins = data.coins;
            this.weaponLvl = data.weaponLvl;
            this.purchasedCharacterIds = data.purchasedCharacterIds;
            this.selectedCharacterId = data.selectedCharacterId;
            DateTime endTime = DateTime.Now;
            TimeSpan duration = endTime - startTime;
            Debug.Log($"Game data loaded ({this.coins}, {this.weaponLvl}, {this.purchasedCharacterIds}, {this.selectedCharacterId}) in {duration.TotalSeconds}s!");
        }
        else
        {
            this.coins = 0;
            this.weaponLvl = 1;
            this.purchasedCharacterIds = new List<int> { 0 };
            this.selectedCharacterId = 0;
            Debug.LogWarning("There is no save data!");
        }

    }

    public void deleteData()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveData.dat");
            this.coins = 0;
            this.weaponLvl = 1;
            this.purchasedCharacterIds = new List<int> { 0 };
            this.selectedCharacterId = 0;
            Debug.Log("Data has been deleted");
        }
    }

    public void addCoins(int newCoins)
    {
        this.coins += newCoins;
    }

    public int getCoins()
    {
        return this.coins;
    }

    public void removeCoins(int newCoins)
    {
        this.coins -= newCoins;
    }

    public void upgradeWeapon()
    {
        this.weaponLvl += 1;
    }

    public void downgradeWeapon()
    {
        this.weaponLvl -= 1;
    }

    public int getWeaponLvl()
    {
        return this.weaponLvl;
    }

    public void buyCharacter(int characterId)
    {
        Debug.Log("Buy character " + characterId);
        Debug.Log(this.purchasedCharacterIds);
        this.purchasedCharacterIds.Add(characterId);
        Debug.Log(this.purchasedCharacterIds);
    }

    public List<int> getPurchasedCharacterIds()
    {
        return this.purchasedCharacterIds;
    }

    public int getSelectedCharacterId()
    {
        return this.selectedCharacterId;
    }

    public void setSelectedCharacterId(int characterId)
    {
        this.selectedCharacterId = characterId;
        Debug.Log($"Current character index is ({this.selectedCharacterId}");
    }

    public int getSpeed(int characterId)
    {
        return characterId + 1;
    }

    public int getHealth(int characterId)
    {
        return (characterId + 1) * 100;
    }

    public int getCharacterCost(int characterId)
    {
        return characterId * 1000;
    }

    public int getWeaponCost(int weaponLvl)
    {
        return weaponLvl * 100;
    }
}
