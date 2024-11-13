using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject LivesScreen;

    void Update()
    {
        LivesScreen.gameObject.SetActive(false);
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                print("Application Closed");
                Application.Quit();
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")) { return;}
            else {
                SceneManager.LoadScene(1,LoadSceneMode.Single);
            }
            
        }
    }
}
