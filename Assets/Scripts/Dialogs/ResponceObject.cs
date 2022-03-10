using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResponceObject : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI myText;
    private int choiceValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(string newDialog, int myChoice)
    {
        myText.text = newDialog;
        choiceValue = myChoice;
    }
}
