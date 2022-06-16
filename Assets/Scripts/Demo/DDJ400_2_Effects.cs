using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using System.Linq;

public class DDJ400_2_Effects : MonoBehaviour
{
    [SerializeField] private List<GameObject> effectObjs;
    List<ParticleSystem> effects;
    [SerializeField] private float effectSpeedMultiply = 1f;

    [SerializeField] private float particleSimSpeed = 1;
    [SerializeField] private int particleNum = 0;

    private void Start()
    {
        UpdateEffects(particleNum);
        UpdateParticleSpeed(particleSimSpeed);
    }

    public void UpdateEffectsInputReceive(RotarySelectorState state)
    {
        if (state == RotarySelectorState.Right)
        {
            UpdateEffects(1);
            UpdateParticleSpeed(0);
        }
        else if (state == RotarySelectorState.Left)
        {
            UpdateEffects(-1);
            UpdateParticleSpeed(0);
        }
    }
    private void UpdateEffects(int num)
    {

        //ParticleNum Update.
        particleNum += num;
        if(particleNum < 0)particleNum = 0;
        if (particleNum >= effectObjs.Count) particleNum = effectObjs.Count - 1;

        //Active GameObjects Set Not Active.
        foreach (var o in effectObjs.Where(x => x.activeSelf))
        {
            o.SetActive(false);
        }

        effectObjs[particleNum].SetActive(true);

        //Effect List Update.
        effects = new List<ParticleSystem>();
        foreach (var part in effectObjs[particleNum].GetComponentsInChildren<ParticleSystem>())
        {
            effects.Add(part);
        }
    }

    public void UpdateParticleSpeed(float value)
    {
        //ParticleSimSpeed Update.
        particleSimSpeed += value * effectSpeedMultiply;
        if (particleSimSpeed < 0) particleSimSpeed = 0;
        //ParticeSystem's SimSpeed Update.
        foreach (var part in effects)
        {
            ParticleSystem.MainModule main = part.main;
            main.simulationSpeed = particleSimSpeed;
        }
    }
}
