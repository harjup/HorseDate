using UnityEngine;
using System.Collections;
using System.Linq;

public class TitleInput : MonoBehaviour 
{	
	// Update is called once per frame
	void Update ()
	{
	    var touches = Input.touches;
	    if (touches.Any(t => t.phase == TouchPhase.Began))
	    {
	        LoadIntro();
	    }

	    if (Input.GetKey(KeyCode.Mouse0))
	    {
            LoadIntro();
	    }
	}

    public void LoadIntro()
    {
        Application.LoadLevel("Intro");
    }


}
