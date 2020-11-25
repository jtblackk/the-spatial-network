using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ServerInstructions : MonoBehaviour
{
    private GameObject module;
    private ModuleInfo modInfo;
    private string instructions;
    private GameObject text;
    public static TextMeshPro screen;

    // Start is called before the first frame update
    void Start()
    {
        module = this.transform.parent.gameObject;
        modInfo = module.GetComponent<ModuleInfo>();

        //instructions = modInfo.instructions;
        text = this.transform.GetChild(0).gameObject;

        screen = text.GetComponent<TextMeshPro>();
        screen.text = "SERVER MACHINE";

    }

    // Update is called once per frame
    void Update()
    {

    }
}
