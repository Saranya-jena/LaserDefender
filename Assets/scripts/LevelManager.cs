using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public void LoadLevel(string name)
    {

        //Application.LoadLevel (name);
        SceneManager.LoadScene(name);
       
    }
    public void QuitRequest()
    {
        Debug.Log("i want to quit");
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        //Application.LoadLevel (Application.loadedLevel + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}