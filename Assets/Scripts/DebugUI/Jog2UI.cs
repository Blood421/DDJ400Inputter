using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;

public class Jog2UI : MonoBehaviour
{
    private TextMeshProUGUI text;
    void Start()
    {
        JogInputController jog = GetComponent<JogInputController>();
        RectTransform rt =transform as RectTransform;
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = jog.GetKnob().ToString();

        jog.GetInputObservable().Subscribe(x => rt.Rotate(0, 0, -x * 5));
    }
    
}
