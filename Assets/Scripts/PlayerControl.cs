using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody playerRb;
    Animator playerAnimator;

    public bool onAir = false;
    float speed = 30;
    float speedOnAir = 15;
    float speedOnFloor = 30;
    float xBound = 8.75f;

    int jumpCount = 0;
    SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        playerAnimator = gameObject.GetComponent<Animator>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Debug.Log(jumpCount);
        

        if (playerRb.velocity.magnitude < 4)
        {
            playerRb.AddForce(Vector3.right * speed * horizontalInput);
            playerAnimator.SetFloat("Speed_f", playerRb.velocity.magnitude);
            
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount <= 1)
        {
            jumpCount++;
            speed = speedOnAir;
            onAir = true;
            playerRb.AddForce(Vector3.up * 50, ForceMode.Impulse);
            if (jumpCount == 1) { 
            playerAnimator.SetTrigger("Jump_trigger");
            }
            if (jumpCount == 2)
            {
                playerAnimator.SetTrigger("Doublejump_trigger");
            }

            } 

        if (onAir && Input.GetKey(KeyCode.S))
        {
            playerRb.AddForce(Vector3.down * 50);
            
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            playerSprite.flipX = true;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            playerSprite.flipX = false;
        }
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            
            speed = speedOnFloor;
            AddForceOnContactFloor();
            onAir = false;
            jumpCount = 0;
        }
    }

    void AddForceOnContactFloor()
    {
        playerRb.AddForce(new Vector3(Input.GetAxis("Horizontal"), 0, 0) * 30, ForceMode.Impulse);

    }

    void JumpCount()
    {
        jumpCount++;
        if (jumpCount > 1)
        {
            jumpCount = 0;
        }
    }

    void KeepInBounds()
    {
        if (gameObject.transform.position.x < -xBound)
        {
            gameObject.transform.Translate(new Vector3(-xBound, transform.position.y, 0));
        }
        if (gameObject.transform.position.x > xBound)
        {
            gameObject.transform.Translate(new Vector3(xBound, transform.position.y, 0));
        }
    }
}
