using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public bool shootingallowed = false; 
    //public Transform target; 
    //public float timeinbetween = 5000f; //Firing a Bullet Every 5 Seconds.
    //public float bulletshot = 15.0f;
    //public float lastshot; 
    public GameObject EnemyBullet;
    public Transform EnemyOffset;
    private float timer;

    private GameObject player;
    public float speed;

    private float distance; 

    public float fireRate = 1f;

    private AudioSource AudioS;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerTag");
        //AudioS.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 Direction = player.transform.position - transform.position;
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        timer += Time.deltaTime;
        if (shootingallowed == true) //Allows for filtering between who can and cannot shoot for enemies. 
        {
            if (timer > fireRate)
            {
                timer = 0;
                shoot();
            }
        }

    }

    void shoot()
    {
        //AudioS.Play();
        Instantiate(EnemyBullet,EnemyOffset.position, Quaternion.identity);
    }

    public void InitStats(float _speed = 5f, bool _canShoot = true, float _fireRate = 1f)    // FOR WAVE SPAWNER will get called with every enemy instantiation. doing this so the wave spawner can assign enemy types
    {
        this.speed = _speed;
        this.shootingallowed = _canShoot;
        this.fireRate = _fireRate;
    }
}