using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizQuestion : MonoBehaviour
{
    private GameObject head, text;
    public static TextMeshProUGUI meshH, mesh;
    private GameObject A, B, C, D;
    public static Button ButtonA, ButtonB, ButtonC, ButtonD;

    // Start is called before the first frame update
    void Start()
    {
        A = GameObject.Find("A");
        ButtonA = A.GetComponent<Button>();

        B = GameObject.Find("B");
        ButtonB = B.GetComponent<Button>();

        C = GameObject.Find("C");
        ButtonC = C.GetComponent<Button>();

        D = GameObject.Find("D");
        ButtonD = D.GetComponent<Button>();

        head = this.transform.GetChild(0).gameObject;
        text = this.transform.GetChild(1).gameObject;

        meshH = head.GetComponent<TextMeshProUGUI>();
        mesh = text.GetComponent<TextMeshProUGUI>();
        mesh.text = "Question 1) What is the first step in sending data through a UDP connection for the CLIENT?";

        ButtonA.GetComponentInChildren<Text>().text = "Close the socket";
        ButtonB.GetComponentInChildren<Text>().text = "Receive the data";
        ButtonC.GetComponentInChildren<Text>().text = "Bind the socket";
        ButtonD.GetComponentInChildren<Text>().text = "Create a socket";

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
