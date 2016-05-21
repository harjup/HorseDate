using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour 
{
    public void OnStartClicked()
    {
        Application.LoadLevel("Intro");
    }
}
