using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    //public float bulletspeed = 15.0f;
    //public GameObject GameOverScreen;
    private GameObject player;
    private Rigidbody2D rb;
    private float timer = 0.0f;

    public float timeAmount = 100f;
    public float force; 

    // Start is called before the first frame update
    void Start()
    {
        //Targets the bullet
        //GameOverScreen.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("PlayerTag");

        //Shoot the bullet
        Vector3 Direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(Direction.x,Direction.y).normalized * force;

        //Allows for bullets to be shot allround the enemy
        float rotation = Mathf.Atan2(-Direction.y, -Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += -transform.right * Time.deltaTime * force;

        timer += Time.deltaTime;
        if (timer > 100)
        {
            Destroy(gameObject);
        }
    }
}

