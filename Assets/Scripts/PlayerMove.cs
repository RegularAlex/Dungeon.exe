using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
using TMPro;
using TMPro.EditorUtilities;


public class PlayerMove : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////
    ///
    // playerObj: doesn't need assigning, happens in code
    // BulletBehaviour: assign to a bullet prefab which has the BulletBehaviour script attatched to it
    // LaunchOffset: assign to player transform
    //
    ///////////////////////////////////////////////////////////////////////////////////////////////////////
    
    // refs
    
    public GameObject playerObj;    // nathan you don't need to assign this it happens in code <3 <3 <3 <3 <3 <3
    public GameObject GameOverObj; 

    // for bullet firing
    public BulletBehaviour BulletPrefab;
    public Transform LaunchOffset;

    // vars
    [SerializeField] float speed = 5f;    
    Vector3 wishDir = new Vector3(0, 0, 0);

    public int lives = 5;
    public GameManager gm;

    private AudioSource ASS;

    public TextMeshProUGUI textelement;


    void Start()
    {
        playerObj = this.gameObject;
        playerObj.SetActive(true);
        GameOverObj.SetActive(false);
        ASS = GetComponent<AudioSource>();
        textelement.text = "Lives: " + lives;
    }   



    void Update()
    {
        // PLAYER MOVEMENT
        // get input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");


        // apply into movement vector
        wishDir.x = gameObject.transform.position.x + (moveX * speed * Time.deltaTime);
        wishDir.y = gameObject.transform.position.y + (moveY * speed * Time.deltaTime);

        gameObject.transform.position = wishDir;    // normalize this if we don't want fast diagonal movement




        // BULLET FIRING 
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")) //Space
        {
            ASS.Play();
            Instantiate(BulletPrefab, LaunchOffset.position, LaunchOffset.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyTag") //Checks for collision with item with the tag "EnemyTag"
        {
            print("Enemy Bodied Player");
            //GameOverObj.SetActive(true); 
            TakeDamage(1);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "EnemyBulletTag")
        {
            print("Bullet Hit Player");
            Destroy(col.gameObject);
            //GameOverObj.SetActive(true);
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        lives -= damageAmount;
        textelement.text = "Lives: " + lives;

        if (lives <= 0)
        {
            print("The player is dead");
            playerObj.SetActive(false);
            gm.running = false;
            Time.timeScale = 0;
            GameOverObj.SetActive(true);
        }
    }
}


