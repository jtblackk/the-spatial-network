using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopThrowable : MonoBehaviour
{
    bool held = false;

    GameObject packetObject;
    GameObject player;
    Vector3 playerPos;

    private UDPInfo udpStore;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!held) {
            
            if (GameObject.FindWithTag("Player")) {
                player = GameObject.FindWithTag("Player");
                if (player.activeSelf) {
                    if (Input.GetMouseButtonDown(0) && !held) {
                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast (ray, out hit, 100.0f)) {
                            if (hit.transform.tag == "UDPThrowable") {
                                packetObject = hit.transform.gameObject;
                                packetObject.GetComponent<Rigidbody>().useGravity = false;
                                held = true;
                            }
                        }
                    }
                }
            }
        }
        else {
            packetObject.GetComponent<UDPInfo>().showPanel();
            holdObject(packetObject, player);
        }
    }

    void holdObject(GameObject heldObject, GameObject holder) {
        playerPos = player.transform.position + player.transform.forward*2.0f;
        if (Input.GetMouseButton(0)) {
            heldObject.transform.position = new Vector3(playerPos.x, playerPos.y, playerPos.z);
            heldObject.transform.rotation = Quaternion.Euler(0f, 1f, 0f);
        }
        else {
            heldObject.GetComponent<UDPInfo>().hidePanel();
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            held = !held;
        }
    }
}