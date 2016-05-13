using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextDisplayGui : MonoBehaviour
{

    private GameObject _dialogueWindow;
    private TextCrawler _textCrawler;
    private DialogueWindowText _dialogueWindowText;
    private GameObject _dialogChoicePrefab;


    // Gonna be lazy about initialization with lazy initialization. Can't trust start to run first anyways.
    private GameObject DialogueWindow 
    { 
        get
        {
            if (_dialogueWindow == null)
            {
                return _dialogueWindow = transform.FindChild("DialogWindow").gameObject;
            } 
            return _dialogueWindow;
        } 
    }

    private TextCrawler TextCrawler
    {
        get
        {
            if (_textCrawler == null)
            {
                return _textCrawler = gameObject.AddComponent<TextCrawler>();
            } 
            return _textCrawler;
        }
    }

    private DialogueWindowText DialogueWindowText
    {
        get
        {
            if (_dialogueWindowText == null)
            {
                return _dialogueWindowText = gameObject.GetComponentInChildren<DialogueWindowText>();
            }

            return _dialogueWindowText;
        }
    }

    // This is an IEnumerator so we can wait for animations
    public IEnumerator ShowDialogueWindow()
    {
        // Do a tween animation and wait for it to finish

        // For now just make it draw
        DialogueWindow.SetActive(true);

        yield return null;
    }

    public void Start()
    {
        _dialogChoicePrefab = Resources.Load<GameObject>("Prefabs/Gui/DialogChoiceButton");
    }

    //TODO: Figure out where this even goes ugh
    public void Update()
    {
        
    }

    public IEnumerator CrawlText(string text, Action callback)
    {        
        yield return StartCoroutine(TextCrawler.TextCrawl(text, DialogueWindowText.SetText));

        callback();
    }

    public void SkipTextCrawl()
    {
        if (TextCrawler._inProcess)
        {
            TextCrawler.SkipToEnd();
        }
    }

    public IEnumerator HideDialogWindow()
    {
        DialogueWindow.SetActive(false);

        yield return null;
    }

    public void ShowChoices(List<string> choices, Action<int> onChoice)
    {
        var initialPosition = GameObject.Find("ChoiceInitialPosition").transform.position;

        var buttons = new List<GameObject>();

        for (int i = 0; i < choices.Count; i++)
        {
            var choice = choices[i];
            var button = Instantiate(_dialogChoicePrefab);
            button.transform.parent = transform;
            button.transform.position = new Vector2(initialPosition.x, initialPosition.y - (48f * i));
            button.GetComponentInChildren<Text>().text = choice;

            var choiceIndex = i; // Curse you C# mutability!

            buttons.Add(button);

            button.GetComponent<Button>()
                .onClick
                .AddListener(() =>
                {
                    CleanUpButtons(buttons); // This is gross, but should be called when all the buttons are in the list.
                    onChoice(choiceIndex);
                });
        }
    }

    private void CleanUpButtons(List<GameObject> buttons)
    {
        foreach (var button in buttons)
        {
            Destroy(button);
        }
    }

}
