using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientController : MonoBehaviour
{   

    public enum port {None, One, Two, Three, Four};
    public port activePort;
  
    public GameObject activeSocketObject;

    public enum socketType {None, TCP, UDP};
    public socketType activeSocketType;
  
    public enum state {Default, Closed, Bound, Transmitting};
    public state activeSocketState;
  



    // shared functions
   public void createSocket() {
       Debug.Log("CLIENT: createSocket stub");
       // if there's already an active socket, send an error message
       if(activeSocketObject != null) {
           Debug.Log("CLIENT ERROR: A client socket is already in use. No need to create another socket.");
           return;
       }
   }

    public void bindSocket() {
       Debug.Log("CLIENT: bindSocket stub");
    }

    public void closeSocket()
    {
        Debug.Log("CLIENT: closeSocket stub");
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
