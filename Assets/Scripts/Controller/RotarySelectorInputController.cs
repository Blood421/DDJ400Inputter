using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MidiJack;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

public enum RotarySelectorState
{
    Right,
    Left,
    On,
    Off,
}
public class RotarySelectorInputController : MonoBehaviour
{

    [SerializeField] private MidiChannel channel = MidiChannel.Ch7;                           //DDJ400 -> Ch7
    RotarySelectorState state = RotarySelectorState.Right;
    [SerializeField] private float thresholdValue = 0.5f;
    [SerializeField] private bool valueMirror = false;
    private Subject<RotarySelectorState> inputSubject = new Subject<RotarySelectorState>();

    private float time = 10;
    private float UpdateTime = 0.05f;

    [SerializeField] private int rotarySelectorKnob = 64;                                  //DDJ400 -> 64
    [SerializeField] private int rotarySelectorButtonNote = 65;                            //DDJ400 -> 65
    void Start()
    {
        inputSubject.AddTo(this);
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    #region MidiJackTempleteSettings
    private void OnEnable()
    {
        MidiMaster.knobDelegate += RotarySelectorInput;
        MidiMaster.noteOnDelegate += RotarySelectorButtonInput;
    }

    private void OnDisable()
    {
        MidiMaster.knobDelegate -= RotarySelectorInput;
        MidiMaster.noteOnDelegate -= RotarySelectorButtonInput;
    }

    private void OnDestroy()
    {
        MidiMaster.knobDelegate -= RotarySelectorInput;
        MidiMaster.knobDelegate -= RotarySelectorButtonInput;
    }
    #endregion

    public void RotarySelectorInput(MidiChannel channel, int knob, float value)
    {
        if (this.channel == channel)
        {
            if (rotarySelectorKnob == knob)
            {
                if (value > thresholdValue)
                {
                    if (valueMirror) state = RotarySelectorState.Right;
                    else state = RotarySelectorState.Left;
                }
                else
                {
                    if (valueMirror) state = RotarySelectorState.Left;
                    else state = RotarySelectorState.Right;
                }
                inputSubject.OnNext(state);
            }
        }
    }

    public void RotarySelectorButtonInput(MidiChannel channel, int note, float velocity)
    {
        if (this.channel == channel)
        {
            if (rotarySelectorButtonNote == note)
            {
                if (velocity > 0.5f)
                {
                    if (time > UpdateTime)
                    {
                        state = RotarySelectorState.On;
                    }
                    time = 0;
                }
                else
                {
                    state = RotarySelectorState.Off;
                }
                inputSubject.OnNext(state);
            }
        }
    }
    public IObservable<RotarySelectorState> GetInputObservable()
    {
        return inputSubject;
    }

    public MidiChannel GetChannel()
    {
        return channel;
    }
    public int GetKnob()
    {
        return rotarySelectorKnob;
    }
    public int GetNote()
    {
        return rotarySelectorButtonNote;
    }
}
