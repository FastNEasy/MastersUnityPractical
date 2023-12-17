using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] float waitForRestart = 2;
    public int enemyCount {get; set;}

    PlayerCharacter _player;
    public PlayerCharacter Player{
        get{
            if(_player == null) _player = FindObjectOfType<PlayerCharacter>();
            return _player;
        }
    }
    void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    void Start(){
    }

    void EnemyKilled(SkeletonAI enemy)
    {
        Debug.Log($"Enemy {enemy.gameObject.name} was disposed");
        enemyCount--;
        if (enemyCount <= 0) GameWin();
    }
    

    void GameWin(){
        Debug.Log("Game win");
        StartCoroutine(RestartRoutine());
    }

    IEnumerator RestartRoutine()
    {
        yield return new WaitForSeconds(waitForRestart);
        RestartGame();

    }

    void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
