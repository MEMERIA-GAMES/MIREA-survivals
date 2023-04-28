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
    public float horizontal;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("Horizontal", horizontal);
    }
}
