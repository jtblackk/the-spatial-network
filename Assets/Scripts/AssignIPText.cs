using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AssignIPText : MonoBehaviour
{
    private GameObject module;
    private ModuleInfo ipStore;
    private string ipToAssign;
    private GameObject text;
    TextMeshPro ipTextChange;
    // Start is called before the first frame update
    void Start()
    {
        module = this.transform.parent.gameObject;
        ipStore = module.GetComponent<ModuleInfo>();

        ipToAssign = ipStore.ip;
        text = this.transform.GetChild(0).gameObject;
        
        ipTextChange = text.GetComponent<TextMeshPro>();
        ipTextChange.text = ipToAssign;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
