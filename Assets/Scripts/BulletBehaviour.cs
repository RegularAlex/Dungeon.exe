using System.Runtime.CompilerServices;
using UnityEngine;


public class BulletBehaviour : MonoBehaviour
{
    public float bulletspeed = 100.0f;


    void Start()
    {
        Destroy(gameObject, 3.0f);
    }


    private void Update()
    {
        transform.position -= (-transform.up * Time.deltaTime * bulletspeed);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
