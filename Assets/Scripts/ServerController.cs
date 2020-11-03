using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerController : MonoBehaviour
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

    private string clearNumpadBuffer() {
        string contents = this.numpadBuffer;
        this.numpadBuffer = "";
        this.bufferReadyToRead = false;

        return contents;
    }

   public IEnumerator createSocket() {
       // if there's already an active socket, send an error message
       if(this.activeSocketObject != null || this.activeSocketState != state.Closed) {
           Debug.Log("SERVER: \"ERROR: A socket is already in use. No need to create another one on this module.\"");
           yield break;
       }
       
        
        // ask user which type of socket to create (TCP or UDP)
        string numpadInput;
        do {
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


        // create the socket, update appropriate variables
        if (numpadInput == "1") {
            this.activeSocketType = socketType.UDP;
        }        
        else {
            this.activeSocketType = socketType.TCP;
        }
        this.activeSocketState = state.Created;
        
        // present creation feedback
        Debug.Log("SERVER: \"createSocket() created a new " + this.activeSocketType + " socket\"");
   }

    public void bindSocket() {
       Debug.Log("SERVER: bindSocket stub");
    }

    public void closeSocket()
    {
        Debug.Log("SERVER: closeSocket stub");
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
        Debug.Log("SERVER: \"numpad() " + key + "\"");
    }

    public void resetModules()
    {
        Debug.Log("SERVER: resetModules() stub");
    }


    // client functions
    public void connectToServer()
    {
        Debug.Log("SERVER ERROR: The server does not need to connect to a sever");
    }

    public void sendData()
    {
        Debug.Log("SERVER ERROR: The server does not need to send data");
    }

    public void toggleAutoSend()
    {
        Debug.Log("SERVER ERROR: The server does not need to send data");
    }



    // server functions
    public void listenForConnection()
    {
        Debug.Log("SERVER: listenForConnection stub");
    }

    public void acceptConnection()
    {
        Debug.Log("SERVER: acceptConnection stub");
    }

    public void receiveData()
    {
        Debug.Log("SERVER: receiveData stub");
    }

    public void toggleAutoReceive()
    {
        Debug.Log("SERVER: toggleAutoReceive stub");
    }

    
}
