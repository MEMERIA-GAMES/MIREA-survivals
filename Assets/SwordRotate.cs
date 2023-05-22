using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Класс оружия-хлыста
/// </summary>
public class SwordRotate : MonoBehaviour
{
    /// <summary>
    /// Интервал атаки
    /// </summary>
    [SerializeField] float timeToAttack = 4f;
    float timer = 0f;

    /// <summary>
    /// Меч
    /// </summary>
    [SerializeField] GameObject swordObject;
    
    /// <summary>
    /// Движение игрока
    /// </summary>
    PlayerMove playerMove;

    /// <summary>
    /// Размер атаки
    /// </summary>
    [SerializeField] Vector2 weaponAttackSize = new Vector2(4f, 2f);

    /// <summary>
    /// Урон оружия
    /// </summary>
    [SerializeField] int weaponDamage = 1;

    [SerializeField] float rotationPerFrame = 2f;

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
        transform.Rotate(0, 0, rotationPerFrame);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(swordObject.transform.position, weaponAttackSize, 0f);
        ApplyDamage(colliders);
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
                e.TakeDamage(weaponDamage);
            }
        }
    }
}
