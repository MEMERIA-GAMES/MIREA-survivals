using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
/// <summary>
/// Класс, управляющий передвижением игрока
/// </summary>
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rgbd2d;
    /// <summary>
    /// Вектор скорости
    /// </summary>
    [HideInInspector] public Vector3 movementVector;

    /// <summary>
    /// Последние сохраненные показатели скорости
    /// </summary>
    [HideInInspector] public float lastHorizontalVector;
    [HideInInspector] public float lastVerticalVector;

    /// <summary>
    /// Величина скорости
    /// </summary>
    [SerializeField] float speed = 3f;

    /// <summary>
    /// Объект аниматора
    /// </summary>
    Animate animate;

    private void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        animate = GetComponent<Animate>();
    }

    /// <summary>
    /// Каждый кадр считываем с устройсва ввода направление движения.
    /// </summary>
    void Update()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        if(movementVector.x != 0){
            lastHorizontalVector = movementVector.x;
        }
        if(movementVector.y != 0){
            lastVerticalVector = movementVector.y;
        }

        // Передаем направление в анимейт для настройки подходящей анимации
        animate.horizontal = movementVector.x;
        animate.vertical = movementVector.y;

        // умножаем вектор направления на скорость
        movementVector *= speed;

        rgbd2d.velocity = movementVector;
    }
}
