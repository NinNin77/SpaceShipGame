using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipCtrlPanel : MonoBehaviour
{
    // UI
    [SerializeField] private TMPro.TMP_Text _healthText;
    [SerializeField] private TMPro.TMP_Text _healthShieldText;

    [SerializeField] private TMPro.TMP_Text _timerText;
    [SerializeField] private Slider _timerSlider;

    [SerializeField] private TMPro.TMP_Text _powerEngineText;
    [SerializeField] private TMPro.TMP_Text _powerShieldText;
    [SerializeField] private TMPro.TMP_Text _powerLaserText;

    [SerializeField] private TMPro.TMP_Text _inputDirectText;
    [SerializeField] private TMPro.TMP_Text _inputForBackText;
    [SerializeField] private TMPro.TMP_Text _inputOtherText;
    // �ėp
    private GameObject _obj;
    // Spaceship�̏�� (ss=SpaceShip)
    private GameObject _spaceship;
    private HealthSystem _ssHealthSystem;
    private HyperspaceCtrl _ssHyperspaceCtrl;
    // ���̑�
    private MainEngine _mainEngine;
    private SubEngine _subEngine;

    void Start()
    {
        _spaceship = GameObject.Find("Spaceship");
        _ssHealthSystem = _spaceship.GetComponent<HealthSystem>();
        _ssHyperspaceCtrl = _spaceship.GetComponent<HyperspaceCtrl>();

        _obj = GameObject.Find("mainEngine");
        _mainEngine = _obj.GetComponent<MainEngine>();
        _obj = GameObject.Find("subEngine");
        _subEngine = _obj.GetComponent<SubEngine>();
    }

    void Update()
    {
        Health();
        Timer();
    }
    void Health()
    {
        // Health
        var tmp = _ssHealthSystem._health * 10;
        string health = tmp.ToString();
        _healthText.SetText($"Health: {health}%");
    }
    void Timer()
    {
        var tmp = _ssHyperspaceCtrl._currentTimer;
        double dbl = Math.Floor(tmp * 10); //������1�ʂŁA�؂�̂�
        dbl = dbl / 10; //���Ƃ͕ʂ̍s�ňʂ�߂��Ȃ��ƁA�Ȃ����o�O��B

        // Text
        string timer = dbl.ToString("F1"); //F1 = ������1�ʂ܂ŕ\������悤�t�H�[�}�b�g
        _timerText.SetText($"Time: {timer}s");
        // Slider
        _timerSlider.value = (float)_ssHyperspaceCtrl._currentTimer / (float)_ssHyperspaceCtrl._maxTimer;
    }
    void Power()
    {

    }
}
