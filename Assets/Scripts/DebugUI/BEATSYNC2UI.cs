using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class BEATSYNC2UI : MonoBehaviour
{
    private TextMeshProUGUI text;
    private bool isOn = false;
    void Start()
    {

        NoteInputController note = GetComponent<NoteInputController>();
        RectTransform rt = transform as RectTransform;
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = note.GetNote().ToString();

        note.GetInputObservable().Subscribe(x => OnOff(x, rt));
    }
    void OnOff(bool value, RectTransform rt)
    {
        if (value)
        {
            if (isOn)
            {
                rt.GetComponent<Image>().color = Color.white;
                isOn = false;
            }
            else
            {
                rt.GetComponent<Image>().color = Color.red;
                isOn = true;
            }
        }
    }
}
