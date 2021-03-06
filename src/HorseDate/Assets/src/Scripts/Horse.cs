﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
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
            transform.DOLocalMove(new Vector3(-550, -90, 0), 1f).SetEase(Ease.OutExpo);
        }

        if (position == "left")
        {
            transform.DOLocalMove(new Vector3(-2000, -90, 0), 1f).SetEase(Ease.OutExpo);
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
        transform.DOShakePosition(100f, Vector2.one * 5f, 15).SetLoops(-1);
    }
}
