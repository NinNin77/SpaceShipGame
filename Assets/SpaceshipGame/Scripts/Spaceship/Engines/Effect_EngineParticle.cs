using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enginePower��particle�ʂ֒������Ă邾��
/// </summary>
public class Effect_EngineParticle : MonoBehaviour
{
    [SerializeField] public float _particlePower;
    [SerializeField] private float _particleEmission = 1f;
    [SerializeField] private float _particleSpeed = 0.7f;

    [SerializeField] private GameObject _particle;
    ParticleSystem _ps;

    private void Start()
    {
        GameObject obj = Instantiate(_particle, this.transform.position, this.transform.rotation);
        obj.transform.parent = transform;
        _ps = obj.GetComponent<ParticleSystem>();
    }
    void Update()
    {
        // ���˗�
        var psEmission = _ps.emission;
        psEmission.rateOverTime = _particlePower * _particleEmission;
        // ���ˑ��x
        var psMain = _ps.main;
        psMain.startSpeed = _particlePower * _particleSpeed;
    }
}
