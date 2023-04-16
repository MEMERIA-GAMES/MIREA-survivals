using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnTimer;
    [SerializeField] GameObject player;
    float timer;

    private void Update(){
        timer -= Time.deltaTime;
        if (timer < 0f){
            SpawnEnemy();
            timer = spawnTimer;
        }
    }

    private void SpawnEnemy(){
        Vector3 position = GenerateRandomPosition();
        position += player.transform.position;

        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;
        newEnemy.GetComponent<Enemy>().SetTarget(player);
        newEnemy.transform.parent = transform;
    }

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
