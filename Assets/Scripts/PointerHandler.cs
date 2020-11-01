using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class PointerHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    

    public void PointerClick(object sender, PointerEventArgs e) {
        if (e.target.tag == "Button") {
            Debug.Log("Button was clicked");
        }
        else if (e.target.tag == "Switch") {
            Debug.Log("Button was switched");
            e.target.GetComponent<SwitchState>().state = !e.target.GetComponent<SwitchState>().state;
        }
    }

    public void PointerInside(object sender, PointerEventArgs e) {
        if (e.target.tag == "Button") {
            Debug.Log("Button was entered");
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e) {
        if (e.target.tag == "Button") {
            Debug.Log("Button was exited");
        }
    }

    void Update() {
        if (GameObject.FindWithTag("Player").activeSelf) {
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast (ray, out hit, 100.0f)) {
                    switch (hit.transform.name)
                    {
                        case "c_Create()":
                            this.client_create();
                            break;
                        case "c_Bind()":
                            this.client_bind();
                            break;
                        case "c_Close()":
                            this.client_close();
                            break;
                        case "c_Connect()":
                            this.client_connect();
                            break;
                        case "c_Send()":
                            this.client_send();
                            break;
                        case "c_Auto_Send()":
                            this.client_auto_send();
                            break;
                        case "c_Listen()":
                            this.client_listen();
                            break;
                        case "c_Accept()":
                            this.client_accept();
                            break;
                        case "c_Receive()":
                            this.client_receive();
                            break;
                        case "c_Auto_Receive()":
                            this.client_auto_receive();
                            break;
                        case "c_Reset()":
                            this.client_reset();
                            break;
                        case "c_0":
                            this.client_0();
                            break;
                        case "c_1":
                            this.client_1();
                            break;
                        case "c_2":
                            this.client_2();
                            break;
                        case "c_3":
                            this.client_3();
                            break;
                        case "c_4":
                            this.client_4();
                            break;
                        case "c_5":
                            this.client_5();
                            break;
                        case "c_6":
                            this.client_6();
                            break;
                        case "c_7":
                            this.client_7();
                            break;
                        case "c_8":
                            this.client_8();
                            break;
                        case "c_9":
                            this.client_9();
                            break;
                        case "c_period":
                            this.client_period();
                            break;
                        // server cases
                        case "s_Create()":
                            this.server_create();
                            break;
                        case "s_Bind()":
                            this.server_bind();
                            break;
                        case "s_Close()":
                            this.server_close();
                            break;
                        case "s_Connect()":
                            this.server_connect();
                            break;
                        case "s_Send()":
                            this.server_send();
                            break;
                        case "s_Auto_Send()":
                            this.server_auto_send();
                            break;
                        case "s_Listen()":
                            this.server_listen();
                            break;
                        case "s_Accept()":
                            this.server_accept();
                            break;
                        case "s_Receive()":
                            this.server_receive();
                            break;
                        case "s_Auto_Receive()":
                            this.server_auto_receive();
                            break;
                        case "s_Reset()":
                            this.server_reset();
                            break;
                        case "s_0":
                            this.server_0();
                            break;
                        case "s_1":
                            this.server_1();
                            break;
                        case "s_2":
                            this.server_2();
                            break;
                        case "s_3":
                            this.server_3();
                            break;
                        case "s_4":
                            this.server_4();
                            break;
                        case "s_5":
                            this.server_5();
                            break;
                        case "s_6":
                            this.server_6();
                            break;
                        case "s_7":
                            this.server_7();
                            break;
                        case "s_8":
                            this.server_8();
                            break;
                        case "s_9":
                            this.server_9();
                            break;
                        case "s_period":
                            this.server_period();
                            break;
                        default:
                            break;
                    }
                     
                }
            }
        }
    }

    private void server_create()
    {
        Debug.Log("stub: server_create()");
    }

    private void server_bind()
    {
        Debug.Log("stub: server_bind()");
    }

    private void server_close()
    {
        Debug.Log("stub: server_close()");
    }

    private void server_connect()
    {
        Debug.Log("stub: server_connect()");
    }

    private void server_send()
    {
        Debug.Log("stub: server_send()");
    }

    private void server_auto_send()
    {
        Debug.Log("stub: server_auto_send()");
    }

    private void server_listen()
    {
        Debug.Log("stub: server_listen()");
    }

    private void server_accept()
    {
        Debug.Log("stub: server_accept()");
    }

    private void server_receive()
    {
        Debug.Log("stub: server_receive()");
    }

    private void server_auto_receive()
    {
        Debug.Log("stub: server_auto_receive()");    
    }

    private void server_reset()
    {
        Debug.Log("stub: server_reset()");
    }

    private void server_0()
    {
        Debug.Log("stub: server_0()");
    }

    private void server_1()
    {
        Debug.Log("stub: server_1()");
    }

    private void server_2()
    {
        Debug.Log("stub: server_2()");
    }

    private void server_3()
    {
        Debug.Log("stub: server_3()");
    }

    private void server_4()
    {
        Debug.Log("stub: server_4()");
    }

    private void server_5()
    {
        Debug.Log("stub: server_5()");
    }

    private void server_6()
    {
        Debug.Log("stub: server_6()");
    }

    private void server_7()
    {
        Debug.Log("stub: server_7()");
    }

    private void server_8()
    {
        Debug.Log("stub: server_8()");
    }

    private void server_9()
    {
        Debug.Log("stub: server_9()");
    }

    private void server_period()
    {
        Debug.Log("stub: server_period()");
    }




    private void client_create() {
        Debug.Log("stub: client_create()");
    }
    private void client_bind() {
        Debug.Log("stub: client_bind()");
    }

    private void client_close() {
        Debug.Log("stub: client_create()");
    }
    private void client_connect() {
        Debug.Log("stub: client_connect");
    }

    private void client_send() {
        Debug.Log("stub: client_send");
    }

    private void client_auto_send() {
        Debug.Log("stub: client_auto_send");
    }

    private void client_listen() {
        Debug.Log("stub: client_listen");
    }

    private void client_accept() {
        Debug.Log("stub: client_accept");
    }

    private void client_receive() {
        Debug.Log("stub: client_receive");
    }

    private void client_auto_receive() {
        Debug.Log("stub: client_auto_receive");
    }

    private void client_reset() {
        Debug.Log("stub: client_reset");
    }

    private void client_0()
    {
        Debug.Log("stub: client_0()");
    }

    private void client_1()
    {
        Debug.Log("stub: client_1()");
    }

    private void client_2()
    {
        Debug.Log("stub: client_2()");
    }

    private void client_3()
    {
        Debug.Log("stub: client_3()");
    }

    private void client_4()
    {
        Debug.Log("stub: client_4()");
    }

    private void client_5()
    {
        Debug.Log("stub: client_5()");
    }

    private void client_6()
    {
        Debug.Log("stub: client_6()");
    }

    private void client_7()
    {
        Debug.Log("stub: client_7()");
    }

    private void client_8()
    {
        Debug.Log("stub: client_8()");
    }

    private void client_9()
    {
        Debug.Log("stub: client_9()");
    }

    private void client_period()
    {
        Debug.Log("stub: client_period()");
    }
}