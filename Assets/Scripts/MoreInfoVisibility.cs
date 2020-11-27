using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreInfoVisibility : MonoBehaviour
{
    private bool isVisible = false;
    private GameObject infoText;
    private GameObject exitButton;

    // Start is called before the first frame update
    void Start()
    {
     infoText = this.transform.GetChild(0).gameObject;   
     exitButton = this.transform.GetChild(1).gameObject;
     this.gameObject.SetActive(false);
    //  infoText.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleInfo() {
        Debug.Log(infoText.name);
        if(isVisible) {
            this.isVisible = false;
            this.infoText.SetActive(false);
            this.gameObject.SetActive(false); 
            // exitButton.SetActive(false);
            
        } 
        else {
            this.isVisible = true;
            this.infoText.SetActive(true); 
            this.gameObject.SetActive(true); 
            exitButton.SetActive(true);

        }
            
    }
}
