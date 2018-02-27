using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZoneChecker : MonoBehaviour
{

    public bool isEnemyInZone;

    private void Start()
    {
        isEnemyInZone = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Debug.Log("say Hi");
        }
    }
}