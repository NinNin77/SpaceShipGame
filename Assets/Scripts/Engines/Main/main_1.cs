using UnityEngine;

/// <summary>
/// enginePower��particle�֒������Ă邾��
/// </summary>
public class main_1 : MonoBehaviour
{
    [SerializeField] public float enginePower;
    [SerializeField] public ParticleSystem particle;

    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>(); //�������I

        if (particle = null)�@//particle==null �Ȃ炨�m�点
        {
            Debug.Log("No ParticleSystem found");
        }
    }

    void Update()
    {
        //// ���˗�
        //var emission = particle.emission;
        //emission.rateOverTime = enginePower;
        //// ���ˑ��x
        //var main = particle.main;
        //main.startSpeed = enginePower / 2;
    }
}
