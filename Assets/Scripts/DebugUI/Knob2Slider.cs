using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Knob2Slider : MonoBehaviour
{
    private TextMeshProUGUI text;
    void Start()
    {

        KnobInputController knob = GetComponent<KnobInputController>();
        RectTransform rt = transform as RectTransform;
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = knob.GetKnob().ToString();
        
        Slider slider = GetComponent<Slider>();
        knob.GetInputObservable().Subscribe(x => slider.value = x);
    }
}
