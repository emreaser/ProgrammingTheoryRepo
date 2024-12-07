using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// INHERITANCE
public class EnemyFlying : EnemyBase
{
    [SerializeField] ParticleSystem explodeFX;
    GameObject player;
    Rigidbody rb;
    float speed;

    void Start()
    {
        player = GameObject.Find("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        rb.mass = Random.Range(5, 8);
        rb.drag = Random.Range(5, 8);
        speed = Random.Range(25, 40);
        
    }
   
    private void FixedUpdate()
    {
        Move();
    }

    // POLYMORPHISM
    public override void Move()
    {
        Vector3 direction;

        if (player != null)
        {
            direction = player.transform.position - transform.position;
            direction = new Vector3(direction.x, 0, 0);
            rb.AddForce(direction * speed);
        }
    }

    public override void Die()
    {
        MainManager.Instance.aliveFlyingEnemyCount--;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) 
        { 
        Instantiate(explodeFX, transform.position, transform.rotation);
        Die();
        }
    }

}
