using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[RequireComponent(typeof(RotarySelectorInputController))]
public class RotarySelector2EffectPresenter : MonoBehaviour
{
    private RotarySelectorInputController RotarySelectorInputController; 
    private void Awake()
    {
        RotarySelectorInputController = GetComponent<RotarySelectorInputController>();
    }
    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
        {
            DDJ400_2_Effects ddj4002Effects = obj.GetComponent<DDJ400_2_Effects>();
            if (ddj4002Effects != null)
            {
                RotarySelectorInputController.GetInputObservable().Subscribe(x => ddj4002Effects.UpdateEffectsInputReceive(x));
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
