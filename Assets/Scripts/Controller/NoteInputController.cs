using System;
using System.Collections;
using System.Collections.Generic;
using MidiJack;
using UniRx;
using UnityEngine;

public class NoteInputController : MonoBehaviour
{

    [SerializeField] private MidiChannel channel = MidiChannel.Ch1;
    Subject<bool> inputSubject = new Subject<bool>();
    [SerializeField] private int note = 0;
    [SerializeField] private float threshold = 0.5f;
    void Start()
    {
        inputSubject.AddTo(this);
    }
    #region MidiJackTempleteSettings
    private void OnEnable()
    {
        MidiMaster.noteOnDelegate += NoteInput;
    }

    private void OnDisable()
    {
        MidiMaster.noteOnDelegate -= NoteInput;
    }

    private void OnDestroy()
    {
        MidiMaster.noteOnDelegate -= NoteInput;
    }
    #endregion

    private void NoteInput(MidiChannel channel, int note, float velocity)
    {
        if (this.channel == channel)
        {
            if (this.note == note)
            {
                if (velocity > threshold)
                {
                    inputSubject.OnNext(true);
                }
                else
                {
                    inputSubject.OnNext(false);
                }
            }
        }
    }
    public IObservable<bool> GetInputObservable()
    {
        return inputSubject;
    }
    public MidiChannel GetChannel()
    {
        return channel;
    }
    public int GetNote()
    {
        return note;
    }
}
