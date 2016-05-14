using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Yarn;

public class MyYarnTheme : Yarn.Unity.DialogueUIBehaviour
{
    private TextDisplayGui _textDisplayGui;

    private TextDisplayGui TextDisplayGui
    {
        get
        {
            if (_textDisplayGui == null)
            {
                return _textDisplayGui = FindObjectOfType<TextDisplayGui>();
            }

            return _textDisplayGui;
        }
    }

    private Yarn.OptionChooser SetSelectedOption;
		
    public override IEnumerator RunLine(Line line)
    {

        StartCoroutine(WaitForInputDown(KeyCode.Mouse0, TextDisplayGui.SkipTextCrawl));

        yield return StartCoroutine(TextDisplayGui.CrawlText(line.text, () => { Debug.Log("RunLine Done"); }));

        var waitingForInput = true;
        TextDisplayGui.ShowChoices(new List<string> { "Continue" }, (i) => { waitingForInput = false; });
        while (waitingForInput)
        {
            yield return null;
        }
    }

    public override IEnumerator RunOptions(Options optionsCollection, OptionChooser optionChooser)
    {
        // Record that we're using it
        SetSelectedOption = optionChooser;

        TextDisplayGui.ShowChoices(optionsCollection.options.ToList(), SetOption);

        // Wait until the chooser has been used and then removed (see SetOption below)
        while (SetSelectedOption != null)
        {
            yield return null;
        }
    }

    public void SetOption(int selectedOption)
    {
        // Call the delegate to tell the dialogue system that we've
        // selected an option.
        SetSelectedOption(selectedOption);

        // Now remove the delegate so that the loop in RunOptions will exit
        SetSelectedOption = null;
    }

    public override IEnumerator RunCommand(Command command)
    {
        Debug.Log(command.text);

        yield return null;
    }

    public override IEnumerator DialogueStarted()
    {
        yield return StartCoroutine(TextDisplayGui.ShowDialogueWindow());
    }

    // Yay we're done. Called when the dialogue system has finished running.
    public override IEnumerator DialogueComplete()
    {
        yield return StartCoroutine(TextDisplayGui.HideDialogWindow());
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
