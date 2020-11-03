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
           Debug.Log("CLIENT: \"ERROR: A client socket is already in use. No need to create another socket.\"");
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

    public void bindSocket() {
        // check that a socket has been created but hasn't been bound
        if(this.activeSocketState != state.Created) {
            if(this.activeSocketState == state.Bound) {
                Debug.Log("CLIENT \"ERROR: already bound a port on the module\"");
                return;
            }
            Debug.Log("CLIENT \"ERROR: must create a socket before binding it\"");
            return;
        }

        // ask user to select a port to bind to

        // get input from the client keypad
            // if the user enters an invalid option, present error message and try again

        // bind the socket, update appropriate values
        this.activeSocketState = state.Bound;
        this.activePort = 3; // TODO / NOTE: just using 3 until keypad system is worked out

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
        Debug.Log("CLIENT: \"sendData stub\"");
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
