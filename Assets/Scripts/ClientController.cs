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
  


    // shared functions
   public void createSocket() {
       // if there's already an active socket, send an error message
       if(this.activeSocketObject != null || this.activeSocketState != state.Closed) {
           Debug.Log("CLIENT ERROR: A client socket is already in use. No need to create another socket.");
           return;
       }
       
        
        // ask user which type of socket to create (TCP or UDP)
        // get input from the client keypad
            // if user enters invalid option, present error message and try again 
        

        // create the socket, update appropriate variables        
        this.activeSocketType = socketType.UDP; // TODO / NOTE: just going to default UDP until keypad system is worked out
        this.activeSocketState = state.Created;
        
        // present creation feedback
        Debug.Log("CLIENT: createSocket() created a new " + this.activeSocketType + " socket");

   }

    public void bindSocket() {
        // check that a socket has been created but hasn't been bound

        // ask user to select a port to bind to

        // get input from the client keypad
            // if the user enters an invalid option, present error message and try again

        // bind the socket, update appropriate values
        this.activeSocketState = state.Bound;
        this.activePort = 3; // TODO / NOTE: just using 3 until keypad system is worked out

        // present bind feedback
        // make socket tube protrude
        Debug.Log("CLIENT: bindSocket() bound the client socket to port " + this.activePort);
    }

    public void closeSocket()
    {   
        // check that there's a socket to close
        

        // update appropriate status variables
        this.activeSocketState = state.Closed;
        this.activeSocketType = socketType.None;
        this.activeSocketObject = null;
        int closedPort = this.activePort;
        this.activePort = 0;

        // present close feedback
        // return socket tube to original position
        Debug.Log("CLIENT: closeSocket() closed client socket on port " + closedPort);

    }

    public void numpad(char key) {
        Debug.Log("CLIENT: numpad() " + key);
    }

    public void resetModules()
    {
        Debug.Log("CLIENT: resetModules() stub");
    }

    // client functions
    public void connectToServer()
    {
        Debug.Log("CLIENT: connectToServer stub");
    }

    public void sendData()
    {
        Debug.Log("CLIENT: sendData stub");
    }

    public void toggleAutoSend()
    {
        Debug.Log("CLIENT: toggleAutoSend stub");
    }



    // server functions
    public void listenForConnection()
    {
        Debug.Log("CLIENT ERROR: The client does not need to listen for a connection.");
    }

    public void acceptConnection()
    {
        Debug.Log("CLIENT ERROR: The client does not need to accept a connection.");
    }

    public void receiveData()
    {
        Debug.Log("CLIENT ERROR: The client does not need to receive data.");
    }

    public void toggleAutoReceive()
    {
        Debug.Log("CLIENT ERROR: The client does not need to receive data.");
    }

    
}
