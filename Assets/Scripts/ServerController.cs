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



    // shared functions
   public void createSocket() {
       Debug.Log("SERVER: createSocket stub");
       // if there's already an active socket, send an error message
       if(activeSocketObject != null) {
           Debug.Log("SERVER ERROR: A server socket is already in use. No need to create another socket.");
           return;
       }
   }

    public void bindSocket() {
       Debug.Log("SERVER: bindSocket stub");
    }

    public void closeSocket()
    {
        Debug.Log("SERVER: closeSocket stub");
    }

    public void numpad(char key) {
        Debug.Log("SERVER: numpad stub -> " + key);
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
