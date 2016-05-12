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
                StartCoroutine(WaitForInput(KeyCode.X, Dialoguer.ContinueDialogue));
            }

            if (data.windowType == DialoguerTextPhaseType.BranchedText)
            {
                _textDisplayGui.ShowChoices(data.choices.ToList());


                var inputRoutines = new List<IEnumerator>();

                Action<int> continueDialog = i =>
                {
                    Dialoguer.ContinueDialogue(i);
                    inputRoutines.ForEach(StopCoroutine);
                };

                inputRoutines = new List<IEnumerator>
                {
                    WaitForInput(KeyCode.Z, () => continueDialog(0)),
                    WaitForInput(KeyCode.X, () => continueDialog(0)),
                    WaitForInput(KeyCode.C, () => continueDialog(0)),
                };

                inputRoutines.ForEach(i => StartCoroutine(i));
            }

            
        });

        StartCoroutine(crawlTextCoroutine);
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

    private IEnumerator WaitForInput(KeyCode keycode, Action callback)
    {
        while (true)
        {
            if (Input.GetKey(keycode))
            {
                callback();
                break;
            }

            yield return null;
        }   
    }
}
