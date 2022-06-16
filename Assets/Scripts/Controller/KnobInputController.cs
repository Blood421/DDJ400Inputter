using System;
using System.Collections;
using System.Collections.Generic;
using MidiJack;
using UnityEngine;
using UniRx;

public class KnobInputController : MonoBehaviour
{
    [SerializeField] private MidiChannel channel = MidiChannel.Ch1;
    Subject<float> inputSubject = new Subject<float>();
    [SerializeField] private int knob = 0;
    [SerializeField] private float threshold = 0.5f;
    void Start()
    {
        inputSubject.AddTo(this);
    }
    #region MidiJackTempleteSettings
    private void OnEnable()
    {
        MidiMaster.knobDelegate += KnobInput;
    }

    private void OnDisable()
    {
        MidiMaster.knobDelegate -= KnobInput;
    }

    private void OnDestroy()
    {
        MidiMaster.knobDelegate -= KnobInput;
    }
    #endregion

    private void KnobInput(MidiChannel channel, int knob, float value)
    {
        if (this.channel == channel)
        {
            if (this.knob == knob)
            {
                inputSubject.OnNext(value);
            }
        }
    }
    public IObservable<float> GetInputObservable()
    {
        return inputSubject;
    }
    public MidiChannel GetChannel()
    {
        return channel;
    }
    public int GetKnob()
    {
        return knob;
    }
}
