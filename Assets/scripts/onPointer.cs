using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class onPointer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    private bool isDown = false;
    private PlayerMovement playerMovement;
    private int bulletCounter;
    private bool mustReload;
 
    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        this.mustReload = playerMovement.mustReload;
    }

    void Update () {
        bulletCounter = playerMovement.bulletCounter;
        if (isDown)
        {
            if(bulletCounter > 0 && mustReload == false)
            {
                Debug.Log("1");
                playerMovement.shoot();
            }
            else if(mustReload == true)
            {
                Debug.Log("2");
                playerMovement.stopShooting();
            }
            else
            {
                Debug.Log("3");
                playerMovement.stopShooting();
            }    
        }
        else
        {
            playerMovement.stopShooting();
        }
	}

    public void OnPointerDown(PointerEventData data)
    {
        isDown = true;
    }
    public void OnPointerUp(PointerEventData data)
    {
        isDown = false;
    }
}
