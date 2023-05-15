using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Класс управления ворлд-скроллинга
/// </summary>
public class WorldScrolling : MonoBehaviour
{
    /// <summary>
    /// Разрешение на бесконечное вертикальное перемещение
    /// </summary>
    public bool verticalMoveAccept = true;

    /// <summary>
    /// Разрешение на бесконечное горизонтальное перемещение
    /// </summary>
    public bool horizontalMoveAccept = true;

    /// <summary>
    /// Положение игрока
    /// </summary>
    [SerializeField] Transform playerTransform;
    Vector2Int currentTilePosition = new Vector2Int(0, 0);
    
    /// <summary>
    /// Тайл, на котором стоит игрок
    /// </summary>
    [SerializeField] Vector2Int playerTilePosition;
    Vector2Int onTileGridPlayerPosition;
    
    /// <summary>
    /// Размер тайла
    /// </summary>
    [SerializeField] float tileSize = 20f;
    
    /// <summary>
    /// Матрица тайлов
    /// </summary>
    GameObject[,] terrainTiles;

    /// <summary>
    /// Количество тайлов
    /// </summary>
    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;

    /// <summary>
    /// Сколько тайлов находятся в области видимости одновременно
    /// </summary>
    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidth = 3;

    /// <summary>
    /// Добавление нового тайла в матрицу
    /// </summary>
    /// <param name="tileGameObject">Объект тайла</param>
    /// <param name="tilePosition">Координаты тайла (в матрице)</param>
    public void Add(GameObject tileGameObject, Vector2Int tilePosition){
        terrainTiles[tilePosition.x, tilePosition.y] = tileGameObject;
    }

    /// <summary>
    /// На старте игры обновляем расположение тайлов
    /// </summary>
    public void Start(){
        UpdateTilesOnScreen();
    }

    /// <summary>
    /// При загрузке сцены создаем матрицу тайлов
    /// </summary>
    private void Awake(){
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }

    /// <summary>
    /// Метод, вызывающийся каждый кадр. 
    /// </summary>
    private void Update(){
        // определяем, в каком тайле находимся
        playerTilePosition.x = (int)(playerTransform.position.x / tileSize);
        playerTilePosition.y = (int)(playerTransform.position.y / tileSize);
        
        // поправка позиции
        playerTilePosition.x -= playerTransform.position.x < 0 ? 1 : 0;
        playerTilePosition.y -= playerTransform.position.y < 0 ? 1 : 0;

        // если игрок перешел на другой тайл
        if (currentTilePosition != playerTilePosition){
            currentTilePosition = playerTilePosition;

            onTileGridPlayerPosition.x = CalculatePositionOnAxis(onTileGridPlayerPosition.x, true);
            onTileGridPlayerPosition.y = CalculatePositionOnAxis(onTileGridPlayerPosition.y, false);
            UpdateTilesOnScreen();
        }
    }

    /// <summary>
    /// Рассчет новой позиции тайла
    /// </summary>
    /// <param name="currentValue">Текущая позиция</param>
    /// <param name="horizontal">Перемещение по горизонтали (true/false)</param>
    /// <returns>Координата</returns>
    private int CalculatePositionOnAxis(float currentValue, bool horizontal){
        if (horizontal){
            if (currentValue >= 0){
                currentValue = currentValue % terrainTileHorizontalCount;
            } else {
                currentValue += 1;
                currentValue = terrainTileHorizontalCount - 1 + currentValue % terrainTileHorizontalCount;
            }
        } else {
            if (currentValue >= 0){
                currentValue = currentValue % terrainTileVerticalCount;
            } else {
                currentValue += 1;
                currentValue = terrainTileVerticalCount - 1 + currentValue % terrainTileVerticalCount;
            }
        }
        
        return (int) currentValue;
    }

    /// <summary>
    /// Перестановка тайлов в зависимости от положения игрока
    /// </summary>
    private void UpdateTilesOnScreen(){
        for (int pov_x = -(fieldOfVisionWidth/2); pov_x <= fieldOfVisionWidth/2; pov_x++){
            for (int pov_y = -(fieldOfVisionHeight/2); pov_y <= fieldOfVisionHeight/2; pov_y++){
                int tileToUpdate_x = CalculatePositionOnAxis(playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatePositionOnAxis(playerTilePosition.y + pov_y, false);

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                tile.transform.position = CalculateTilePosition(
                    playerTilePosition.x + pov_x,
                    playerTilePosition.y + pov_y
                );
            }
        }
    }

    private Vector3 CalculateTilePosition(int x, int y){
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }
}
