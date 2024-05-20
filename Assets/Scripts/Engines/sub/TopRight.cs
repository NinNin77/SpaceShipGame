using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enginePower‚ðparticle‚Ö’¼—¬‚µ‚Ä‚é‚¾‚¯
/// </summary>
public class TopRight : MonoBehaviour
{
    [SerializeField] public float enginePower;
    [SerializeField] private ParticleSystem particle;

    void Update()
    {
        // •¬ŽË—Ê
        var emission = particle.emission;
        emission.rateOverTime = enginePower;
        // •¬ŽË‘¬“x
        var main = particle.main;
        main.startSpeed = enginePower / 2;
    }
}
