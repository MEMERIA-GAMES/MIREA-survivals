using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Класс оружия-хлыста
/// </summary>
public class Whip : MonoBehaviour
{
    [SerializeField] float timeToAttack = 4f;
    float timer = 0f;

    [SerializeField] GameObject leftWhipObject;
    [SerializeField] GameObject rightWhipObject;
    
    PlayerMove playerMove;
    [SerializeField] Vector2 whipAttackSize = new Vector2(4f, 2f);
    [SerializeField] int whipDamage = 1;

    /// <summary>
    /// Метод исполняется при загрузке сцены
    /// </summary>
    private void Awake(){
        playerMove = GetComponentInParent<PlayerMove>();
    }

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
            rightWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, whipAttackSize, 0f);
            ApplyDamage(colliders);
        }else{
            leftWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, whipAttackSize, 0f);
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
