using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDrag : MonoBehaviour {

    float OffsetX;
    float OffsetY;
    Vector2 originalPosition;

    // Use this for initialization
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BeginDrag()
    {
        OffsetX = transform.position.x - Input.mousePosition.x;
        OffsetY = transform.position.y - Input.mousePosition.y;
    }
    public void OnDrag()
    {
        transform.position = new Vector2(OffsetX + Input.mousePosition.x, OffsetY + Input.mousePosition.y);
    }
    public void EndDrag()
    {
        transform.position = originalPosition;
    }

}
