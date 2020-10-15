using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
	public int mouseBtn = 2;
	[Space(5)]
	public float dragSpeed = 0.05f;
	public float scrollSpeed = 1f;
	[Tooltip("How small and big the camera can become by scrolling.")]
	public Vector2 cameraSizeBounds = new Vector2(5, 10);

	private Camera cam;

	private bool moving = false;
	private Vector3 originClick;
	private Vector3 originCam;

	private void Awake()
	{
		cam = GetComponent<Camera>();
	}

	void Update()
	{

		if (Input.GetMouseButtonDown(mouseBtn))
		{
			if (moving == false)
			{
				originClick = Input.mousePosition;
				originCam = transform.position;

				//Debug.Log("Start Moving");
				moving = true;
			}
		}
		if (Input.GetMouseButton(mouseBtn))
		{
			//Debug.Log("Moving");
			transform.position = originCam + ((originClick - Input.mousePosition) * dragSpeed);
		}

		if (Input.GetMouseButtonUp(mouseBtn))
		{
			//Debug.Log("End Moving");
			moving = false;
		}


		float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
		if (scrollWheelInput != 0)
		{
			cam.orthographicSize -= scrollWheelInput * scrollSpeed;
			cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, cameraSizeBounds.x, cameraSizeBounds.y);
		}
	}
}
