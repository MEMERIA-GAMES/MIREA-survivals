using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс тайла
/// </summary>
public class TerrainTile : MonoBehaviour
{
    /// <summary>
    /// Позиция тайла в сетке
    /// </summary>
    [SerializeField] Vector2Int tilePosition;

    /// <summary>
    /// На старте добавляем тайл в сетку тайлов в WorldScrolling
    /// </summary>
    void Start()
    {
        GetComponentInParent<WorldScrolling>().Add(gameObject, tilePosition);
        //transform.position = new Vector3(-100, -100, 0);
    }
}
