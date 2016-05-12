using UnityEngine;
using System.Collections;
using DialoguerCore;

public class Scratch : MonoBehaviour
{
    public bool IsApple;

	// Update is called once per frame
	void Update () 
    {
        IsApple = Dialoguer.GetGlobalBoolean(0);
	}
}
