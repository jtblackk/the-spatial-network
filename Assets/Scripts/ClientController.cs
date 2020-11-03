using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClientController : MonoBehaviour
{   
    public enum socketType {None, TCP, UDP};
    public socketType activeSocketType;

    public int activePort;
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
    
    // shared functions

    // helper: clears the numpad buffer and blocks reading from it
    // returns the original contents of the buffer
    private string clearNumpadBuffer() {
        string contents = this.numpadBuffer;
        this.numpadBuffer = "";
        this.bufferReadyToRead = false;

        return contents;
    }

   public IEnumerator createSocket() {
       // if there's already an active socket, send an error message
       if(this.activeSocketObject != null || this.activeSocketState != state.Closed) {
           Debug.Log("CLIENT: \"ERROR: A socket is already in use. No need to create another one on this module.\"");
           yield break;
       }
       
        
        // ask user which type of socket to create (TCP or UDP)
        string numpadInput;
        do {
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


        // create the socket, update appropriate variables
        if (numpadInput == "1") {
            this.activeSocketType = socketType.UDP;
        }        
        else {
            this.activeSocketType = socketType.TCP;
        }
        this.activeSocketState = state.Created;
        
        // present creation feedback
        Debug.Log("CLIENT: \"createSocket() created a new " + this.activeSocketType + " socket\"");

   }

    public IEnumerator bindSocket() {
        // check that a socket has been created but hasn't been bound
        if(this.activeSocketState != state.Created) {
            if(this.activeSocketState == state.Bound) {
                Debug.Log("CLIENT \"ERROR: already bound a port on the module\"");
                yield break;
            }
            Debug.Log("CLIENT \"ERROR: must create a socket before binding it\"");
            yield break;
        }

        // ask user to select a port to bind to
        string numpadInput;
        do {
            Debug.Log("CLIENT: \"Enter a port to bind to (1-4)\"");

            // clear the numpad buffer
            this.clearNumpadBuffer();

            // wait for the numpad buffer to be ready to read
            while(this.bufferReadyToRead != true) {
                yield return null;
            }

            // record and clear the buffer contents
            numpadInput = this.clearNumpadBuffer();

        } while(Int32.Parse(numpadInput) < 1 || Int32.Parse(numpadInput) > 4);

        // bind the socket, update appropriate values
        this.activePort = Int32.Parse(numpadInput);
        this.activeSocketState = state.Bound;

        // present bind feedback
        // make socket tube protrude
        Debug.Log("CLIENT: \"bindSocket() bound the client socket to port " + this.activePort + "\"");
    }

    public void closeSocket()
    {   
        // check that there's a socket to close
        if(this.activeSocketState == state.Closed) {
            Debug.Log("CLIENT \"ERROR: called close without any sockets opened\"");
            return;
        }

        // update appropriate status variables
        this.activeSocketState = state.Closed;
        this.activeSocketType = socketType.None;
        this.activeSocketObject = null;
        int closedPort = this.activePort;
        this.activePort = 0;

        // present close() feedback
        // return socket tube to original position
        Debug.Log("CLIENT: \"closeSocket() closed client socket on port " + closedPort + "\"");

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
        Debug.Log("CLIENT: \"numpad() " + key + "\"");
    }

    public void resetModules()
    {
        
        Debug.Log("CLIENT: \"resetModules() stub\"");
    }

    // client functions
    public void connectToServer()
    {
        Debug.Log("CLIENT: \"connectToServer stub\"");
    }

    public void sendData()
    {
        // Decides which socket, or path
        // Needs more looking into how we decide ports
        input = 1;
        switch (input)
        {
            case 1:
                start = GameObject.Find("Client Area/Client Ports/Socket 1").transform.position;
                target = GameObject.Find("Server Area/Server Ports/Socket 1").transform.position;
                break;

        }

        p = Instantiate(this.packet, start, Quaternion.identity);
        p.name = "tcp_model";
        p = GameObject.Find("tcp_model");

        Destroy(p, 12);

        Vector3 scaleChange = new Vector3(75f, 75f, 75f);

        p.transform.localScale = scaleChange;
        p.transform.Rotate(0f, 0f, 90f, Space.Self);
   
        Debug.Log("CLIENT: \"sendData stub\"");
    }
    
    private void Update()
    {
        if (p != null)
        {
            p.transform.position = Vector3.MoveTowards(p.transform.position, target, Time.deltaTime * speed);
        }
    }

    public void toggleAutoSend()
    {
        Debug.Log("CLIENT: \"toggleAutoSend stub\"");
    }



    // server functions
    public void listenForConnection()
    {
        Debug.Log("CLIENT \"ERROR: The client does not need to listen for a connection.\"");
    }

    public void acceptConnection()
    {
        Debug.Log("CLIENT \"ERROR: The client does not need to accept a connection.\"");
    }

    public void receiveData()
    {
        Debug.Log("CLIENT \"ERROR: The client does not need to receive data.\"");
    }

    public void toggleAutoReceive()
    {
        Debug.Log("CLIENT \"ERROR: The client does not need to receive data.\"");
    }

    
}
