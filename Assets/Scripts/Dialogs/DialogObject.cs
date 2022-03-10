using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogObject : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI myText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(string newDialog)
    {
        myText.text = newDialog;
    }
}
