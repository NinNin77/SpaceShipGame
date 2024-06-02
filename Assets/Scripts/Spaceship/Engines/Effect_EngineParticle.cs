using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enginePower��particle�ʂ֒������Ă邾��
/// </summary>
public class Effect_EngineParticle : MonoBehaviour
{
    [SerializeField] public float enginePower;
    [SerializeField] private GameObject particle;
    ParticleSystem ps;

    private void Start()
    {
        GameObject obj = Instantiate(particle, this.transform.position, this.transform.rotation);
        obj.transform.parent = transform;
        ps = obj.GetComponent<ParticleSystem>();
    }
    void Update()
    {
        // ���˗�
        var emission = ps.emission;
        emission.rateOverTime = enginePower;
        // ���ˑ��x
        var main = ps.main;
        main.startSpeed = enginePower / 2;
    }
}
