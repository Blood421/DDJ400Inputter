using System;
using System.Collections;
using System.Collections.Generic;
using MidiJack;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MidiTest : MonoBehaviour
{

    private void OnEnable()
    {
        MidiMaster.noteOnDelegate += NoteOn;
        MidiMaster.noteOffDelegate += NoteOff;
        MidiMaster.knobDelegate += KnobOn;
    }

    private void OnDisable()
    {
        MidiMaster.noteOnDelegate -= NoteOn;
        MidiMaster.noteOffDelegate -= NoteOff;
        MidiMaster.knobDelegate -= KnobOn;
    }

    private void OnDestroy()
    {
        MidiMaster.noteOnDelegate -= NoteOn;
        MidiMaster.noteOffDelegate -= NoteOff;
        MidiMaster.knobDelegate -= KnobOn;
    }

    private void NoteOn(MidiChannel channel, int note, float velocity)
    {
        Debug.Log("NoteOn: " + channel + ", " + note + ", " + velocity);
    }

    private void NoteOff(MidiChannel channel, int note)
    {
        Debug.Log("NoteOff: " + channel + ", " + note);
    }

    private void KnobOn(MidiChannel channel, int knob, float value)
    {
        Debug.Log("Knob: " + channel + ", " + knob + ", " + value);
    }
    
}
