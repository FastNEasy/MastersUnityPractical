using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    
    public void StartGame(){
        Debug.Log("Game is starting");
        SceneManager.LoadScene("MainLevel");
    }
    public void QuitGame () {
        Application.Quit ();
        Debug.Log("Game is exiting");
    }
    public void EndGame(){
        Debug.Log("Game is ending");
        SceneManager.LoadScene("MainMenu");
    }
}
