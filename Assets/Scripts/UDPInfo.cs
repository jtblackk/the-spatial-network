using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UDPInfo : MonoBehaviour
{
    public char payload;
    public string destination;
    public int port;

    private GameObject panel;

    TextMeshPro payloadText;
    TextMeshPro destText;
    TextMeshPro portText;
    
    // Start is called before the first frame update
    void Start()
    {
        panel = this.transform.GetChild(6).gameObject;
        payloadText = panel.transform.GetChild(0).GetComponent<TextMeshPro>();
        portText = panel.transform.GetChild(1).GetComponent<TextMeshPro>();
        destText = panel.transform.GetChild(2).GetComponent<TextMeshPro>();
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // enable game object and update variables
    public void showPanel() {
        payloadText.text = "Payload: " + payload;
        destText.text = "Dest: " + destination;
        portText.text = "Port: " + port;
        panel.SetActive(true);
    }

    public void hidePanel() {
        panel.SetActive(false);
    }
}
