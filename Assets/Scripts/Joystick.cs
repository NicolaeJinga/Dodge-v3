using UnityEngine;
using System.Collections;

public class Joystick : MonoBehaviour 
{
	[HideInInspector]
	public Vector2 value;
	private int touchIndex; 
	private Camera mainCamera;
	public GUIText JoyValue;
	private float radiusPad;
	private Vector3 padCenterSS;

	// Use this for initialization
	void Start () 
	{
		Input.multiTouchEnabled = true;
		value = new Vector2(0,0);
		value.Normalize ();
		touchIndex = -1;
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		padCenterSS = mainCamera.WorldToScreenPoint(transform.position);
		radiusPad = transform.Find("Pad").transform.localScale.x/2.0f;
		Vector3 RadiusPadVector = transform.position + new Vector3(radiusPad,0.0f,0.0f);
		radiusPad = (mainCamera.WorldToScreenPoint(RadiusPadVector) - padCenterSS).x;
	}

	private bool isInPadZone (Touch touch)
	{
		GameObject pad = transform.Find ("Pad").gameObject;
		BoxCollider collider = pad.GetComponent<BoxCollider>();

		Vector3 A = new Vector3 (transform.position.x - collider.size.x * pad.transform.localScale.x/2.0f, transform.position.y, transform.position.z + collider.size.y * pad.transform.localScale.y/2.0f);
		Vector3 B = new Vector3 (transform.position.x + collider.size.x * pad.transform.localScale.x/2.0f, transform.position.y, transform.position.z - collider.size.y * pad.transform.localScale.y/2.0f);
		A = mainCamera.WorldToScreenPoint (A);
		B = mainCamera.WorldToScreenPoint (B);

		//Debug.Log ("("+A.x+", "+A.y+"), ("+B.x+", "+B.y+")"+collider.size.x * pad.transform.localScale.x);

		if ((touch.position.x > A.x)
			&& (touch.position.x < B.x)
			&& (touch.position.y > B.y)
			&& (touch.position.y < A.y))
			return true;
		return false;
	}

	// Update is called once per frame
	void Update () 
	{
		value.x = 0;
		value.y = 0;
		touchIndex = -1;
		for (int i = 0; i < Input.touchCount; i++)
		{
			if (isInPadZone (Input.GetTouch(i)))
			{
				touchIndex = i;
				break;
			}
		}
		//Debug.Log (touchIndex);
		Vector3 valueRaw = new Vector3 ();
		if (touchIndex != -1)
		{
			valueRaw = Input.GetTouch (touchIndex).position-(Vector2)padCenterSS;
			float touchLenght = valueRaw.magnitude;
			if(touchLenght > radiusPad)
			{
				touchLenght = radiusPad;
			}
			valueRaw.Normalize();
			valueRaw *= Mathf.SmoothStep(0,1,touchLenght/radiusPad);
			//JoyValue.text = valueRaw.x + ", " + valueRaw.y;
		}
		value = valueRaw;
	}
}
