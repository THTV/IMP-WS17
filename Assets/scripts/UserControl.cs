using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class UserControl : MonoBehaviour 
{
	public PlayerMovement character;

	void Awake()
	{
		character = GetComponent<PlayerMovement> ();
	}

	void FixedUpdate()
	{
		#if CROSS_PLATFORM_INPUT
		float h = Input.GetAxis ("Horizontal");
		#endif

		//character.Move (h);
	}
}