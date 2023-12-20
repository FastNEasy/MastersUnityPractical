using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] float waitForRestart = 6f;
    public int enemyCount {get; set;}
    // public int waveState {get; set;}

    public bool levelStarted {get; set;}
    // public bool waveCompleted {get; set;}

    PlayerCharacter _player;
    public PlayerCharacter Player{
        get{
            if(_player == null) _player = FindObjectOfType<PlayerCharacter>();
            return _player;
        }
    }
    void Awake(){
        enemyCount = 3;
        levelStarted = false;
        // waveState = 1;
        // waveCompleted = false;
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    // void Start(){
    //     // StartNextWave();
    //     survivalStarted = true;
    //     // FindObjectOfType<SkeletonAI>().onSkellyKilled += EnemyKilled;

    // }

    // public void EnemyKilled(SkeletonAI skelly)
    // {
    //     enemyCount--;
    //     //GameWin()
    //     if (enemyCount <= 0){
    //         waveCompleted = true;
    //         waveState++;
    //         // Debug.Log("I work!");
    //         // StartNextWave();
    //         if(waveState > 3) GameWin();
    //     }
    // }
    

    public void GameWin(){
        Debug.Log("Game win");
        //display win window
        // _player.
        StartCoroutine(RestartRoutine());
    }

    public void GameLose(){
        Debug.Log("Game lose");
        //display lose window
        StartCoroutine(RestartRoutine());
    }
    
    // public void StartNextWave(){
    //     Debug.Log("Next wave: " + waveState);
    //     waveCompleted = false;
    //     enemyCount = waveState;

    //     // FindObjectOfType<SkeletonAI>().OnSkellyKilled += EnemyKilled;

    // }

    IEnumerator RestartRoutine()
    {
        yield return new WaitForSeconds(waitForRestart);
        RestartGame();

    }

    void RestartGame(){
        //SceneManager.GetActiveScene().name
        SceneManager.LoadScene("MainMenu");
    }
}
