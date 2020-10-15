using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class PointerHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    

    public void PointerClick(object sender, PointerEventArgs e) {
        if (e.target.tag == "Button") {
            Debug.Log("Button was clicked");
        }
        else if (e.target.tag == "Switch") {
            Debug.Log("Button was switched");
            e.target.GetComponent<SwitchState>().state = !e.target.GetComponent<SwitchState>().state;
        }
    }

    public void PointerInside(object sender, PointerEventArgs e) {
        if (e.target.tag == "Button") {
            Debug.Log("Button was entered");
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e) {
        if (e.target.tag == "Button") {
            Debug.Log("Button was exited");
        }
    }

    void Update() {
        if (GameObject.FindWithTag("Player").activeSelf) {
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast (ray, out hit, 100.0f)) {
                    if (hit.transform.tag == "Button") {
                        Debug.Log("Button was clicked");
                    }
                    else if (hit.transform.tag == "Switch") {
                        Debug.Log("Button was switched");
                        hit.transform.gameObject.GetComponent<SwitchState>().state = !hit.transform.gameObject.GetComponent<SwitchState>().state;
                    }
                }
            }
        }
    }
}