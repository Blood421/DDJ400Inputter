using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class RotarySelector2UI : MonoBehaviour
{
    private TextMeshProUGUI knobtext;
    [SerializeField] private GameObject noteUIObj;
    void Start()
    {
        RotarySelectorInputController rotary = GetComponent<RotarySelectorInputController>();
        RectTransform rt0 = transform as RectTransform;
        knobtext = GetComponentInChildren<TextMeshProUGUI>();
        knobtext.text = rotary.GetKnob().ToString();
        rotary.GetInputObservable()
            .Where(x => x == RotarySelectorState.Left || x == RotarySelectorState.Right)
            .Subscribe(x => Rotate(x, rt0));

        RectTransform rt1 = noteUIObj.transform as RectTransform;
        noteUIObj.GetComponentInChildren<TextMeshProUGUI>().text = rotary.GetNote().ToString();
        rotary.GetInputObservable()
            .Where(x => x == RotarySelectorState.On || x == RotarySelectorState.Off)
            .Subscribe(x => OnOff(x, rt1));
    }

    void Rotate(RotarySelectorState state,RectTransform rt)
    {
        if (state == RotarySelectorState.Right)
        {
            rt.Rotate(0,0,-5);
        }
        else if(state == RotarySelectorState.Left)
        {
            rt.Rotate(0, 0, 5);
        }
    }

    void OnOff(RotarySelectorState state, RectTransform rt)
    {
        if (state == RotarySelectorState.Off)
        {
            rt.GetComponent<Image>().color = Color.white;
        }
        else if (state == RotarySelectorState.On)
        {
            rt.GetComponent<Image>().color = Color.red;
        }
    }
}
