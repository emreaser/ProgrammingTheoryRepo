using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

// INHERITANCE
//Parent Class of Enemy
public abstract class EnemyBase : MonoBehaviour
{
    public float heightPos;

    // POLYMORPHISM
    public abstract void Move();


    public virtual void DestroyOutOfBounds()
    {
        if (gameObject.transform.position.x < -12 || gameObject.transform.position.x > 12)
        {
            MainManager.Instance.aliveEnemyCount--;
            Destroy(gameObject);
        }
        
    }

    public virtual void DealDamage()
    {

    }

    public virtual void Die()
    {
        MainManager.Instance.aliveEnemyCount--;
        Destroy(gameObject);
    }

    

}
