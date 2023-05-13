using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections.Generic;


public class Saver : MonoBehaviour
{
    int coins;
    int weaponLvl;
    List<string> purchasedCharacters;

    [Serializable]
    class Data
    {
        public int coins;
        public int weaponLvl;
        public List<string> purchasedCharacters;
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
        data.purchasedCharacters = this.purchasedCharacters;
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
            this.purchasedCharacters = data.purchasedCharacters;
            DateTime endTime = DateTime.Now;
            TimeSpan duration = endTime - startTime;
            Debug.Log($"Game data loaded in {duration.TotalSeconds}s!");
        }
        else
        {
            this.coins = 0;
            this.weaponLvl = 1;
            List<int> list = new List<int> { 0 };
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
            this.purchasedCharacters = new List<string> { "spriteshadowless.png" };
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
}
