using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;

public class BranchingDialogController : MonoBehaviour
{

    [SerializeField] private GameObject branchingCanvas;
    [SerializeField] private GameObject dialogPrefab;
    [SerializeField] private GameObject choicePrefab;
    [SerializeField] private GameObject dialogHolder;
    [SerializeField] private GameObject choiceHolder;
    [SerializeField] private TextAssetValue dialogValue;
    [SerializeField] private Story myStory;
    [SerializeField] private ScrollRect dialogScroll;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableCanvas()
    {
        branchingCanvas.SetActive(true);
        SetStory();
        RefreshView();
    }

    public void SetStory()
    {
        if (dialogValue.value)
        {
            DeleteOldDialog();
            myStory = new Story(dialogValue.value.text);
        }
        else
        {
            Debug.Log("Story cannot be seted inside branching dialog controller");
        }
    }

    void DeleteOldDialog()
    {
        for (int i = 0; i < dialogHolder.transform.childCount; i++)
        {
            Destroy(dialogHolder.transform.GetChild(i).gameObject);
        }
    }

    public void RefreshView()
    {
        while(myStory.canContinue)
        {
            MakeNewDialog(myStory.Continue());
        }
        if (myStory.currentChoices.Count > 0)
        {
            MakeNewChoice();
        }
        else
        {
            branchingCanvas.SetActive(false);
        }
        StartCoroutine(ScrollCo());
    }

    IEnumerator ScrollCo()
    {
        yield return null;
        dialogScroll.verticalNormalizedPosition = 0f;
    }

    void MakeNewDialog(string newDialog)
    {
        DialogObject newDialogObject = Instantiate(dialogPrefab, dialogHolder.transform).GetComponent<DialogObject>();
        newDialogObject.Setup(newDialog);
    }

    void MakeNewResponce(string newDialog, int choiceValue)
    {
        ResponceObject newResponceObject = Instantiate(choicePrefab, choiceHolder.transform).GetComponent<ResponceObject>();
        newResponceObject.Setup(newDialog, choiceValue);
        Button responceButton = newResponceObject.gameObject.GetComponent<Button>();
        if (responceButton)
        {
            responceButton.onClick.AddListener(delegate { ChooseChoice(choiceValue); });
        }
    }

    void MakeNewChoice()
    {
        for (int i = 0; i < choiceHolder.transform.childCount; i++)
        {
            Destroy(choiceHolder.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < myStory.currentChoices.Count; i++)
        {
            MakeNewResponce(myStory.currentChoices[i].text, i);
        }
    }

    void ChooseChoice(int choice)
    {
        myStory.ChooseChoiceIndex(choice);
        RefreshView();
    }
}
