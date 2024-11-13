using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool running = true;
    public GameObject LivesScreen;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        running = true;
        LivesScreen.gameObject.SetActive(true);
    }
}