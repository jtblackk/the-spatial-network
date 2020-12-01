using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ServerController : MonoBehaviour
{   
    
    public enum socketType {None, TCP, UDP};
    private socketType activeSocketType;

    public int activePort;
    private Vector3 originalSocketPos;
    private float spaceBetweenPorts = .505f;
    public GameObject activeSocketObject;

    public enum state {Closed, Created, Bound, Transmitting};
    public state activeSocketState;

    public string numpadBuffer;
    public bool bufferReadyToRead;
    public TextMeshPro keypadData;

    public List<char> dataReceived = new List<char>();

    public GameObject serverArea;

    public bool autoReceiveToggled;
    public bool screenBlanked;

    public List<GameObject> checkmarks;

    public void Start() {
        this.originalSocketPos = this.activeSocketObject.transform.localPosition;
        
        checkmarks = new List<GameObject>();
        GameObject[] objs = FindObjectsOfType<GameObject>();
        foreach(GameObject obj in objs) {
            if(obj.name.Contains("Checkmark (s")) {
                // Debug.Log("adding " + obj.name + "to the list");
                checkmarks.Add(obj);
                obj.SetActive(false);
            }
        }
        checkmarks.Reverse();
    }

    // shared functions


   public IEnumerator createSocket() {
       // if there's already an active socket, send an error message
       if(this.activeSocketState != state.Closed) {
            ServerInstructions.screen.text = "A socket is already in use. No need to create another one on this module.";
            // Debug.Log("SERVER: \"ERROR: A socket is already in use. No need to create another one on this module.\"");
           yield break;
       }
       
        
        // ask user which type of socket to create (TCP or UDP)
        // string numpadInput;
        // do {
        //     ServerInstructions.screen.text = "SERVER: Choose a socket type: (1) UDP (2) TCP";
        //     Debug.Log("SERVER: \"Choose a socket type: (1) UDP (2) TCP\"");

        //     // clear the numpad buffer
        //     this.clearNumpadBuffer();

        //     // wait for the numpad buffer to be ready to read
        //     while(this.bufferReadyToRead != true) {
        //         yield return null;
        //     }

        //     // record and clear the buffer inputs
        //     numpadInput = this.clearNumpadBuffer();

        // } while(numpadInput != "1" && numpadInput != "2");


        //  // make the socket visible, update appropriate status variables
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
        ServerInstructions.screen.text = "created a new " + this.activeSocketType + " socket";
        foreach(GameObject obj in checkmarks) {
            if(obj.name == "Checkmark (sc)") {
               obj.SetActive(true);
            }
        }
   }

    public IEnumerator bindSocket() {
        // check that a socket has been created but hasn't been bound
        if(this.activeSocketState != state.Created) {
            if(this.activeSocketState == state.Bound) {
                ServerInstructions.screen.text = "ERROR: already bound a port on the module";
                yield break;
            }
            ServerInstructions.screen.text = "ERROR: must create a socket before binding it";
            yield break;
        }

        // ask user to select a port to bind to
        string numpadInput;
        do {
            ServerInstructions.screen.text = "enter a port to bind to\n(1-4 on keypad)\n\npress enter when finished";
            // Debug.Log("SERVER: \"Enter a port to bind to (1-4)\"");

            
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
        // a. move socket object to the correct port hole
        this.activeSocketObject.transform.Translate(Vector3.back * (this.activePort - 1) * this.spaceBetweenPorts, Space.World);
        
        // b. hide the port cover of the current port
        this.transform.Find("Server Ports").transform.Find("Port Cover " + this.activePort).gameObject.SetActive(false);

        // c.  move the socket object along the x axis
        while(this.originalSocketPos.x - this.activeSocketObject.transform.localPosition.x > -.309f) {
            this.activeSocketObject.transform.Translate(Vector3.right * .001f);
            yield return null;
        }

        // present bind feedback 
        ServerInstructions.screen.text = "bound the socket to port " + this.activePort;
        foreach(GameObject obj in checkmarks) {
            if(obj.name == "Checkmark (sb)") {
               obj.SetActive(true);
            }
        }
    }

    public IEnumerator closeSocket()
    {
        // check that there's a socket to close
        if(this.activeSocketState == state.Closed) {
            ServerInstructions.screen.text = "ERROR: called close without any sockets opened";
            yield break;
        }
        else if (this.activeSocketState == state.Created) {
             // update appropriate status variables
            this.activeSocketState = state.Closed;
            this.activeSocketType = socketType.None;
            int closedPort = this.activePort;
            this.activePort = 0;
            
            // present feedback
            ServerInstructions.screen.text = "closed socket";

        }
        else if (this.activeSocketState == state.Bound) {
            
            // update appropriate status variables
            this.activeSocketState = state.Closed;
            this.activeSocketType = socketType.None;
            int closedPort = this.activePort;
            this.activePort = 0;
            
            // present feedback
            ServerInstructions.screen.text = "closed socket on port " + closedPort;

            // do close transformations
            // a. retreat socket back into port box
            while(this.originalSocketPos.x - this.activeSocketObject.transform.localPosition.x < 0) {
                this.activeSocketObject.transform.Translate(Vector3.left * .001f);
                yield return null;
            }

            // b. show port cover
            this.transform.Find("Server Ports").transform.Find("Port Cover " + closedPort).gameObject.SetActive(true);
            
            // c. move socket back to port 1
            this.activeSocketObject.transform.Translate(Vector3.forward * (closedPort - 1) * this.spaceBetweenPorts, Space.World);
            
            // d. hide socket
            this.activeSocketObject.transform.Find("UDP Socket").gameObject.SetActive(false);
            this.activeSocketObject.transform.Find("TCP Socket").gameObject.SetActive(false);


        }

        foreach(GameObject obj in checkmarks) {
            if(obj.name == "Checkmark (scl)") {
               obj.SetActive(true);
            }
        }
    }

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
    public void connectToServer()
    {
        ServerInstructions.screen.text = "ERROR: connecting is only done on a TCP client";
    }

    public void sendData()
    {
        ServerInstructions.screen.text = "ERROR: only the client sends data";
    }

    public void toggleAutoSend()
    {
        ServerInstructions.screen.text = "ERROR: only the client sends data. No need for auto-send";
    }

    // server functions
    public void listenForConnection()
    {
        // Debug.Log("SERVER: \"listenForConnection stub\"");
        ServerInstructions.screen.text = "ERROR: listening is only done when using TCP";
    }

    public void acceptConnection()
    {
        ServerInstructions.screen.text = "ERROR: accepting is only done when using TCP";
        // Debug.Log("SERVER: \"acceptConnection stub\"");
    }

    public void receiveData()
    {
        if(this.activeSocketState != state.Bound) {
            ServerInstructions.screen.text = "ERROR: you need to bind a socket before you can receive data";
            return;
        }
        
        int tail = dataReceived.Count;

        if (ServerInstructions.screen.text != "" && !screenBlanked) {
            ServerInstructions.screen.text = "";
            screenBlanked = true;
        }
        if (tail.Equals(0)) {
            ServerInstructions.screen.text = "No data to process";
            return;
        }
        else {
            ServerInstructions.screen.text += dataReceived[tail-1];
            dataReceived.Remove(dataReceived[tail-1]);
        }
        
        // Debug.Log("SERVER: \"receiveData stub\"");
        foreach(GameObject obj in checkmarks) {
            if(obj.name == "Checkmark (sr)") {
               obj.SetActive(true);
            }
        }
    }

    public void toggleAutoReceive()
    {
        // Debug.Log("SERVER: \"toggleAutoReceive stub\"");
        autoReceiveToggled = !autoReceiveToggled;
        StartCoroutine(autoReceive());
    }

    public IEnumerator autoReceive() {
        while (autoReceiveToggled) {
            int tail = dataReceived.Count;

            if (ServerInstructions.screen.text != "" && !screenBlanked) {
                ServerInstructions.screen.text = "";
                screenBlanked = true;
                yield return null;
            }
            if (!tail.Equals(0)) {
                ServerInstructions.screen.text += dataReceived[tail-1];
                //dataReceived.Remove(dataReceived[tail-1]);
                yield return null;
            }
            yield return new WaitForSeconds(1.0f);
        }
        screenBlanked = false;
        yield return null;
    }
}
