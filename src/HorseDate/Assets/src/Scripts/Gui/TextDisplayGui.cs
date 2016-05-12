using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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


    public IEnumerator CrawlText(string text, Action callback)
    {
        yield return StartCoroutine(TextCrawler.TextCrawl(text, DialogueWindowText.SetText));

        callback();
    }

    public IEnumerator HideDialogWindow()
    {
        DialogueWindow.SetActive(false);

        yield return null;
    }

    public void ShowChoices(List<string> choices)
    {
        if (_dialogChoicePrefab == null)
        {
            _dialogChoicePrefab = Resources.Load<GameObject>("Prefabs/Gui/DialogChoiceButton");
        }

        var initialPosition = GameObject.Find("ChoiceInitialPosition").transform.position;


        for (int index = 0; index < choices.Count; index++)
        {
            var choice = choices[index];
            var instance = Instantiate(_dialogChoicePrefab);
            instance.transform.parent = transform;
            instance.transform.position = new Vector2(initialPosition.x, initialPosition.y - (48f * index));
            instance.GetComponentInChildren<Text>().text = choice;
        }
    }
}
