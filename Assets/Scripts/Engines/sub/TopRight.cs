using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enginePower��particle�֒������Ă邾��
/// </summary>
public class TopRight : MonoBehaviour
{
    [SerializeField] public float enginePower;
    [SerializeField] private ParticleSystem particle;

    void Update()
    {
        // ���˗�
        var emission = particle.emission;
        emission.rateOverTime = enginePower;
        // ���ˑ��x
        var main = particle.main;
        main.startSpeed = enginePower / 2;
    }
}
