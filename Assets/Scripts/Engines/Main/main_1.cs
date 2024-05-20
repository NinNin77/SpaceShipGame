using UnityEngine;

/// <summary>
/// enginePowerをparticleへ直流してるだけ
/// </summary>
public class main_1 : MonoBehaviour
{
    [SerializeField] public float enginePower;
    [SerializeField] public ParticleSystem particle;

    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>(); //ここだ！

        if (particle = null)　//particle==null ならお知らせ
        {
            Debug.Log("No ParticleSystem found");
        }
    }

    void Update()
    {
        //// 噴射量
        //var emission = particle.emission;
        //emission.rateOverTime = enginePower;
        //// 噴射速度
        //var main = particle.main;
        //main.startSpeed = enginePower / 2;
    }
}
