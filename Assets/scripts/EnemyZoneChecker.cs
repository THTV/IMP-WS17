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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            isEnemyInZone = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(isEnemyInZone);
        if(collision.tag == "Fireball" && isEnemyInZone == true)
        {
            Debug.Log("aua");
        }
    }
}