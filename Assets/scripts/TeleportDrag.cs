using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDrag : MonoBehaviour {

    private float OffsetX;
    private float OffsetY;
    private Vector2 originalPosition;
    private PlayerMovement playerMovement;
    public GameObject player;
    private Vector2 newPosition;

    // Use this for initialization
    void Start()
    {
        originalPosition = transform.position;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        
    }

    public void BeginDrag()
    {
        OffsetX = Input.mousePosition.x - transform.position.x; 
        OffsetY = Input.mousePosition.y - transform.position.y;
    }
    public void OnDrag()
    {
        newPosition = new Vector2(OffsetX + Input.mousePosition.x, OffsetY + Input.mousePosition.y);
        transform.position = newPosition;
    }
    public void EndDrag()
    {
        player.transform.position = new Vector2(newPosition.x, 0);
        transform.position = originalPosition;
    }
}
