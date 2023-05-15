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
    public int maxHP;

    /// <summary>
    /// Текущее здоровье
    /// </summary>
    public int currentHP;

    public Saver saver;
    public UI ui;
    public int coinsCollected = 0;
    public int characterId;
    public SpriteRenderer sprite;
    /// <summary>
    /// Объект ХП-бара
    /// </summary>
    [SerializeField] StatusBar hpBar;

    private void Start(){
        hpBar.SetState(currentHP, maxHP);
        ui.coinsCollectedText.text = "МОНЕТЫ: " + coinsCollected.ToString();
        characterId = saver.getSelectedCharacterId();
        if (characterId == 1)
        {
            sprite.color = new Color(0.7f, 0.7f, 0, 1);

        }
        maxHP = saver.getHealth(characterId);
        currentHP = maxHP;
        //saver.addCoins(1000);

    }

    /// <summary>
    /// Получить урон. Вычитает урон из текущего здоровья
    /// </summary>
    /// <param name="damage">Полученный урон</param>
    public void TakeDamage(int damage){
        //Debug.Log("Take damage");
        currentHP -= damage;

        if (currentHP <= 0){
            Debug.Log("Character is dead");
        }
        hpBar.SetState(currentHP, maxHP);
    }

    public void gainCoins(int coinCount)
    {
        coinsCollected += coinCount;
        ui.coinsCollectedText.text = "COINS: " + coinsCollected.ToString();
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
