using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

// INHERITANCE
public class EnemyGorundHopping : EnemyGroundBase
{
    Rigidbody bunnyRb;
    bool hop = true;

    void Start()
    {
        FindRelativeRight();
        bunnyRb = GetComponent<Rigidbody>();
        StartCoroutine("Hop");
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

        if (hop)
        {
            StartCoroutine("Hop");
        }
    }

    IEnumerator Hop()
    {
        hop = false;
        yield return new WaitForSeconds(1.5f);
        bunnyRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        hop = true;
    }
}
