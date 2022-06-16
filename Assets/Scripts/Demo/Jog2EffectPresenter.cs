using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[RequireComponent(typeof(JogInputController))]
public class Jog2EffectPresenter : MonoBehaviour
{
    private JogInputController jogInputController;
    private void Awake()
    {
        jogInputController = GetComponent<JogInputController>();
    }

    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
        {
            DDJ400_2_Effects ddj4002Effects = obj.GetComponent<DDJ400_2_Effects>();
            if (ddj4002Effects != null)
            {
                jogInputController.GetInputObservable().Subscribe(x => ddj4002Effects.UpdateParticleSpeed(x));
            }
            else
            {
                Debug.Log("DDJ400_2_Effects Not Attached");
            }
        }
        else
        {
            Debug.Log("Player Tag Not Found");
        }


    }
    
}
