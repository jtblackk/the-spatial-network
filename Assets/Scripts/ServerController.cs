using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ServerController : MonoBehaviour
{   
    
    public enum socketType {None, TCP, UDP};
    public socketType activeSocketType;

    public int activePort;
    private Vector3 originalSocketPos;
    private float spaceBetweenPorts = .505f;
    public GameObject activeSocketObject;

    public enum state {Closed, Created, Bound, Transmitting};
    public state activeSocketState;

    public string numpadBuffer;
    public bool bufferReadyToRead;

    public List<char> dataReceived = new List<char>();

    public GameObject serverArea;

    public bool autoReceiveToggled;
    public bool screenBlanked;

    public void Start() {
        this.originalSocketPos = this.activeSocketObject.transform.localPosition;
    }

    // shared functions

    private string clearNumpadBuffer() {
        string contents = this.numpadBuffer;
        this.numpadBuffer = "";
        this.bufferReadyToRead = false;

        return contents;
    }

   public IEnumerator createSocket() {
       // if there's already an active socket, send an error message
       if(this.activeSocketState != state.Closed) {
            ServerInstructions.screen.text = "SERVER ERROR: A socket is already in use. No need to create another one on this module.";
            Debug.Log("SERVER: \"ERROR: A socket is already in use. No need to create another one on this module.\"");
           yield break;
       }
       
        
        // ask user which type of socket to create (TCP or UDP)
        string numpadInput;
        do {
            ServerInstructions.screen.text = "SERVER: Choose a socket type: (1) UDP (2) TCP";
            Debug.Log("SERVER: \"Choose a socket type: (1) UDP (2) TCP\"");

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
        ServerInstructions.screen.text = "SERVER: createSocket() created a new " + this.activeSocketType + " socket";
        Debug.Log("SERVER: \"createSocket() created a new " + this.activeSocketType + " socket\"");
   }

    public IEnumerator bindSocket() {
        // check that a socket has been created but hasn't been bound
        if(this.activeSocketState != state.Created) {
            if(this.activeSocketState == state.Bound) {
                ServerInstructions.screen.text = "SERVER: ERROR: already bound a port on the module";
                Debug.Log("SERVER: \"ERROR: already bound a port on the module\"");
                yield break;
            }
            ServerInstructions.screen.text = "SERVER: ERROR: must create a socket before binding it";
            Debug.Log("SERVER: \"ERROR: must create a socket before binding it\"");
            yield break;
        }

        // ask user to select a port to bind to
        string numpadInput;
        do {
            ServerInstructions.screen.text = "SERVER: Enter a port to bind to (1-4)";
            Debug.Log("SERVER: \"Enter a port to bind to (1-4)\"");

            
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
        ServerInstructions.screen.text = "SERVER: bindSocket() bound the server socket to port " + this.activePort;
        Debug.Log("SERVER: \"bindSocket() bound the server socket to port " + this.activePort + "\"");
    }

    public IEnumerator closeSocket()
    {
        // check that there's a socket to close
        if(this.activeSocketState == state.Closed) {
            ServerInstructions.screen.text = "SERVER: ERROR: called close without any sockets opened";
            Debug.Log("SERVER: \"ERROR: called close without any sockets opened\"");
            yield break;
        }

        // update appropriate status variables
        this.activeSocketState = state.Closed;
        this.activeSocketType = socketType.None;
        int closedPort = this.activePort;
        this.activePort = 0;

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

        // return socket tube to original position
        ServerInstructions.screen.text = "SERVER: closeSocket() closed server socket on port";
        Debug.Log("SERVER: \"closeSocket() closed server socket on port " + closedPort + "\"");
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
        ServerInstructions.screen.text = "SERVER: numpad() " + key;
        Debug.Log("SERVER: \"numpad() " + key + "\"");
    }

    public void resetModules()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }


    // client functions
    public void connectToServer()
    {
        Debug.Log("SERVER: \"ERROR: The server does not need to connect to a sever\"");
    }

    public void sendData()
    {
        Debug.Log("SERVER: \"ERROR: The server does not need to send data\"");
    }

    public void toggleAutoSend()
    {
        Debug.Log("SERVER: \"ERROR: The server does not need to send data\"");
    }

    // server functions
    public void listenForConnection()
    {
        Debug.Log("SERVER: \"listenForConnection stub\"");
    }

    public void acceptConnection()
    {
        Debug.Log("SERVER: \"acceptConnection stub\"");
    }

    public void receiveData()
    {
        int tail = dataReceived.Count;

        if (ServerInstructions.screen.text != "" && !screenBlanked) {
            ServerInstructions.screen.text = "";
            screenBlanked = true;
        }
        if (tail.Equals(0)) {
            ServerInstructions.screen.text = "No data to receive";
        }
        else {
            ServerInstructions.screen.text += dataReceived[tail-1];
            //dataReceived.Remove(dataReceived[tail-1]);
        }
        
        Debug.Log("SERVER: \"receiveData stub\"");
    }

    public void toggleAutoReceive()
    {
        Debug.Log("SERVER: \"toggleAutoReceive stub\"");
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
