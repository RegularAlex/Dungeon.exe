using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class MainMenuScript : MonoBehaviour
{
    public GameObject MenuItem;
    

    public void StartPressed()
    {
        if (MenuItem.active == true)
        {
            MenuItem.SetActive(false);
        }
        else
        {
            MenuItem.SetActive(true);
        }
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }
}
