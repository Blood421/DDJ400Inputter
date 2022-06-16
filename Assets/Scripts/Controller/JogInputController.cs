using System;
using System.Collections;
using System.Collections.Generic;
using MidiJack;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;

public class JogInputController : MonoBehaviour
{
    [SerializeField] private MidiChannel channel = MidiChannel.Ch1;
    [SerializeField] private float jogThresholdMinusDir = 0.5f, jogThresholdPlusDir = 0.5f;
    [SerializeField] private int jogKnob = 33;
    Subject<float> inputSubject = new Subject<float>();

    private void Start()
    {
        inputSubject.AddTo(this);
    }

    #region MidiJackTempleteSettings
    private void OnEnable()
    {
        MidiMaster.knobDelegate += JogInput;
    }

    private void OnDisable()
    {
        MidiMaster.knobDelegate -= JogInput;
    }

    private void OnDestroy()
    {
        MidiMaster.knobDelegate -= JogInput;
    }
    #endregion


    public void JogInput(MidiChannel channel, int knob, float value)
    {
        if (this.channel == channel)
        {
            if (knob == jogKnob)
            {
                if (jogThresholdMinusDir > value)
                {
                    inputSubject.OnNext(value - jogThresholdMinusDir);
                }
                else if (jogThresholdPlusDir < value)
                {
                    inputSubject.OnNext(value - jogThresholdPlusDir);
                }
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
        return jogKnob;
    }
}
