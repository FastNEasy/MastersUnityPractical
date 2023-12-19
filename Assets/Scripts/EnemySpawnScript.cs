using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    // [SerializeField] GameObject[] spawnPoints;
    // public GameObject enemy;
    // bool enemiesSpanwed = false;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     StartCoroutine(checkWave());
    // }


    // IEnumerator checkWave(){
    //     while (true)
    //     {
    //         Debug.Log("Wave:" + GameManager.instance.waveState + "EnemysLeft:" + GameManager.instance.enemyCount + GameManager.instance.waveCompleted);
    //         if(GameManager.instance.survivalStarted){
    //             if(!enemiesSpanwed){
    //                 SpawnEnemies(GameManager.instance.enemyCount);
                    
    //             }
    //             if(GameManager.instance.waveCompleted){
    //                 Debug.Log("Wave complete.. moving on");
    //                 GameManager.instance.StartNextWave();
    //                 enemiesSpanwed = false;
    //             }
    //         }
    //     yield return null;   
    //     }
    // }

    // void SpawnEnemies(int enemyCount){
    //     int prevIndex = 1;
    //     for(int i = 1; i <= enemyCount; i++){
    //         int index = Random.Range(0, spawnPoints.Length);
    //         if(enemyCount == 1){
    //             GameObject newEnemy = Instantiate(enemy, spawnPoints[index].transform.position, Quaternion.identity);
    //         }else if(index != prevIndex){
    //             GameObject newEnemy = Instantiate(enemy, spawnPoints[index].transform.position, Quaternion.identity);
    //         }
    //         prevIndex = index;
    //     }
    //     enemiesSpanwed = true;
    // }
}
