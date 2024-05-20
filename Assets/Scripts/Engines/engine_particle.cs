using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enginePower��particle�ʂ֒������Ă邾��
/// </summary>
public class engine_particle : MonoBehaviour
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
