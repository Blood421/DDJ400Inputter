using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class Note2UI : MonoBehaviour
{
    private TextMeshProUGUI text;
    void Start()
    {

        NoteInputController note = GetComponent<NoteInputController>();
        RectTransform rt = transform as RectTransform;
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = note.GetNote().ToString();

        note.GetInputObservable().Subscribe(x => OnOff(x,rt));
    }
    void OnOff(bool value, RectTransform rt)
    {
        if (value)
        {
            rt.GetComponent<Image>().color = Color.red;
        }
        else
        {
            rt.GetComponent<Image>().color = Color.white;
        }
    }
}
