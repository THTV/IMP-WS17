/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour {

	public const string Dragable_Tag = "DragAndDrop";

	private bool dragging = false;

	private Vector2 originalPosition;

	private Transform objectToDrag;
	private Image objectToDragImage;

	List<RaycastResult> hitObjects = new List<RaycastResult>();


	void Update () {
		if (Input.GetMouseButtonDown (0)) 
		{
			objectToDrag = GetDraggaableTransformUnderMouse ();

			if (objectToDrag != null) 
			{
				dragging = true;

				objectToDrag.SetAsLastSibling ();

				originalPosition = objectToDrag.position;
				objectToDragImage = objectToDrag.GetComponent<Image> ();
				objectToDragImage.raycastTarget = false;

			}
		}

		if (dragging) 
		{
			objectToDrag.position = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp (0)) 
		{
			if (objectToDrag != null) 
			{
				Transform objectToReplace = GetDraggaableTransformUnderMouse ();

				if (objectToReplace != null) {
					objectToDrag.position = objectToReplace.position;
					objectToReplace.position = originalPosition;
				} else 
				{
					objectToDrag.position = originalPosition;
				}

				objectToDragImage.raycastTarget = true;
				objectToDrag = null;

			}

			dragging = false;
		}


	}


	private GameObject GetObjectUnderMouse()
	{
		var pointer = new PointerEventData (EventSystem.current);

		pointer.position = Input.mousePosition;

		EventSystem.current.RaycastAll(pointer, hitObjects);

		if (hitObjects.Count <= 0) return null;

		return hitObjects.First ().gameObject;

	}

	private Transform GetDraggaableTransformUnderMouse()
	{
		GameObject clickedObject = GetObjectUnderMouse ();

		if (clickedObject != null && clickedObject.tag == Dragable_Tag) 
		{
			return clickedObject.transform;
		}

		return null;

	}

}
*/