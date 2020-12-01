using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    private GameObject t;
    private int currQ = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void goToLab()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void A_choices()
    {
        switch (currQ)
        {
            case 1:
                QuizQuestion.ButtonA.GetComponentInChildren<Text>().text = "INCORRECT";
                break;
            case 2:
                QuizQuestion.meshH.text = "CORRECT";
                currQ++;

                QuizQuestion.mesh.text = "Question 3) Between the SERVER and the CLIENT machine, which HAS to bind its socket for the connection?";
                QuizQuestion.ButtonA.GetComponentInChildren<Text>().text = "Client";
                QuizQuestion.ButtonB.GetComponentInChildren<Text>().text = "Server";
                QuizQuestion.ButtonC.GetComponentInChildren<Text>().text = "Both";
                QuizQuestion.ButtonD.GetComponentInChildren<Text>().text = "Neither";

                break;
            case 3:
                QuizQuestion.ButtonA.GetComponentInChildren<Text>().text = "INCORRECT";

                break;
        }
    }

    public void B_choices()
    {
        switch (currQ)
        {
            case 1:
                QuizQuestion.ButtonB.GetComponentInChildren<Text>().text = "INCORRECT";
                break;
            case 2:
                QuizQuestion.ButtonB.GetComponentInChildren<Text>().text = "INCORRECT";

                break;
            case 3:
                QuizQuestion.meshH.text = "CORRECT";
                QuizQuestion.mesh.text = "Congratulations! You have completed this activity!";


                break;
        }
    }

        public void C_choices()
    {
        switch (currQ)
        {
            case 1:
                QuizQuestion.ButtonC.GetComponentInChildren<Text>().text = "INCORRECT";
                break;
            case 2:
                QuizQuestion.ButtonC.GetComponentInChildren<Text>().text = "INCORRECT";

                break;
            case 3:
                QuizQuestion.ButtonC.GetComponentInChildren<Text>().text = "INCORRECT";

                break;
        }
    }

    public void D_choices()
    {
        switch (currQ)
        {
            case 1:
                QuizQuestion.meshH.text = "CORRECT";
                currQ++;

                QuizQuestion.mesh.text = "Question 2) When does the SERVER specify which CLIENT to recieve from?";
                QuizQuestion.ButtonA.GetComponentInChildren<Text>().text = "Never";
                QuizQuestion.ButtonC.GetComponentInChildren<Text>().text = "After creating the socket";
                QuizQuestion.ButtonB.GetComponentInChildren<Text>().text = "Before closing the socket";
                QuizQuestion.ButtonD.GetComponentInChildren<Text>().text = "After binding the socket";

                break;
            case 2:
                QuizQuestion.ButtonD.GetComponentInChildren<Text>().text = "INCORRECT";

                break;
            case 3:
                QuizQuestion.ButtonD.GetComponentInChildren<Text>().text = "INCORRECT";

                break;
        }
    }

}
