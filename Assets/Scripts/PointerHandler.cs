using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR.Extras;

public class PointerHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;

    public GameObject clientArea;
    public GameObject serverArea;
    public string quizScene;

    public GameObject desktopPlayer;
    public GameObject vrPlayer;
    public Vector3 labPositionDesktop;
    public Vector3 labPositionVR;
    public Vector3 startPosDesktop;
    public Vector3 startPosVR;

    public Vector3 defaultRotation;

    public GameObject taskList;


    // List<GameObject> moreInfoButtons;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    private void processInput(String buttonName) {
        switch (buttonName) {
            //client cases
            case "c_Create()":
                StartCoroutine(this.clientArea.GetComponent<ClientController>().createSocket());
                break;
            case "c_Bind()":
                StartCoroutine(this.clientArea.GetComponent<ClientController>().bindSocket());
                break;
            case "c_Close()":
                StartCoroutine(this.clientArea.GetComponent<ClientController>().closeSocket());
                break;
            case "c_Connect()":
                this.clientArea.GetComponent<ClientController>().connectToServer();
                break;
            case "c_Send()":
                StartCoroutine(this.clientArea.GetComponent<ClientController>().sendData());
                break;
            case "c_Auto_Send()":
                this.clientArea.GetComponent<ClientController>().toggleAutoSend();
                break;
            case "c_Listen()":
                this.clientArea.GetComponent<ClientController>().listenForConnection();
                break;
            case "c_Accept()":
                this.clientArea.GetComponent<ClientController>().acceptConnection();
                break;
            case "c_Receive()":
                this.clientArea.GetComponent<ClientController>().receiveData();
                break;
            case "c_Auto_Receive()":
                this.clientArea.GetComponent<ClientController>().toggleAutoReceive();
                break;
            case "c_Reset()":
                this.clientArea.GetComponent<ClientController>().resetModules();
                break;
            case "c_0":
                this.clientArea.GetComponent<ClientController>().numpad('0');
                break;
            case "c_1":
                this.clientArea.GetComponent<ClientController>().numpad('1');
                break;
            case "c_2":
                this.clientArea.GetComponent<ClientController>().numpad('2');
                break;
            case "c_3":
                this.clientArea.GetComponent<ClientController>().numpad('3');
                break;
            case "c_4":
                this.clientArea.GetComponent<ClientController>().numpad('4');
                break;
            case "c_5":
                this.clientArea.GetComponent<ClientController>().numpad('5');
                break;
            case "c_6":
                this.clientArea.GetComponent<ClientController>().numpad('6');
                break;
            case "c_7":
                this.clientArea.GetComponent<ClientController>().numpad('7');
                break;
            case "c_8":
                this.clientArea.GetComponent<ClientController>().numpad('8');
                break;
            case "c_9":
                this.clientArea.GetComponent<ClientController>().numpad('9');
                break;
            case "c_period":
                this.clientArea.GetComponent<ClientController>().numpad('.');
                break;
            case "c_backspace":
                this.clientArea.GetComponent<ClientController>().numpad('b');
                break;
            case "c_enter":
                this.clientArea.GetComponent<ClientController>().numpad('e');
                break;

            // server cases
            case "s_Create()":
                StartCoroutine(this.serverArea.GetComponent<ServerController>().createSocket());
                break;
            case "s_Bind()":
                    StartCoroutine(this.serverArea.GetComponent<ServerController>().bindSocket());
                break;
            case "s_Close()":
                    StartCoroutine(this.serverArea.GetComponent<ServerController>().closeSocket());
                break;
            case "s_Connect()":
                this.serverArea.GetComponent<ServerController>().connectToServer();
                break;
            case "s_Send()":
                this.serverArea.GetComponent<ServerController>().sendData();
                break;
            case "s_Auto_Send()":
                this.serverArea.GetComponent<ServerController>().toggleAutoSend();
                break;
            case "s_Listen()":
                this.serverArea.GetComponent<ServerController>().listenForConnection();
                break;
            case "s_Accept()":
                this.serverArea.GetComponent<ServerController>().acceptConnection();
                break;
            case "s_Receive()":
                this.serverArea.GetComponent<ServerController>().receiveData();
                break;
            case "s_Auto_Receive()":
                this.serverArea.GetComponent<ServerController>().toggleAutoReceive();
                break;
            case "s_Reset()":
                this.serverArea.GetComponent<ServerController>().resetModules();
                break;
            case "s_0":
                this.serverArea.GetComponent<ServerController>().numpad('0');
                break;
            case "s_1":
                this.serverArea.GetComponent<ServerController>().numpad('1');
                break;
            case "s_2":
                this.serverArea.GetComponent<ServerController>().numpad('2');
                break;
            case "s_3":
                this.serverArea.GetComponent<ServerController>().numpad('3');
                break;
            case "s_4":
                this.serverArea.GetComponent<ServerController>().numpad('4');
                break;
            case "s_5":
                this.serverArea.GetComponent<ServerController>().numpad('5');
                break;
            case "s_6":
                this.serverArea.GetComponent<ServerController>().numpad('6');
                break;
            case "s_7":
                this.serverArea.GetComponent<ServerController>().numpad('7');
                break;
            case "s_8":
                this.serverArea.GetComponent<ServerController>().numpad('8');
                break;
            case "s_9":
                this.serverArea.GetComponent<ServerController>().numpad('9');
                break;
            case "s_period":
                this.serverArea.GetComponent<ServerController>().numpad('.');
                break;
            case "s_backspace":
                this.serverArea.GetComponent<ServerController>().numpad('b');
                break;
            case "s_enter":
                this.serverArea.GetComponent<ServerController>().numpad('e');
                break;
            // task board cases
            case "More Info Button (cc)":
                GameObject.Find("Info pages").transform.Find("Client Create").GetComponent<MoreInfoVisibility>().toggleInfo();
                break;
            case "More Info Button (cb)":
                GameObject.Find("Info pages").transform.Find("Client Bind").GetComponent<MoreInfoVisibility>().toggleInfo();
                break;
            case "More Info Button (sc)":
                GameObject.Find("Info pages").transform.Find("Server Create").GetComponent<MoreInfoVisibility>().toggleInfo();
                break;
            case "More Info Button (sb)":
                GameObject.Find("Info pages").transform.Find("Server Bind").GetComponent<MoreInfoVisibility>().toggleInfo();
                break;
            case "More Info Button (sr)":
                GameObject.Find("Info pages").transform.Find("Server Receive").GetComponent<MoreInfoVisibility>().toggleInfo();
                break;
            case "More Info Button (cs)":
                GameObject.Find("Info pages").transform.Find("Client Send").GetComponent<MoreInfoVisibility>().toggleInfo();
                break;
            case "More Info Button (scl)":
                GameObject.Find("Info pages").transform.Find("Server Close").GetComponent<MoreInfoVisibility>().toggleInfo();
                break;
            case "More Info Button (ccl)":
                GameObject.Find("Info pages").transform.Find("Client Close").GetComponent<MoreInfoVisibility>().toggleInfo();
                break;
            case "Exit button":
                GameObject info_pages = GameObject.Find("Info pages");
                foreach(Transform page in info_pages.transform) {
                    if(page.gameObject.activeSelf == true) {
                        page.GetComponent<MoreInfoVisibility>().toggleInfo();
                    }
                }
                break;
            case "Task List Button":
                taskList.SetActive(!taskList.activeSelf);
                break;
            case "Quiz Button":
                // check that Checkmark (cc), Checkmark (cs), Checkmark (ccl) are all active
                ClientController ccontrol = Transform.FindObjectOfType<ClientController>();
                bool clientComplete = true;
                foreach(GameObject obj in ccontrol.checkmarks) {
                    if(obj.name != "Checkmark (cb)" && obj.activeSelf == false) {
                        clientComplete = false;
                    }
                }

                ServerController scontrol = Transform.FindObjectOfType<ServerController>();
                bool serverComplete = true;
                foreach(GameObject obj in scontrol.checkmarks) {
                    if(obj.activeSelf == false) {
                        clientComplete = false;
                    }
                }
                // check that Checkmark (sc), Checkmark (sb), Checkmark (sr), and Checkmark (scl) are all active
                if(clientComplete && serverComplete) {
                    Debug.Log("start quiz here");
                    SceneManager.LoadScene(quizScene, LoadSceneMode.Single);

                }
                else {
                    Debug.Log("still more steps to complete before starting quiz");
                }
                break;

            case "StartGame":
                desktopPlayer.transform.position = labPositionDesktop;
                vrPlayer.transform.position = labPositionVR;
                break;

            case "TitleButton":
                desktopPlayer.transform.position = startPosDesktop;
                vrPlayer.transform.position = startPosVR;
                break;

            case "QuitButton":
                Application.Quit();
                break;

            // Quiz Scene buttons
            case "A":
                GameObject.Find("QuizHandler").GetComponent<Quiz>().A_choices();
                break;
            case "B":
                GameObject.Find("QuizHandler").GetComponent<Quiz>().B_choices();
                break;
            case "C":
                GameObject.Find("QuizHandler").GetComponent<Quiz>().C_choices();
                break;
            case "D":
                GameObject.Find("QuizHandler").GetComponent<Quiz>().D_choices();
                break;
            case "GoToLab":
                GameObject.Find("QuizHandler").GetComponent<Quiz>().goToLab();
                break;

            default:
                break;
        }
    }

    

    public void PointerClick(object sender, PointerEventArgs e) {
        if (e.target.tag == "Button") {
            Debug.Log("Button was clicked");
            processInput(e.target.name);
        }
        else if (e.target.tag == "Switch") {
            Debug.Log("Button was switched");
            processInput(e.target.name);
            //e.target.GetComponent<SwitchState>().state = !e.target.GetComponent<SwitchState>().state;
        }
    }

    public void PointerInside(object sender, PointerEventArgs e) {
        if (e.target.tag == "Button") {
            //Debug.Log("Button was entered");
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e) {
        if (e.target.tag == "Button") {
            //Debug.Log("Button was exited");
        }
    }

    void Update() {
        if (GameObject.FindWithTag("Player")) {
            if (GameObject.FindWithTag("Player").activeSelf) {
                if (Input.GetMouseButtonDown(0)) {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast (ray, out hit, 100.0f)) {
                        processInput(hit.transform.name);                     
                    }
                }
            }
        }
    }

}