using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OverworldRoom : Room
{
    public BoolValue starter;
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    // Start is called before the first frame update
    void Start()
    {
        if (starter.RuntimeValue)
        {
            dialogBox.SetActive(true);
            dialogText.text = dialog;
            starter.RuntimeValue = false;
            StartCoroutine(Co_OnEnter(6));
        }
    }

    private IEnumerator Co_OnEnter(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialogBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
