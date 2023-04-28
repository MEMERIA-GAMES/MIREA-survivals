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
    // Вектор скорости
    [HideInInspector] public Vector3 movementVector;
    // Последние сохраненные показатели скорости
    [HideInInspector] public float lastHorizontalVector;
    [HideInInspector] public float lastVerticalVector;

    // Скорость
    [SerializeField] float speed = 3f;

    // Аниматор
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

        // Передаем направление в аниматор для настройки подходящей анимации
        animate.horizontal = movementVector.x;

        // умножаем вектор направления на скорость
        movementVector *= speed;

        rgbd2d.velocity = movementVector;
    }
}
