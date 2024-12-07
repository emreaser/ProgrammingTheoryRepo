using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] ParticleSystem fireworksFX;
    SpriteRenderer playerSprite;
    Rigidbody playerRb;
    Animator playerAnimator;

    float horizontalInput;
    float speed = 5f;
    float xBound = 8.75f;
    
    public bool onAir = false;
    bool jump = false;
    bool smash = false;
    int jumpCount = 0;

    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        playerAnimator = gameObject.GetComponent<Animator>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount <= 1)
        {
            jump = true;
        }
        if (onAir && Input.GetKey(KeyCode.S))
        {
            smash = true;
        }
    }

    private void FixedUpdate()
    {
        // ABSTRACTION
        if (!MainManager.Instance.gameOver)
        {
        Move();
        Jump();
        FlipPlayerSprite();
        KeepInBounds();
        }
        if (smash)
        {
            playerRb.AddForce(Vector3.down * 20, ForceMode.Impulse);
            smash = false;
        }

    }




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            AddForceOnContactFloor();
            onAir = false;
            jumpCount = 0;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAnimator.Play("Hit");
            SpawnFireworkFX();
            MainManager.Instance.GameOver();
            Destroy(gameObject);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            playerAnimator.Play("Hit");
            SpawnFireworkFX();
            MainManager.Instance.GameOver();
            Destroy(gameObject);
        }

            
    }



    void Move()
    {
        Vector3 vel = new Vector3(0, playerRb.velocity.y, 0);

        if (horizontalInput < 0)
        {
            vel.x = -speed;
        }
        else if (horizontalInput > 0)
        {
            vel.x = speed;
        }
        else 
        { 
            vel.x = 0; 
        }

        if (!onAir)
        {
            if (horizontalInput != 0)
            {
                playerAnimator.Play("Run");
            }
            else
            {
                playerAnimator.Play("Idle");
            }
        }

        playerRb.velocity = vel;
    }

    void Jump()
    {
        if (jump) 
        { 
            jumpCount++;
            onAir = true;
            playerRb.AddForce(Vector3.up * 50, ForceMode.Impulse);
            if (jumpCount == 1)
            {
                playerAnimator.Play("Jump");
            }
            if (jumpCount == 2)
            {
                playerAnimator.Play("DoubleJump");

            }
        }

        if (onAir && Input.GetKey(KeyCode.S))
        {
            playerRb.AddForce(Vector3.down * 50);

        }
        jump = false;

    }

    void FlipPlayerSprite()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            playerSprite.flipX = true;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            playerSprite.flipX = false;
        }
    }

    void KeepInBounds()
    {
        if (gameObject.transform.position.x < -xBound)
        {
            gameObject.transform.position = (new Vector3(-xBound, transform.position.y, 0));
        }
        if (gameObject.transform.position.x > xBound)
        {
            gameObject.transform.position = (new Vector3(xBound, transform.position.y, 0));
        }
    }

    void AddForceOnContactFloor()
    {
        playerRb.AddForce(new Vector3(Input.GetAxis("Horizontal"), 0, 0) * 30, ForceMode.Impulse);
    }

    void SpawnFireworkFX()
    {
        Instantiate(fireworksFX, gameObject.transform.position, gameObject.transform.rotation);
    }
}
