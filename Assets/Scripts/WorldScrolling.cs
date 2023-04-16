using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    Vector2Int currentTilePosition = new Vector2Int(0, 0);
    [SerializeField] Vector2Int playerTilePosition;
    Vector2Int onTileGridPlayerPosition;
    [SerializeField] float tileSize = 20f;
    GameObject[,] terrainTiles;

    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;

    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidth = 3;

    public void Add(GameObject tileGameObject, Vector2Int tilePosition){
        terrainTiles[tilePosition.x, tilePosition.y] = tileGameObject;
    }

    public void Start(){
        UpdateTilesOnScreen();
    }

    private void Awake(){
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }

    private void Update(){
        playerTilePosition.x = (int)(playerTransform.position.x / tileSize);
        playerTilePosition.y = (int)(playerTransform.position.y / tileSize);

        playerTilePosition.x -= playerTransform.position.x < 0 ? 1 : 0;
        playerTilePosition.y -= playerTransform.position.y < 0 ? 1 : 0;

        if (currentTilePosition != playerTilePosition){
            currentTilePosition = playerTilePosition;

            onTileGridPlayerPosition.x = CalculatePositionOnAxis(onTileGridPlayerPosition.x, true);
            onTileGridPlayerPosition.y = CalculatePositionOnAxis(onTileGridPlayerPosition.y, false);
            UpdateTilesOnScreen();
        }
    }

    private int CalculatePositionOnAxis(float currectValue, bool horizontal){
        if (horizontal){
            if (currectValue >= 0){
                currectValue = currectValue % terrainTileHorizontalCount;
            } else {
                currectValue += 1;
                currectValue = terrainTileHorizontalCount - 1 + currectValue % terrainTileHorizontalCount;
            }
        } else {
            if (currectValue >= 0){
                currectValue = currectValue % terrainTileVerticalCount;
            } else {
                currectValue += 1;
                currectValue = terrainTileVerticalCount - 1 + currectValue % terrainTileVerticalCount;
            }
        }
        
        return (int) currectValue;
    }

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
