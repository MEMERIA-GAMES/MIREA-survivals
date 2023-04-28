using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс тайла
/// </summary>
public class TerrainTile : MonoBehaviour
{
    // Позиция тайла
    [SerializeField] Vector2Int tilePosition;

    /// <summary>
    /// На старте добавляем тайл в матрицу тайлов в WorldScrolling
    /// </summary>
    void Start()
    {
        GetComponentInParent<WorldScrolling>().Add(gameObject, tilePosition);
        transform.position = new Vector3(-100, -100, 0);
    }
}
