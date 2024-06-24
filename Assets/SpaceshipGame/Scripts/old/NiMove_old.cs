using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiMove : MonoBehaviour
{
    [SerializeField] float rotat_speed = 50.0f;
    [SerializeField] float acceleration = 10.0f;
    [SerializeField] float brake = 3.0f;
    //Rigidbody2D myRigidbody;

    void Update()
    {
        //awake
        //myRigidbody = GetComponent<Rigidbody2D>();

        // W.. 加速
        if (Input.GetKey(KeyCode.W))
            transform.position += transform.up * acceleration * Time.deltaTime;
        //myRigidbody.AddForce(transform.up * acceleration);

        // S.. ブレーキ
        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.up * brake * Time.deltaTime;
        //myRigidbody.AddForce(-transform.up * brake);

        // D.. 右回転
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(new Vector3(0f, 0f, -rotat_speed * Time.deltaTime));

        // A.. 左回転
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(new Vector3(0f, 0f, rotat_speed * Time.deltaTime));
    }
}