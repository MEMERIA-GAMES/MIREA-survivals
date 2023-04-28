using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Класс генерации врагов
/// </summary>
public class EnemiesManager : MonoBehaviour
{
    /// <summary>
    /// Класс врага
    /// </summary>
    [SerializeField] GameObject enemy;
    
    /// <summary>
    /// Область спавна
    /// </summary>
    [SerializeField] Vector2 spawnArea;
    
    /// <summary>
    /// Таймер спавна
    /// </summary>
    [SerializeField] float spawnTimer;
    
    /// <summary>
    /// Цель врагов
    /// </summary>
    [SerializeField] GameObject player;
    
    /// <summary>
    /// Таймер
    /// </summary>
    float timer;
    
    /// <summary>
    /// Каждый кадр таймер уменьшается. Когда он дойдет до нуля, он обновиться, и заспавниться новый враг
    /// </summary>
    private void Update(){
        timer -= Time.deltaTime;
        if (timer < 0f){
            SpawnEnemy();
            timer = spawnTimer;
        }
    }

    /// <summary>
    /// Спавн врага. Спавнит врага в случайном месте по периметру spawnArea
    /// </summary>
    private void SpawnEnemy(){
        Vector3 position = GenerateRandomPosition();
        position += player.transform.position;

        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;
        newEnemy.GetComponent<Enemy>().SetTarget(player);
        newEnemy.transform.parent = transform;
    }

    /// <summary>
    /// Генерирует место появления врага.
    /// </summary>
    /// <returns>Координаты врага</returns>
    private Vector3 GenerateRandomPosition(){
        Vector3 position = new Vector3();
        float fvalue = UnityEngine.Random.value > 0.5f ? -1f : 1f;
        if (fvalue>0){
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * fvalue;
        }else{
            position.x = spawnArea.x * fvalue;
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
        }
        position.z = 0f;
        return position;
    }
}
