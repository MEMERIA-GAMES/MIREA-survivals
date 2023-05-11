using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс игрового персонажа
/// </summary>
public class Character : MonoBehaviour
{
    /// <summary>
    /// Максимальное здоровье
    /// </summary>
    public int maxHP = 1000;

    /// <summary>
    /// Текущее здоровье
    /// </summary>
    public int currentHP = 1000;

    public Saver saver;
    public UI ui;
    public int coinsCollected = 0;
    /// <summary>
    /// Объект ХП-бара
    /// </summary>
    [SerializeField] StatusBar hpBar;

    private void Start(){
        hpBar.SetState(currentHP, maxHP);
        ui.coinsCollectedText.text = "COINS: " + coinsCollected.ToString();
    }

    /// <summary>
    /// Получить урон. Вычитает урон из текущего здоровья
    /// </summary>
    /// <param name="damage">Полученный урон</param>
    public void TakeDamage(int damage){
        //Debug.Log("Take damage");
        currentHP -= damage;
        coinsCollected += 1;
        ui.coinsCollectedText.text = "COINS: " + coinsCollected.ToString();

        if (currentHP <= 0){
            Debug.Log("Character is dead");
        }
        hpBar.SetState(currentHP, maxHP);
    }

    /// <summary>
    /// Лечение. Восстанавливает текущее хп
    /// </summary>
    /// <param name="amount">Размер лечения</param>
    public void Heal(int amount){
        if (currentHP <= 0){return;}

        currentHP += amount;
        if (currentHP > maxHP){
            currentHP = maxHP;
        }
        hpBar.SetState(currentHP, maxHP);
    }

    public void saveProgress()
    {
        saver.addCoins(coinsCollected);
        saver.saveData();
    }
}
