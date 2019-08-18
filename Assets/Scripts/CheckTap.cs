using UnityEngine;
using System.Collections;

public class CheckTap : MonoBehaviour 
{
	private Camera mainCamera;
	public delegate void PowerTrigger();
	public PowerTrigger power;

	private float cooldown;
	private float powerCooldown;

	private Vector3 originalPosition;

	// Use this for initialization
	void Start () 
	{
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();

		originalPosition = gameObject.transform.localPosition;

		cooldown = 0.0f;
		ChoosePower ();
	}

	public void ChoosePower()
	{
		power = null;
		GameObject player = GameObject.FindGameObjectWithTag ("Player");

		Bash bashComponent = player.GetComponent<Bash> ();
		if (bashComponent!=null)
		{
			power = bashComponent.StartBash;
			powerCooldown = bashComponent.cooldown;
		}
	}

	private bool IsPositionInTapZone(Vector3 position)
	{
		Vector3 A = new Vector3();
		A.x = transform.position.x - transform.localScale.x / 2.0f;
		A.z = transform.position.z + transform.localScale.y / 2.0f;
		Vector3 B = new Vector3 ();
		B.x = transform.position.x + transform.localScale.x / 2.0f;
		B.z = transform.position.z - transform.localScale.y / 2.0f;
		A = mainCamera.WorldToScreenPoint (A);
		B = mainCamera.WorldToScreenPoint (B);

		if ((position.x > A.x)
		    && (position.x < B.x)
		    && (position.y < A.y)
		    && (position.y > B.y))
			return true;
		return false;
	}

	// Update is called once per frame
	void Update () 
	{
		cooldown -= Time.deltaTime;
		if (cooldown < 0.0f)
		{
			gameObject.transform.localPosition = originalPosition;
			cooldown = 0.0f;
		}
		foreach (Touch t in Input.touches)
		{
			if(IsPositionInTapZone(t.position))
			{
				if (cooldown < 0.0001f)
				{
					Debug.Log("PowerA");
					if (power != null)
					{
						power();
						cooldown = powerCooldown;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 1000.0f, gameObject.transform.localPosition.z);
					}
				}
				else
				{
					Debug.Log("PowerA is cooling down!");
				}
			}
		}
	}
}
