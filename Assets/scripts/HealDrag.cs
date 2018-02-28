using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealDrag : MonoBehaviour {

    private float OffsetX;
    private float OffsetY;
    private Vector2 originalPosition;
    private PlayerMovement playerMovement;

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
        transform.position = new Vector3(OffsetX + Input.mousePosition.x, OffsetY + Input.mousePosition.y);
    }
    public void EndDrag()
    {
        transform.position = originalPosition;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Shadow")
        {
            Debug.Log("healed");
            playerMovement.curHealth += 4;
        }
    }

}
