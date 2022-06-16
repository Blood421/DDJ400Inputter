using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Knob2ImageFill : MonoBehaviour
{
    private TextMeshProUGUI text;
    void Start()
    {

        KnobInputController knob = GetComponent<KnobInputController>();
        RectTransform rt = transform as RectTransform;
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = knob.GetKnob().ToString();

        Image image = GetComponent<Image>();
        knob.GetInputObservable().Subscribe(x => image.fillAmount = x);
    }
}
