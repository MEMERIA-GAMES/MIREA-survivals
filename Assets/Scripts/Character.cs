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
    
    /// <summary>
    /// Список спрайтов
    /// </summary>
    public List<string> spritesList;

    public Saver saver;
    public UI ui;
    public int coinsCollected = 0;
    public int characterId;
    public Animator spriteAnimator;
    /// <summary>
    /// Объект ХП-бара
    /// </summary>
    [SerializeField] StatusBar hpBar;

    private void Start(){
        saver.loadData();
        hpBar.SetState(currentHP, maxHP);
        ui.coinsCollectedText.text = "МОНЕТЫ: " + coinsCollected.ToString();
        characterId = saver.getSelectedCharacterId();
        Debug.Log("CharacterId " + characterId);
        Debug.Log("List " + spritesList);
        Debug.Log("Path " + spritesList[characterId]);
        spriteAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(spritesList[characterId]);
        maxHP = saver.getHealth(characterId);
        currentHP = maxHP;
        spriteAnimator.Play("Entry", 0, 0);
        //saver.addCoins(1000);

    }

    private void Awake(){
        saver.loadData();
        characterId = saver.getSelectedCharacterId();
        spriteAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(spritesList[characterId]);
        Debug.Log("Awake " + characterId);
        spriteAnimator.Play("Entry", 0, 0);
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
