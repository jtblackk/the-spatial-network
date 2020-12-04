using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random=UnityEngine.Random;
using TMPro;
using System.Linq;
using UnityEngine.UIElements;

public class ClientController : MonoBehaviour
{   
    public enum socketType {None, TCP, UDP};
    private socketType activeSocketType;

    private ServerController server;

    private int activePort;
    private int sourcePort;
    private string destPort;
    private Vector3 originalSocketPos;
    private float spaceBetweenPorts = .505f;
    public GameObject activeSocketObject;

    public enum state {Closed, Created, Binding, Bound, Transmitting, Transmitted};
    public state activeSocketState;

    public string numpadBuffer;
    public bool bufferReadyToRead;
    public TextMeshPro keypadData;

    public GameObject packet;
    private Vector3 start, target;
    private float speed = 0.8f;
    private int input;
    private GameObject p;

    bool autoSendToggled;
    bool payloadIndexed;
    
    public List<GameObject> checkmarks;

    public void Start() {
        this.originalSocketPos = this.activeSocketObject.transform.localPosition;
        server = GameObject.FindWithTag("Server").GetComponent<ServerController>();
        
        // GameObject checkmark = GameObject.Find("Task (client create)/Checkmark");
        checkmarks = new List<GameObject>();
        GameObject[] objs = FindObjectsOfType<GameObject>();
        foreach(GameObject obj in objs) {
            if(obj.name.Contains("Checkmark (c")) {
                // Debug.Log("adding " + obj.name + "to the list");
                checkmarks.Add(obj);
                obj.SetActive(false);
            }
        }
        // checkmarks.Reverse();
        // Debug.Log(checkmarks);
        // Debug.Log("client controller loaded");

    }

    // shared functions

   public IEnumerator createSocket() {
       // if there's already an active socket, send an error message
       if(this.activeSocketState != state.Closed) {
            ClientInstructions.screen.text = "A socket is already in use. No need to create another one on this module.";
            yield break;
       }
       
        
        // ask user which type of socket to create (TCP or UDP)
        // string numpadInput;
        // do {
        //     ClientInstructions.screen.text = "Choose a socket type\n\n On the keypad\npress 1 (UDP) or 2 (TCP)\n\npress enter when finished";

        //     // clear the numpad buffer
        //     this.clearNumpadBuffer();

        //     // wait for the numpad buffer to be ready to read
        //     while(this.bufferReadyToRead != true) {
        //         yield return null;
        //     }

        //     // record and clear the buffer inputs
        //     numpadInput = this.clearNumpadBuffer();

        // } while(numpadInput != "1" && numpadInput != "2");


        // make the socket visible, update appropriate status variables
        // if (numpadInput == "1") {
        //     this.activeSocketType = socketType.UDP;
        //     this.activeSocketObject.transform.Find("UDP Socket").gameObject.SetActive(true);
        //     this.activeSocketObject.transform.Find("TCP Socket").gameObject.SetActive(false);
        // }        
        // else {
        //     this.activeSocketType = socketType.TCP;
        //     this.activeSocketObject.transform.Find("UDP Socket").gameObject.SetActive(false);
        //     this.activeSocketObject.transform.Find("TCP Socket").gameObject.SetActive(true);
        // }
        
        this.activeSocketType = socketType.UDP;
        this.activeSocketObject.transform.Find("UDP Socket").gameObject.SetActive(true);
        this.activeSocketObject.transform.Find("TCP Socket").gameObject.SetActive(false);
        this.activeSocketState = state.Created;


        // present creation feedback
        ClientInstructions.screen.text = "created a new " + this.activeSocketType + " socket \n";
        foreach(GameObject obj in checkmarks) {
            if(obj.name == "Checkmark (cc)") {
               obj.SetActive(true);
            }
        }

   }

    public IEnumerator bindSocket() {
        // check that a socket has been created but hasn't been bound
        if(this.activeSocketState != state.Created) {
            if(this.activeSocketState == state.Bound) {
                ClientInstructions.screen.text = "ERROR: already bound a port on the module";
                yield break;
            }
            ClientInstructions.screen.text = "ERROR: must create a socket before binding it";
            yield break;
        }

        this.activeSocketState = state.Binding;

        
        // ask user to select a port to bind to
        string numpadInput;
        do {
            this.clearNumpadBuffer();

            // wait for the numpad buffer to be ready to read
            
            while(this.bufferReadyToRead != true) {
                ClientInstructions.screen.text = "enter a port to bind to\n(1-4 on keypad)\n\npress enter when finished";
                yield return null;
            }
            
            numpadInput = this.clearNumpadBuffer();
        } while(Int32.Parse(numpadInput) < 1 || Int32.Parse(numpadInput) > 4);

        if(this.activeSocketState != state.Binding) {
            yield break;
        }

        // bind the socket, update appropriate values
        this.activePort = Int32.Parse(numpadInput);
        this.sourcePort = Int32.Parse(numpadInput);
        this.activeSocketState = state.Bound;

        // make socket tube protrude
        // a: move socket object to the correct port hole
        this.activeSocketObject.transform.Translate(Vector3.back * (this.activePort - 1) * this.spaceBetweenPorts, Space.World);
        
        // b. hide the port cover of the current port
        this.transform.Find("Client Ports").transform.Find("Port Cover " + this.activePort).gameObject.SetActive(false);

        // c: move the socket object along the x axis
        while(this.originalSocketPos.x - this.activeSocketObject.transform.localPosition.x < .309f) {
            this.activeSocketObject.transform.Translate(Vector3.left * .001f);
            yield return null;
        }

        // present bind feedback        
        ClientInstructions.screen.text = "bound the socket to port " + this.activePort;
        foreach(GameObject obj in checkmarks) {
            if(obj.name == "Checkmark (cb)") {
               obj.SetActive(true);
            }
        }
    }

    public IEnumerator bindSocketRandom() {
        // bind the socket, update appropriate values
        this.activePort = (int)Random.Range(1, 4);
        // this.activeSocketState = state.Bound;

        // make socket tube protrude
        // a: move socket object to the correct port hole
        this.activeSocketObject.transform.Translate(Vector3.back * (this.activePort - 1) * this.spaceBetweenPorts, Space.World);
        
        // b. hide the port cover of the current port
        this.transform.Find("Client Ports").transform.Find("Port Cover " + this.activePort).gameObject.SetActive(false);

        // c: move the socket object along the x axis
        while(this.originalSocketPos.x - this.activeSocketObject.transform.localPosition.x < .309f) {
            this.activeSocketObject.transform.Translate(Vector3.left * .001f);
            yield return null;
        }
        this.activeSocketState = state.Bound;
    }

    public IEnumerator closeSocket()
    {   
        // check that there's a socket to close
        if(this.activeSocketState == state.Closed) {
            ClientInstructions.screen.text = "ERROR: called close without any sockets opened";
            // Debug.Log("CLIENT: \"ERROR: called close without any sockets opened\"");
            yield break;
        }
        else if(this.activeSocketState == state.Created) {
            // update appropriate status variables
            this.activeSocketState = state.Closed;
            this.activeSocketType = socketType.None;
            int closedPort = this.activePort;
            this.activePort = 0;
            
            // present feedback
            ClientInstructions.screen.text = "closed socket";
        }
        else if (this.activeSocketState == state.Bound) {

            // update appropriate status variables
            this.activeSocketState = state.Closed;
            this.activeSocketType = socketType.None;
            int closedPort = this.activePort;
            this.activePort = 0;
            
            // present feedback
            ClientInstructions.screen.text = "closed socket on port " + closedPort;

            // do close transformations
            // a. retreat socket back into port box
            while(this.originalSocketPos.x - this.activeSocketObject.transform.localPosition.x > 0) {
                this.activeSocketObject.transform.Translate(Vector3.right * .001f);
                yield return null;
            }

            // b. show port cover
            this.transform.Find("Client Ports").transform.Find("Port Cover " + closedPort).gameObject.SetActive(true);

            // c. move socket back to port 1
            this.activeSocketObject.transform.Translate(Vector3.forward * (closedPort - 1) * this.spaceBetweenPorts, Space.World);
            
            // d. hide socket
            this.activeSocketObject.transform.Find("UDP Socket").gameObject.SetActive(false);
            this.activeSocketObject.transform.Find("TCP Socket").gameObject.SetActive(false);

        }

        // mark off the checklist
        foreach(GameObject obj in checkmarks) {
            if(obj.name == "Checkmark (ccl)") {
                obj.SetActive(true);
            }
        }

    }



    // helper: clears the numpad buffer and blocks reading from it
    // returns the original contents of the buffer
    private string clearNumpadBuffer() {
        string contents = this.numpadBuffer;
        this.numpadBuffer = "";
        keypadData.text = this.numpadBuffer;
        this.bufferReadyToRead = false;

        return contents;
    }

    public void numpad(char key) {
        // pressed enter key--mark buffer as ready to read from
        if(key == 'e') {
            this.bufferReadyToRead = true;
        }
        // pressed backspace key--remove the last character from numpad buffer
        else if (key == 'b') {
            if(this.numpadBuffer.Length > 0) {
                this.bufferReadyToRead = false;
                this.numpadBuffer = this.numpadBuffer.Remove(this.numpadBuffer.Length - 1, 1);
                keypadData.text = this.numpadBuffer;
            }
        }
        // pressed a character key, add key to the buffer
        else {
            this.bufferReadyToRead = false;
            this.numpadBuffer += key;
            keypadData.text = this.numpadBuffer;
        }
    }

    public void resetModules()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // client functions

    // TODO:
    // Might want to rename function to createPacket, split up into assigning data to packet
    // and creation and travel of the packet object itself.
    public IEnumerator sendData()
    {
        if(this.activeSocketState != state.Created && this.activeSocketState != state.Bound) {
            ClientInstructions.screen.text = "ERROR: you need to create a socket object before you can send data.";
            yield break;
        }

        state prev_socket_state = this.activeSocketState;
        this.activeSocketState = state.Transmitting;

        // retrieve the ip address and port to send data to by using the numpad.
        // TODO: this should only be done once if using auto-send
        string numpadInput;
        do {
            
            this.clearNumpadBuffer();

            // wait for the numpad buffer to be ready to read
            while(this.bufferReadyToRead != true) {
                ClientInstructions.screen.text = "enter the ip address of the module to send data to by using the keypad\n\n press enter when finished";
                yield return null;
            }
            
            numpadInput = this.clearNumpadBuffer();
        } while(numpadInput != "192.168.0.2");
        string destIP = numpadInput;

        do {
            
            this.clearNumpadBuffer();

            // wait for the numpad buffer to be ready to read
            while(this.bufferReadyToRead != true) {
                ClientInstructions.screen.text = "enter a port to send the data to (1-4 on keypad)\n\npress enter when finished";;
                yield return null;
            }
            
            numpadInput = this.clearNumpadBuffer();
        } while(Int32.Parse(numpadInput) < 1 || Int32.Parse(numpadInput) > 4);
        destPort = numpadInput;
        
        if(this.activeSocketState != state.Transmitting) {
            yield break;
        }
        else {
            this.activeSocketState = prev_socket_state;
        }

        // bind the client socket to a random port if the user did not bind it
        if(activeSocketState != state.Bound) {
            StartCoroutine(bindSocketRandom());
            while(activeSocketState != state.Bound) {
                yield return null;
            }
        }


        // Decides which socket, or path
        // Needs more looking into how we decide ports
        input = 1;
        switch (input)
        {
            case 1:
                start = GameObject.Find("Client Area/Client Ports/CSocket").transform.position;
                target = GameObject.Find("Server Area/Server Ports/SSocket").transform.position;
                break;
        }

        p = Instantiate(this.packet, start, Quaternion.identity);
        p.GetComponent<UDPInfo>().payload = (char)('A' + Random.Range (0,26));
        p.GetComponent<UDPInfo>().data = "" + p.GetComponent<UDPInfo>().payload;
        p.GetComponent<UDPInfo>().port = activePort;
        p.GetComponent<UDPInfo>().destPort = activePort;
        p.GetComponent<UDPInfo>().srcPort = sourcePort;
        p.GetComponent<UDPInfo>().length = 1;
        p.GetComponent<UDPInfo>().destination = "192.168.0.2";
        p.GetComponent<UDPInfo>().destIP = "192.168.0.2";
        p.GetComponent<UDPInfo>().srcIP = "192.168.0.1";
        p.GetComponent<UDPInfo>().showPanel();
        //p.name = "tcp_model";
        //p = GameObject.Find("tcp_model");

        // TODO: Destroy based on colliding with the destination, not on time.
        Destroy(p, 12);

        Vector3 scaleChange = new Vector3(75f, 75f, 75f);

        p.transform.localScale = scaleChange;
        p.transform.Rotate(0f, 0f, 90f, Space.Self);

        ClientInstructions.screen.text = "Sent data";
      
        foreach(GameObject obj in checkmarks) {
            if(obj.name == "Checkmark (cs)") {
               obj.SetActive(true);
            }
        }
        // Debug.Log("CLIENT: \"sendData stub\"");
    }

    public void sendPacket() {
        // TODO: use System.Linq to get the actual tail of the list so the payload is not
        // added repeatedly.
        if (p != null) {
            char payload = p.GetComponent<UDPInfo>().payload;
            int tail = server.dataReceived.Count;

            if (this.activePort == 0) p.GetComponent<Rigidbody>().useGravity = true;
            p.transform.position = Vector3.MoveTowards(p.transform.position, target, Time.deltaTime * speed);
            if (p.GetComponent<PacketCollider>().received && server.activePort == int.Parse(this.destPort)) {
                // TODO: lots of conditionals to check each frame,
                // definitely a better way to insert once while collisions occur,
                // likely from the PacketCollider script.
                if (tail.Equals(0)) {
                    Debug.Log("Adding payload to server");
                    server.dataReceived.Add(payload);
                    Destroy(p);
                }
                else {
                    if (server.dataReceived[tail-1] != payload) {
                        Debug.Log("Adding payload to server");
                        server.dataReceived.Add(payload);
                        Destroy(p);
                    }
                }
            }
        }
    }
    
    private void Update()
    {
        sendPacket();
    }

    public void toggleAutoSend()
    {   
        autoSendToggled = !autoSendToggled;
        if(autoSendToggled) ClientInstructions.screen.text = "auto-send enabled";
        else ClientInstructions.screen.text = "auto-send disabled";
        StartCoroutine(autoSend());
        Debug.Log("CLIENT: \"toggleAutoSend stub\"");
    }

    public IEnumerator autoSend() {
        while (autoSendToggled) {
            sendData();
            sendPacket();
            yield return new WaitForSeconds(13);
        }
    }

    public void connectToServer()
    {
        ClientInstructions.screen.text = "ERROR: no need to connect when using UDP. Only on TCP.";
    }
    
    // server functions
    public void listenForConnection()
    {
        ClientInstructions.screen.text = "ERROR: listening is only done on a TCP server.";

    }

    public void acceptConnection()
    {
        ClientInstructions.screen.text = "ERROR: accepting is only done on a TCP server.";
    }

    public void receiveData()
    {
        ClientInstructions.screen.text = "ERROR: only the server receives data";

    }

    public void toggleAutoReceive()
    {
        ClientInstructions.screen.text = "ERROR: only the server receives data";
    }

    
}
