using UnityEngine;
using System.Collections;
using DialoguerCore;
using Yarn.Unity;

public class Scratch : MonoBehaviour
{
    public bool IsApple;


    void Start()
    {
        var runner = FindObjectOfType<DialogueRunner>();
        var introScript = Resources.Load<TextAsset>("Text/Intro");

        runner.AddScript(introScript);

        runner.StartDialogue("Start");
    }
}
