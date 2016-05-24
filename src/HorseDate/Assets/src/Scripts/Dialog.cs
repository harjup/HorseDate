using UnityEngine;
using System.Collections;
using System.Linq.Expressions;
using Yarn.Unity;

public class Dialog : MonoBehaviour
{
    public bool IsApple;

    private DialogueRunner _runner;

    private DialogueRunner Runner
    {
        get { return _runner ?? (_runner = FindObjectOfType<DialogueRunner>()); }
    }

    void Start()
    {
        var introScript = Resources.Load<TextAsset>("Text/Intro");
        Runner.AddScript(introScript);
        Runner.StartDialogue("Start");
    }

    [YarnCommand("run")]

    public void Run(string startNode)
    {
        if (startNode.ToLower() == "field")
        {
            Runner.StartDialogue("Field");
        }
    }
}
