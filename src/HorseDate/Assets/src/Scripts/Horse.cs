using UnityEngine;
using System.Collections;
using DG.Tweening;
using DialoguerCore;
using DialoguerEditor;
using UnityEngine.UI;
using Yarn.Unity;

public class Horse : MonoBehaviour 
{
    // walk center|left
    // switch brown|striped|bancho
    // vibrate

    private Sprite BrownSprite;
    private Sprite StripedSprite;
    private Sprite BanchoSprite;

    private Image Image;

    public void Start()
    {
        Image = GetComponent<Image>();

        BrownSprite = Resources.Load<Sprite>("Images/brown-horse-transparent");
        StripedSprite = Resources.Load<Sprite>("Images/striped-horse");
        BanchoSprite = Resources.Load<Sprite>("Images/bancho-horse");
    }

    [YarnCommand("walk")]
    public void Walk(string position)
    {
        if (position == "center")
        {
            transform.DOLocalMove(new Vector3(-200, -90, 0), 1f).SetEase(Ease.OutExpo);
        }

        if (position == "left")
        {
            transform.DOLocalMove(new Vector3(-645, -90, 0), 1f).SetEase(Ease.OutExpo);
        }
    }

    [YarnCommand("switch")]
    public void Switch(string skin)
    {
        switch (skin.ToLower())
        {
            case "brown":
                Image.sprite = BrownSprite;
                break;
            case "striped":
                Image.sprite = StripedSprite;
                break;
            case "bancho":
                Image.sprite = BanchoSprite;
                break;
        }
    }

    [YarnCommand("vibrate")]
    public void Vibrate()
    {
        transform.DOShakePosition(1f, Vector2.one * 5f).SetLoops(-1);
    }
}
