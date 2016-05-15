using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Yarn.Unity;

public class Background : MonoBehaviour
{

    private Dictionary<string, Sprite> _backgrounds;

    void Start()
    {
        var intro = Resources.Load<Sprite>("Images/intro-bg");
        var main = Resources.Load<Sprite>("Images/main-bg");

        _backgrounds = new Dictionary<string, Sprite>
        {
            {"intro", intro},
            {"main", main},
        };

    }

    [YarnCommand("show")]
    public void ChangeBackground(string imageName)
    {
        Sprite sprite;
        if (!_backgrounds.TryGetValue(imageName, out sprite))
        {
            Debug.LogError("Sprite " + imageName + "not found in backgrounds! Double check your spelling and casing.");
        }

        GetComponent<Image>().sprite = sprite;
    }
}
