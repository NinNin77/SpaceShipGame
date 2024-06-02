using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class NiParticle : MonoBehaviour
{
    [SerializeField] private float myScale = 0.1f;
    [SerializeField] private ParticleSystem engine_mainRight;
    [SerializeField] private ParticleSystem engine_mainLeft;
    [SerializeField] private ParticleSystem engine_backRight;
    [SerializeField] private ParticleSystem engine_backLeft;

    // Update is called once per frame
    void Update()
    {
        NiMove_Accel myScript = GetComponent<NiMove_Accel>();
        var emission = engine_mainRight.emission;
        var engine = myScript.engine_mainRight;

        //メイン-右
        emission = engine_mainRight.emission;
        engine = myScript.engine_mainRight;

        emission.rateOverTime = engine * myScale;

        //メイン-左
        emission = engine_mainLeft.emission;
        engine = myScript.engine_mainlLeft;

        emission.rateOverTime = engine * myScale;

        //バッグ-右
        emission = engine_backRight.emission;
        engine = myScript.engine_backRight;

        emission.rateOverTime = engine * myScale;

        //バッグ-左
        emission = engine_backLeft.emission;
        engine = myScript.engine_backLeft;

        emission.rateOverTime = engine * myScale;
    }
}
