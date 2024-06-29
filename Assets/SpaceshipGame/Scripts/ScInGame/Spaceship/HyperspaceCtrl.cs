using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HyperspaceCtrl : MonoBehaviour
{
    //パラメータ
    [SerializeField] public float _maxTimer = 180.0f;
    public float _currentTimer = 0.0f;
    private float _1sCounter = 0.0f;

    private void Start()
    {
        _currentTimer = _maxTimer;
    }

    void Update()
    {
        _currentTimer -= Time.deltaTime;
        _1sCounter += Time.deltaTime;

        // 一秒ごとに呼び出される(完全に正確ではない)
        if (_1sCounter > 1.0f )
        {
            _1sCounter -= 1.0f;

            var tmp = Math.Floor(_currentTimer * 100); //小数第2位で、切り捨て
            tmp = tmp / 100; //↑とは別の行で位を戻さないと、なぜかバグる。
            //Debug.Log("時間 " + tmp + "　秒経過");
        }
    }
}
