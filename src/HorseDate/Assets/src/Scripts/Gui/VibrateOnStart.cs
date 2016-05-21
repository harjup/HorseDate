using UnityEngine;
using System.Collections;
using DG.Tweening;

public class VibrateOnStart : MonoBehaviour
{
    public float Duration = 100f;
    public float Strength = 5f;
    public int Vibrato = 15;

	void Start () 
    {
        transform.DOShakePosition(Duration, Vector2.one * Strength, Vibrato).SetLoops(-1);
	}
}
