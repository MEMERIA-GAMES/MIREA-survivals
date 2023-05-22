using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, управляющий аниматором
/// </summary>
public class Animate : MonoBehaviour
{

    /// <summary>
    /// Аниматор
    /// </summary>
    Animator animator;

    /// <summary>
    /// Скорость по горизонтали
    /// </summary>
    public float oldHorizontal = 0f, horizontal = 0f;
    public float oldVertical = 0f, vertical = 0f;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // изменение в горизонтальной скорости
        if (oldHorizontal != horizontal)
            animator.SetFloat("Horizontal", horizontal);
        if (oldVertical != vertical)
            animator.SetFloat("Vertical", horizontal);
        oldHorizontal = horizontal;
        oldVertical = vertical;
    }

}
