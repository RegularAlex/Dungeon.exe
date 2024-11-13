using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using System.Linq;
using TMPro;


public class WaveSpawner : MonoBehaviour
{
    #region whole lotta variables

    public GameManager gm;

    // DIFFICULTY PROPERTIES
    // easy
    float easyShooterChance = 10;
    float easyLowerSpeed = 1f;
    float easyUpperSpeed = 1.5f;
    float easyLowerFirerate = 2f;
    float easyUpperFirerate = 5f;

    // medium
    float mediumShooterChance = 50;
    float mediumLowerSpeed = 1.5f;
    float mediumUpperSpeed = 2.5f;
    float mediumLowerFirerate = 0.5f;
    float mediumUpperFirerate = 2f;

    // hard
    float hardShooterChance = 80;
    float hardLowerSpeed = 2.5f;
    float hardUpperSpeed = 6f;
    float hardLowerFirerate = 0.25f;
    float hardUpperFirerate = 0.75f;

    // very hard
    float veryHardShooterChance = 80;
    float veryHardLowerSpeed = 6f;
    float veryHardUpperSpeed = 10f;
    float veryHardLowerFirerate = 0.1f;
    float veryHardUpperFirerate = 0.75f;


    // overall game wave properties
    int numEasyWaves = 3;
    int numMediumWaves = 5;
    int numHardWaves = 10;
    // ... infinite very hard from here
    //float timeBetweenWaves = 5f; 

    int currentWave = 0;

    public TextMeshProUGUI textelement;

    #endregion


    public GameObject baseEnemyPrefab;
    public GameObject playerObj;
    [HideInInspector] public List<string> waveDifficulties = new List<string>();



    void Start()
    {
        StartCoroutine(SpawnWave("easy", 1, 5)); // this + starting currentWave at 0 makes it so Update doesn't immediately think we won wave 1 and increment. can work as a quick "tutorial" anyway
        //StartCoroutine(StartManagingWaves());

        waveDifficulties = InitWaveDifficulties();

        textelement.text = "Wave: " + getWave();
    }



    void Update()
    {
        /* for debug
        if (Input.GetKeyDown(KeyCode.Alpha1)) StartCoroutine(SpawnWave("easy", 10));
        if (Input.GetKeyDown(KeyCode.Alpha2)) StartCoroutine(SpawnWave("medium", 10));
        if (Input.GetKeyDown(KeyCode.Alpha3)) StartCoroutine(SpawnWave("hard", 10));
        if (Input.GetKeyDown(KeyCode.Alpha4)) StartCoroutine(SpawnWave("veryhard", 10));
        */

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            foreach (var enemy in GameObject.FindGameObjectsWithTag("EnemyTag"))
            {
                Destroy(enemy);
            }
        }

        if (gm.running != false)
        {
            // WAVE MANAGING //
            if (GameObject.FindGameObjectsWithTag("EnemyTag").Length < 1)    // no enemies found; wave killed
            {
                Debug.Log("Wave " + currentWave + " dead!");

                currentWave++;

                // SPAWN NEW WAVE WITH DELAY
                // if we're past the threshold, start endless very hard waves. else use current wave index difficulty

                if (currentWave >= waveDifficulties.Count)
                {
                    StartCoroutine(SpawnWave("veryhard", UnityEngine.Random.Range(4, 7)));
                }

                else
                {
                   StartCoroutine(SpawnWave(waveDifficulties[currentWave], UnityEngine.Random.Range(5, 9)));
                }
            }
        }
        


            // ... otherwise keep on gaming
    }


    void SpawnEnemyOfDifficulty(string difficulty, float xPos, float yPos)    // spawns an enemy with random attributes, but biased depending on difficulty
    {
        // get properties to spawn with
        float shooterChance = 0;
        float lowerSpeed = 0;
        float upperSpeed = 0;
        float lowerFirerate = 0;
        float upperFirerate = 0;

        bool canShoot;
        float speed;
        float fireRate;


        // this sucks lol
        switch (difficulty.ToLower())
        {
            case "easy":
                shooterChance = easyShooterChance;
                lowerSpeed = easyLowerSpeed;
                upperSpeed = easyUpperSpeed;
                lowerFirerate = easyLowerFirerate;
                upperFirerate = easyUpperFirerate;

                break;


            case "medium":
                shooterChance = mediumShooterChance;
                lowerSpeed = mediumLowerSpeed;
                upperSpeed = mediumUpperSpeed;
                lowerFirerate = mediumLowerFirerate;
                upperFirerate = mediumUpperFirerate;

                break;


            case "hard":
                shooterChance = hardShooterChance;
                lowerSpeed = hardLowerSpeed;
                upperSpeed = hardUpperSpeed;
                lowerFirerate = hardLowerFirerate;
                upperFirerate = hardUpperFirerate;

                break;


            case "veryhard":
                shooterChance = veryHardShooterChance;
                lowerSpeed = veryHardLowerSpeed;
                upperSpeed = veryHardUpperSpeed;
                lowerFirerate = veryHardLowerFirerate;
                upperFirerate = veryHardUpperFirerate;

                break;
        }



        canShoot = UnityEngine.Random.Range(0, 100) < shooterChance;
        speed = UnityEngine.Random.Range(lowerSpeed, upperSpeed);
        fireRate = UnityEngine.Random.Range(lowerFirerate, upperFirerate);


        // instantiate enemy and apply these properties
        GameObject enemy = Instantiate(baseEnemyPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
        enemy.GetComponent<ChasePlayer>().InitStats(speed, canShoot, fireRate);
    }



    public IEnumerator SpawnWave(string difficulty, int amount, float delayBetween = 5f, float radius = 5f)
    {
        textelement.text = "Wave: " + getWave();
        Debug.Log("doing " + amount + " " + difficulty.ToUpper() + " spawns...");

        for (int i = 0; i < amount; i++)
        {
            float xPos = (float) UnityEngine.Random.Range(-5.7f, 5.79f);
            float yPos = (float) UnityEngine.Random.Range(-3.69f, 3f);
            //float xPos = (float) UnityEngine.Random.Range(playerObj.transform.position.x + 2f, playerObj.transform.position.x + radius);
            //float yPos = (float) UnityEngine.Random.Range(playerObj.transform.position.y + 2f, playerObj.transform.position.y + radius);

            SpawnEnemyOfDifficulty(difficulty, xPos, yPos);
            yield return new WaitForSeconds(0.05f);
        }
    }



    List<string> InitWaveDifficulties()    // creates a list of strings that can be used to "index" wave difficulties, based on numEasyWaves etc
    {
        List<string> waves = new List<string>();

        for (int i = 0; i < numEasyWaves; i++)
        {
            waves.Add("easy");
        }

        for (int i = 0; i < numMediumWaves; i++)
        {
            waves.Add("medium");
        }

        for (int i = 0; i < numHardWaves; i++)
        {
            waves.Add("hard");
        }

        // infinite very hard waves from here can be detected using waves.length
        return waves;
    }


    IEnumerator WaveDelay(float seconds) // unity :) :) :) :)
    {
        yield return new WaitForSeconds(seconds);
    }

    public int getWave()
    {
        return currentWave;
    }
}