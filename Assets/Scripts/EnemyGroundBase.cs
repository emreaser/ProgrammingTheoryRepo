using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
//Parent class of ground enemies
public class EnemyGroundBase : EnemyBase
{
    public float speed;

    protected Vector3 globalForward = Vector3.forward;
    protected Vector3 relativeRight; 
    
    void Start()
    {
        FindRelativeRight();
        
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (!MainManager.Instance.gameOver)
        {
        Move();
        DestroyOutOfBounds();
        }
    }

    // POLYMORPHISM
    public override void Move()
    {
        gameObject.transform.Translate(-relativeRight * Time.deltaTime * speed);
    }

    protected void FindRelativeRight()
    {
        relativeRight = new Vector3(globalForward.z, 0, -globalForward.x).normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Die();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Die();
        }
    }
}
