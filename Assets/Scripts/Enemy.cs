using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс поведения врагов
/// </summary>
public class Enemy : MonoBehaviour, IDamagable
{
    // Координаты цели
    Transform targetDestination;
    // Цель
    GameObject targetGameobject;
    Character targetCharacter;
    // Скорость
    [SerializeField] float speed;

    Rigidbody2D rgdbd2d;
    
    // Здоровье и урон
    [SerializeField] int hp = 999;
    [SerializeField] int damage = 1;

    private void Awake(){
        rgdbd2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Установить цель
    /// </summary>
    /// <param name="target">Цель</param>
    public void SetTarget(GameObject target){
        targetGameobject = target;
        targetDestination = target.transform;
    }

    /// <summary>
    /// Перемещение враша в сторону цели
    /// </summary>
    private void FixedUpdate(){
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rgdbd2d.velocity = direction * speed;
    }

    /// <summary>
    /// При коллизии врага с целью наносится атака
    /// </summary>
    /// <param name="collision">Коллизия</param>
    private void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject == targetGameobject){
            Attack();
        }
    }

    /// <summary>
    /// Атака цели (персонажа)
    /// </summary>
    private void Attack(){
        //.Debug.Log("Attacking the character");
        if(targetCharacter == null) 
            targetCharacter = targetGameobject.GetComponent<Character>();
        
        targetCharacter.TakeDamage(damage);
    }

    /// <summary>
    /// Получить урон. Отнимает от здоровья полученный урон
    /// </summary>
    /// <param name="damage">Полученный урон</param>
    public void TakeDamage(int damage){
        hp -= damage;

        if (hp <= 0){
            Destroy(gameObject);
        }
    }
}
