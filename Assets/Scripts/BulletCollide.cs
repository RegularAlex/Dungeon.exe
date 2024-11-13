using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletcollide : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BulletTag") //Checks for collision with item with the tag "BulletTag"
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
