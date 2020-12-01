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
    


    public string srcIP;
    public string destIP;
    
    public int srcPort;
    public int destPort;

    public int length;
    public string checksum;

    public string data;

    TextMeshPro srcIPTxt;
    TextMeshPro destIPTxt;

    TextMeshPro srcPortTxt;

    TextMeshPro destPortTxt;

    TextMeshPro lengthTxt;

    TextMeshPro checksumTxt;

    TextMeshPro dataTxt;





    // Start is called before the first frame update
    void Start()
    {
        // panel = this.transform.GetChild(6).gameObject;
        // payloadText = panel.transform.GetChild(0).GetComponent<TextMeshPro>();
        // portText = panel.transform.GetChild(1).GetComponent<TextMeshPro>();
        // destText = panel.transform.GetChild(2).GetComponent<TextMeshPro>();
        // panel.SetActive(false);
        
        srcIPTxt = GameObject.Find("source ip").GetComponent<TextMeshPro>();
        destIPTxt = GameObject.Find("dest ip").GetComponent<TextMeshPro>();
        srcPortTxt = GameObject.Find("source port").GetComponent<TextMeshPro>();
        destPortTxt = GameObject.Find("dest port").GetComponent<TextMeshPro>();
        lengthTxt = GameObject.Find("length").GetComponent<TextMeshPro>();
        checksumTxt = GameObject.Find("checksum").GetComponent<TextMeshPro>();
        dataTxt = GameObject.Find("data").GetComponent<TextMeshPro>();


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // enable game object and update variables
    public void showPanel() {
        // payloadText.text = "Payload: " + payload;
        // destText.text = "Dest: " + destination;
        // portText.text = "Port: " + port;
        // panel.SetActive(true);


        destIP = destination;
        srcPort = port;
        data = payload.ToString();


        // srcIPTxt.text = srcIP;
        srcIPTxt.text = srcIP;
        destIPTxt.text = destIP;
        srcPortTxt.text = srcPort.ToString();
        destPortTxt.text = destPort.ToString();
        lengthTxt.text = length.ToString();
        checksumTxt.text = checksum;
        dataTxt.text = data;       
    }

    public void hidePanel() {
        // panel.SetActive(false);
    }
}
