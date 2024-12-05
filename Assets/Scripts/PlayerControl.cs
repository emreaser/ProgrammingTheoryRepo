using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody playerRb;
    Animator playerAnimator;
    

    float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        playerAnimator = gameObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalinput = Input.GetAxis("Horizontal");

        playerRb.AddForce(Vector3.right * speed * horizontalinput);
        playerAnimator.SetFloat("Speed_f", playerRb.velocity.magnitude);
        Debug.Log(playerRb.velocity.magnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            playerAnimator.SetTrigger("Jump_trigger");
        }

    }
}
