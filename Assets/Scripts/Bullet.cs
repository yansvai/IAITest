using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Bullet Behaviour
/// </summary>
public class Bullet : MonoBehaviour
{
  
    private void OnTriggerEnter(Collider Enemy)
    {
        if (Enemy.gameObject.layer == 9 && Enemy.gameObject.GetComponent<Rigidbody>())
        {
            Enemy.gameObject.GetComponent<Enemy>().Die();
        
        }
    }

  


}
