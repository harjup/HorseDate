using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextDisplayGui : MonoBehaviour
{

    private GameObject _dialogueWindow;

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

    private TextCrawler _textCrawler;


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

    private DialogueWindowText _dialogueWindowText;

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

    public void ShowChoices(List<string> toList)
    {
        
    }
}
