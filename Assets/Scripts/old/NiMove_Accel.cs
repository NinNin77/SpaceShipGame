using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiMove_Accel : MonoBehaviour
{
    // �p�����[�^
    [SerializeField] public float player_accel = 1000.0f;
    [SerializeField] public float player_brake = 500.0f;
    [SerializeField] public float player_rotat = 500.0f;

    //�ϑ�
    public float engine_mainRight = 0f;
    public float engine_mainlLeft = 0f;
    public float engine_backRight = 0f;
    public float engine_backLeft = 0f;

    // Update is called once per frame
    void Update()
    {
        ResetEngine(); //��������G���W�������Z�b�g

        //Rigidbody
        Rigidbody2D myRigidbody;
        myRigidbody = this.GetComponent<Rigidbody2D>();

        // W.. ����
        if (Input.GetKey(KeyCode.W))
        {
            myRigidbody.AddForce(transform.up * player_accel * Time.deltaTime, ForceMode2D.Force);
            engine_mainRight = player_accel / 2;
            engine_mainlLeft = player_accel / 2;
        }

        // S.. �u���[�L
        if (Input.GetKey(KeyCode.S))
        {
            myRigidbody.AddForce(-transform.up * player_brake * Time.deltaTime, ForceMode2D.Force);
            engine_backRight = player_brake / 2;
            engine_backLeft = player_brake / 2;
        }

        // D.. �E��]
        if (Input.GetKey(KeyCode.D))
        {
            myRigidbody.AddTorque(-player_rotat * Time.deltaTime, ForceMode2D.Force);
            engine_mainlLeft = player_rotat / 2;
            engine_backRight = player_rotat / 2;
        }

        // A.. ����]
        if (Input.GetKey(KeyCode.A))
        {
            myRigidbody.AddTorque(player_rotat * Time.deltaTime, ForceMode2D.Force);
            engine_mainRight = player_rotat / 2;
            engine_backLeft = player_rotat / 2;
        }
    }

    void ResetEngine()
    {
        engine_mainRight = 0f;
        engine_mainlLeft = 0f;
        engine_backRight = 0f;
        engine_backLeft = 0f;
    }
}

