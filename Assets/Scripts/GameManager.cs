using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] float waitForRestart = 2;

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
