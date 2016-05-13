using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MyDialoguerTheme : MonoBehaviour 
{

    private enum State
    {
        Off,
        Crawling,
        WaitingForInput
    }

    private State _state;

    private TextDisplayGui _textDisplayGui;

    void Awake()
    {
        Dialoguer.Initialize();
        AddDialoguerEvents();
    }

	void Start()
	{
	    _state = State.Off;

	    _textDisplayGui = FindObjectOfType<TextDisplayGui>();
        
        Dialoguer.StartDialogue(DialoguerDialogues.Intro);
	}

    private void AddDialoguerEvents()
    {
        Dialoguer.events.onStarted +=OnStarted;
        Dialoguer.events.onEnded += OnEnded;
        Dialoguer.events.onInstantlyEnded += OnInstantlyEnded;
        Dialoguer.events.onTextPhase += OnTextPhase;
        Dialoguer.events.onWindowClose += OnWindowClose;

        Dialoguer.events.onMessageEvent += OnMessageEvent;
    }

    private void OnStarted()
    {
        StartCoroutine(_textDisplayGui.ShowDialogueWindow());
        Debug.Log("OnStarted");
    }

    private void OnTextPhase(DialoguerTextData data)
    {
        // We can probably import our textcrawler for this
        var crawlTextCoroutine = _textDisplayGui.CrawlText(data.text, () =>
        {
            if (data.windowType == DialoguerTextPhaseType.Text)
            {
                var waitForInputRoutine = WaitForInputDown(KeyCode.X, Dialoguer.ContinueDialogue);

                StartCoroutine(waitForInputRoutine);
                _textDisplayGui.ShowChoices(new List<string>{"Continue"}, i =>
                {
                    StopCoroutine(waitForInputRoutine);
                    Dialoguer.ContinueDialogue();
                });
            }

            if (data.windowType == DialoguerTextPhaseType.BranchedText)
            {
                _textDisplayGui.ShowChoices(data.choices.ToList(), Dialoguer.ContinueDialogue);
            }

            
        });

        StartCoroutine(crawlTextCoroutine);
        StartCoroutine(WaitForInputDown(KeyCode.X, _textDisplayGui.SkipTextCrawl));
        StartCoroutine(WaitForInputDown(KeyCode.Mouse0, _textDisplayGui.SkipTextCrawl));


        Debug.Log("OnTextPhase: " + data.text);
    }

    private void OnEnded()
    {
        StartCoroutine(_textDisplayGui.HideDialogWindow());
        Debug.Log("OnEnded");
    }

    private void OnInstantlyEnded()
    {
        Debug.Log("OnInstantlyEnded");
    }

    private void OnWindowClose()
    {
        Debug.Log("OnWindowClose");
    }

    private void OnMessageEvent(string message, string metadata)
    {
        // I'm assuming this is for having characters do things within a script.
        // We need to confirm whether Dialoguer waits until we're done with the message
        // to continue with the next action.

        throw new System.NotImplementedException();
    }

    private IEnumerator WaitForInputDown(KeyCode keycode, Action callback)
    {
        // If the player was holding the button
        var buttonPressedOnEnter = Input.GetKey(keycode);        
        while (buttonPressedOnEnter)
        {
            buttonPressedOnEnter = Input.GetKey(keycode);

            yield return null;
        }
        
        while (true)
        {
            if (Input.GetKeyDown(keycode))
            {
                callback();
                break;
            }

            yield return null;
        }   
    }
}
