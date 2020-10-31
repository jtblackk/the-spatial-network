/* 
 * author : jiankaiwang
 * description : The script provides you with basic operations 
 *               of first personal camera look on mouse moving.
 * platform : Unity
 * date : 2017/12
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Lock up and down look to 90 degrees

public class MouseLook : MonoBehaviour {

    // [SerializeField]
    // public float sensitivity = 5.0f;
    // [SerializeField]
    // public float smoothing = 2.0f;
    // // the chacter is the capsule
    // public GameObject character;
    // // get the incremental value of mouse moving
    // private Vector2 mouseLook;
    // // smooth the mouse moving
    // private Vector2 smoothV;

	// Use this for initialization

    public float mouseSensitivity = 100f;
    public Transform playerBody;

    float xRotation = 0f;

	void Start () {
        // hide and lock cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        // // md is mosue delta
        // var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        // md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        // // the interpolated float result between the two float values
        // smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        // smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        // // incrementally add to the camera look
        // mouseLook += smoothV;

        // // vector3.right means the x-axis
        // transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        // character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        // character.transform.Rotate(Vector3.)
    }
}



