using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Класс оружия-хлыста
/// </summary>
public class Whip : MonoBehaviour
{
    /// <summary>
    /// Интервал атаки
    /// </summary>
    [SerializeField] float timeToAttack = 4f;
    float timer = 0f;

    /// <summary>
    /// Левый и правый спрайт атаки
    /// </summary>
    [SerializeField] GameObject leftWeaponObject;
    [SerializeField] GameObject rightWeaponObject;
    
    /// <summary>
    /// Движение игрока
    /// </summary>
    PlayerMove playerMove;

    /// <summary>
    /// Размер атаки
    /// </summary>
    [SerializeField] Vector2 whipAttackSize = new Vector2(4f, 2f);

    /// <summary>
    /// Урон оружия
    /// </summary>
    [SerializeField] int whipDamage = 1;

    /// <summary>
    /// Метод исполняется при загрузке сцены
    /// </summary>
    private void Awake(){
        playerMove = GetComponentInParent<PlayerMove>();
    }

    /// <summary>
    /// Обновление таймера каждый кадр
    /// </summary>
    private void Update()
    {   
        // Отсчет таймера
        timer -= Time.deltaTime;
        // Когда таймер доходит до нуля, хлыст атакует
        if (timer < 0f){
            Attack();
        }
    }

    /// <summary>
    /// Атака оружия
    /// </summary>
    private void Attack(){
        //Debug.Log("Whip Attack");
        // Возобновление таймера
        timer = timeToAttack;
        // Расчет анимации атаки
        if(playerMove.lastHorizontalVector > 0){
            rightWeaponObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWeaponObject.transform.position, whipAttackSize, 0f);
            ApplyDamage(colliders);
        }else{
            leftWeaponObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWeaponObject.transform.position, whipAttackSize, 0f);
            ApplyDamage(colliders);
        }
    }

    /// <summary>
    /// Нанесение урона
    /// </summary>
    /// <param name="colliders">Коллизия объекта с хлыстом</param>
    private void ApplyDamage(Collider2D[] colliders){
        for(int i = 0; i<colliders.Length; ++i){
            //Debug.Log(colliders[i].gameObject.name);
            // Получение объекта коллизии
            IDamagable e = colliders[i].GetComponent<IDamagable>();
            if (e != null){
                // Нанесение урона
                e.TakeDamage(whipDamage);
            }
        }
    }
}
