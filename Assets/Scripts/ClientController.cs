using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random=UnityEngine.Random;

public class ClientController : MonoBehaviour
{   
    public enum socketType {None, TCP, UDP};
    public socketType activeSocketType;

    public ServerController server;

    public int activePort;
    private Vector3 originalSocketPos;
    private float spaceBetweenPorts = .505f;
    public GameObject activeSocketObject;

    public enum state {Closed, Created, Bound, Transmitting};
    public state activeSocketState;

    public string numpadBuffer;
    public bool bufferReadyToRead;

    public GameObject packet;
    private Vector3 start, target;
    private float speed = 0.5f;
    private int input;
    private GameObject p;

    bool autoSendToggled;
    bool payloadIndexed;
    

    public void Start() {
        this.originalSocketPos = this.activeSocketObject.transform.localPosition;
        server = GameObject.FindWithTag("Server").GetComponent<ServerController>();
    }

    // shared functions

   public IEnumerator createSocket() {
       // if there's already an active socket, send an error message
       if(this.activeSocketState != state.Closed) {
            ClientInstructions.screen.text = "CLIENT ERROR: A socket is already in use. No need to create another one on this module.";
            Debug.Log("CLIENT: \"ERROR: A socket is already in use. No need to create another one on this module.\"");
            yield break;
       }
       
        
        // ask user which type of socket to create (TCP or UDP)
        string numpadInput;
        do {
            ClientInstructions.screen.text = "CLIENT: Choose a socket type: (1) UDP (2) TCP";
            Debug.Log("CLIENT: \"Choose a socket type: (1) UDP (2) TCP\"");

            // clear the numpad buffer
            this.clearNumpadBuffer();

            // wait for the numpad buffer to be ready to read
            while(this.bufferReadyToRead != true) {
                yield return null;
            }

            // record and clear the buffer inputs
            numpadInput = this.clearNumpadBuffer();

        } while(numpadInput != "1" && numpadInput != "2");


        // make the socket visible, update appropriate status variables
        if (numpadInput == "1") {
            this.activeSocketType = socketType.UDP;
            this.activeSocketObject.transform.Find("UDP Socket").gameObject.SetActive(true);
            this.activeSocketObject.transform.Find("TCP Socket").gameObject.SetActive(false);
        }        
        else {
            this.activeSocketType = socketType.TCP;
            this.activeSocketObject.transform.Find("UDP Socket").gameObject.SetActive(false);
            this.activeSocketObject.transform.Find("TCP Socket").gameObject.SetActive(true);
        }
        this.activeSocketState = state.Created;


        // present creation feedback
        ClientInstructions.screen.text = "CLIENT: createSocket() created a new " + this.activeSocketType + " socket";
        Debug.Log("CLIENT: \"createSocket() created a new " + this.activeSocketType + " socket\"");

   }

    public IEnumerator bindSocket() {
        // check that a socket has been created but hasn't been bound
        if(this.activeSocketState != state.Created) {
            if(this.activeSocketState == state.Bound) {
                ClientInstructions.screen.text = "CLIENT ERROR: already bound a port on the module";
                Debug.Log("CLIENT: \"ERROR: already bound a port on the module\"");
                yield break;
            }
            ClientInstructions.screen.text = "CLIENT ERROR: must create a socket before binding it";
            Debug.Log("CLIENT: \"ERROR: must create a socket before binding it\"");
            yield break;
        }

        // ask user to select a port to bind to
        string numpadInput;
        do {
            ClientInstructions.screen.text = "CLIENT: Enter a port to bind to (1-4)";
            Debug.Log("CLIENT: \"Enter a port to bind to (1-4)\"");

            
            this.clearNumpadBuffer();

            // wait for the numpad buffer to be ready to read
            while(this.bufferReadyToRead != true) {
                yield return null;
            }
            
            numpadInput = this.clearNumpadBuffer();
        } while(Int32.Parse(numpadInput) < 1 || Int32.Parse(numpadInput) > 4);

        // bind the socket, update appropriate values
        this.activePort = Int32.Parse(numpadInput);
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
        ClientInstructions.screen.text = "CLIENT: bindSocket() bound the client socket to port " + this.activePort;
        Debug.Log("CLIENT: \"bindSocket() bound the client socket to port " + this.activePort + "\"");
    }

    public IEnumerator closeSocket()
    {   
        // check that there's a socket to close
        if(this.activeSocketState == state.Closed) {
            ClientInstructions.screen.text = "CLIENT ERROR: called close without any sockets opened";
            Debug.Log("CLIENT: \"ERROR: called close without any sockets opened\"");
            yield break;
        }

        // update appropriate status variables
        this.activeSocketState = state.Closed;
        this.activeSocketType = socketType.None;
        int closedPort = this.activePort;
        this.activePort = 0;

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

        // return socket tube to original position
        ClientInstructions.screen.text = "CLIENT: closeSocket() closed client socket on port " + closedPort;
        Debug.Log("CLIENT: \"closeSocket() closed client socket on port " + closedPort + "\"");

    }


    // helper: clears the numpad buffer and blocks reading from it
    // returns the original contents of the buffer
    private string clearNumpadBuffer() {
        string contents = this.numpadBuffer;
        this.numpadBuffer = "";
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
            }
        }
        // pressed a character key, add key to the buffer
        else {
            this.bufferReadyToRead = false;
            this.numpadBuffer += key;
        }
        ClientInstructions.screen.text = "CLIENT: numpad() " + key;
        Debug.Log("CLIENT: \"numpad() " + key + "\"");
    }

    public void resetModules()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // client functions
    public void connectToServer()
    {
        Debug.Log("CLIENT: \"connectToServer stub\"");
    }

    // TODO:
    // Might want to rename function to createPacket, split up into assigning data to packet
    // and creation and travel of the packet object itself.
    public void sendData()
    {
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
        p.GetComponent<UDPInfo>().port = activePort;
        p.GetComponent<UDPInfo>().destination = "192.168.0.2";
        //p.name = "tcp_model";
        //p = GameObject.Find("tcp_model");

        // TODO: Destroy based on colliding with the destination, not on time.
        Destroy(p, 12);

        Vector3 scaleChange = new Vector3(75f, 75f, 75f);

        p.transform.localScale = scaleChange;
        p.transform.Rotate(0f, 0f, 90f, Space.Self);

        ClientInstructions.screen.text = "Sending data...";
        Debug.Log("CLIENT: \"sendData stub\"");
    }

    public void sendPacket() {
        // TODO: use System.Linq to get the actual tail of the list so the payload is not
        // added repeatedly.
        if (p != null) {
            char payload = p.GetComponent<UDPInfo>().payload;
            int tail = server.dataReceived.Count;

            if (this.activePort == 0) p.GetComponent<Rigidbody>().useGravity = true;
            p.transform.position = Vector3.MoveTowards(p.transform.position, target, Time.deltaTime * speed);
            if (p.GetComponent<PacketCollider>().received) {
                // TODO: lots of conditionals to check each frame,
                // definitely a better way to insert once while collisions occur,
                // likely from the PacketCollider script.
                if (tail.Equals(0)) {
                    Debug.Log("Adding payload to server");
                    server.dataReceived.Add(payload);
                }
                else {
                    if (server.dataReceived[tail-1] != payload) {
                        Debug.Log("Adding payload to server");
                        server.dataReceived.Add(payload);
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

    // server functions
    public void listenForConnection()
    {
        Debug.Log("CLIENT: \"ERROR: The client does not need to listen for a connection.\"");
    }

    public void acceptConnection()
    {
        Debug.Log("CLIENT: \"ERROR: The client does not need to accept a connection.\"");
    }

    public void receiveData()
    {
        Debug.Log("CLIENT: \"ERROR: The client does not need to receive data.\"");
    }

    public void toggleAutoReceive()
    {
        Debug.Log("CLIENT: \"ERROR: The client does not need to receive data.\"");
    }

    
}
