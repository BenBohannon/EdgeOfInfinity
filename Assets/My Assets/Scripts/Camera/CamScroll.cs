using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CamScroll : MonoBehaviour {

    private Camera myCamera;
    private Rect screenRect;

    public float scrollSpeed = 0.2f;

	// Use this for initialization
	void Start () {
        myCamera = GetComponent<Camera>();
        screenRect = new Rect(0, 0, Screen.width, Screen.height);

	}
	
	// Update is called once per frame
	void Update () {

        if (!screenRect.Contains(Input.mousePosition))
        {
            return;
        }

        Vector2 mousePos = myCamera.ScreenToViewportPoint(Input.mousePosition);

	    if (mousePos.x < 0.05)
        {
            Vector3 currentPos = transform.position;
            transform.position = new Vector3(currentPos.x - scrollSpeed, currentPos.y, currentPos.z);
        }
        else if (mousePos.x > 0.95)
        {
            Vector3 currentPos = transform.position;
            transform.position = new Vector3(currentPos.x + scrollSpeed, currentPos.y, currentPos.z);
        }

        if (mousePos.y < 0.05)
        {
            Vector3 currentPos = transform.position;
            transform.position = new Vector3(currentPos.x, currentPos.y - scrollSpeed, currentPos.z);
        }
        else if (mousePos.y > 0.95)
        {
            Vector3 currentPos = transform.position;
            transform.position = new Vector3(currentPos.x, currentPos.y + scrollSpeed, currentPos.z);
        }
	}
}
