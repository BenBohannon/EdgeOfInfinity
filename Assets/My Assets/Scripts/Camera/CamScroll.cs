using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CamScroll : MonoBehaviour {

    private Camera myCamera;
    private Rect screenRect;

    public float scrollSpeed = 0.2f;
    public float heightMax = 50.0f;
    public float heightMin = -50.0f;
    public float widthMax = 50.0f;
    public float widthMin = -50.0f;

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
            if (currentPos.x > widthMin)
            {
                transform.position = new Vector3(currentPos.x - scrollSpeed, currentPos.y, currentPos.z);
            }
        }
        else if (mousePos.x > 0.95)
        {
            Vector3 currentPos = transform.position;
            if (currentPos.x < widthMax)
            {
                transform.position = new Vector3(currentPos.x + scrollSpeed, currentPos.y, currentPos.z);
            }
        }

        if (mousePos.y < 0.05)
        {
            Vector3 currentPos = transform.position;
            if (currentPos.y > heightMin)
            {
                transform.position = new Vector3(currentPos.x, currentPos.y - scrollSpeed, currentPos.z);
            }
        }
        else if (mousePos.y > 0.95)
        {
            Vector3 currentPos = transform.position;
            if (currentPos.y < heightMax)
            {
                transform.position = new Vector3(currentPos.x, currentPos.y + scrollSpeed, currentPos.z);
            }
        }
	}
}
