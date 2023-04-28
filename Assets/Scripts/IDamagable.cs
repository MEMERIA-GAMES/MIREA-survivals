using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Интерфейс для объектов, которые могут получить урон
/// </summary>
public interface IDamagable
{
    public void TakeDamage(int damage);
}