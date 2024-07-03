using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using UnityEngine.EventSystems;

public class ShipCtrlPanel : MonoBehaviour
{
    // UI
    //Health
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _healthShieldText;
    //Timer
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private UnityEngine.UI.Slider _timerSlider;
    //Power
    [SerializeField] private UnityEngine.UI.Slider _powerEngineSlider;
    [SerializeField] private UnityEngine.UI.Slider _powerShieldSlider;
    [SerializeField] private UnityEngine.UI.Slider _powerLaserSlider;
    [SerializeField] private UnityEngine.UI.Slider _powerEngineMainSlider;
    [SerializeField] private UnityEngine.UI.Slider _powerEngineSubSlider;
    //Input
    [SerializeField] private TMP_Text _inputDirectText;
    [SerializeField] private TMP_Text _inputForBackText;
    [SerializeField] private TMP_Text _inputOtherText;
    //Speed
    [SerializeField] private TMP_Text _speedText;
    [SerializeField] private float _speedScale = 1.0f;
    [SerializeField] private string _speedFormat = "000.0 Mag"; // �t�H�[�}�b�g���w��

    // �ėp
    private GameObject _obj;
    // Spaceship�̏�� (ss=SpaceShip)
    private GameObject _spaceship;
    private HealthSystem _ssHealthSystem;
    private ShieldCtrl _ssShieldCtrl;
    private HyperspaceCtrl _ssHyperspaceCtrl;
    private InputCtrl _ssInputCtrl;
    // ���̑�
    private MainEngine _mainEngine;
    private SubEngine _subEngine;

    void Start()
    {
        // �A�T�C��
        _spaceship = GameObject.Find("Spaceship");
        _ssHealthSystem = _spaceship.GetComponent<HealthSystem>();
        _ssShieldCtrl = _spaceship.GetComponent<ShieldCtrl>();
        _ssHyperspaceCtrl = _spaceship.GetComponent<HyperspaceCtrl>();
        _ssInputCtrl = _spaceship.GetComponent<InputCtrl>();

        _obj = GameObject.Find("mainEngine");
        _mainEngine = _obj.GetComponent<MainEngine>();
        _obj = GameObject.Find("subEngine");
        _subEngine = _obj.GetComponent<SubEngine>();

        // �X���C�_�[�̒l���ύX���ꂽ���ɌĂ΂�郁�\�b�h��o�^
        //_slider.onValueChanged.AddListener(delegate { Method(); });
        _powerEngineMainSlider.onValueChanged.AddListener(delegate { SliderValueChanged(_powerEngineMainSlider); });
        _powerEngineSubSlider.onValueChanged.AddListener(delegate { SliderValueChanged(_powerEngineSubSlider); });
    }

    void Update()
    {
        // Get and Display
        GetHealth();
        GetTimer();
        GetPower();
        GetInput();
        GetSpeed();
    }
    // Get
    void GetHealth()
    {
        //Health Text 
        var tmp = _ssHealthSystem._health * 10;
        string health = tmp.ToString("###.00");
        _healthText.SetText($"Health: {health}%");

        // Shield Text
        var tmp2 = _ssShieldCtrl._shield * 10;
        string shield = tmp2.ToString("###.00");
        _healthShieldText.SetText($"Shield: {shield}%");
    }
    void GetTimer()
    {
        var tmp = _ssHyperspaceCtrl._currentTimer;
        double dbl = Math.Floor(tmp * 10); //������1�ʂŁA�؂�̂�
        dbl = dbl / 10; //���Ƃ͕ʂ̍s�ňʂ�߂��Ȃ��ƁA�Ȃ����o�O��B

        // Text
        string timer = dbl.ToString("F1"); //F1 = ������1�ʂ܂ŕ\������悤�t�H�[�}�b�g
        _timerText.SetText($"{timer}s");
        // Slider
        _timerSlider.value = (float)_ssHyperspaceCtrl._currentTimer / (float)_ssHyperspaceCtrl._maxTimer;
    }
    void GetPower()
    {
        // Display1

        // Display2
        // Engine_MainSub
        //Slider
        //main
        _powerEngineMainSlider.maxValue = (float)_mainEngine.MaxPower;
        _powerEngineMainSlider.value = (float)_mainEngine.Power / (float)_mainEngine.MaxPower;
        //sub
        _powerEngineSubSlider.maxValue = (float)_subEngine.MaxPower;
        _powerEngineSubSlider.value = (float)_subEngine.Power / (float)_subEngine.MaxPower;
    }
    void GetInput()
    {
        var list = new List<string>();
        var dict = new Dictionary<string, string>();

        //����Input���擾
        if (_ssInputCtrl != null)
        {
            //Type�HScript�́H�Ȃɂ��ꂨ�������́H
            System.Type scriptType = _ssInputCtrl.GetType();
            //BindingFlags.Public | BindingFlags.Instance���g�p���āAPublic�ȃC���X�^���X�t�B�[���h�݂̂��擾�B
            FieldInfo[] fields = scriptType.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                //�t�B�[���h����"_input"�Ŏn�܂�ꍇ
                if (field.Name.StartsWith("_input"))
                {
                    //�l���������シ��
                    object fieldValue = field.GetValue(_ssInputCtrl);
                    //�l���o�͂���
                    dict.Add(field.Name, fieldValue.ToString());
                }
            }
        }

        string text = "";
        foreach (var item in dict)
        {
            string key = item.Key.Replace("_input", string.Empty); //"_input"�����������B

            text += "> " + key + ": " + item + "\n"; //�g�ݍ��킹��B
        }
        _inputOtherText.SetText($"{text}");
    }
    void GetSpeed()
    {
        Rigidbody2D rgd =_spaceship. GetComponent<Rigidbody2D>();
        // ���x�x�N�g��
        var vector = rgd.velocity;
        // ���x�x�N�g���̑傫��
        var speed = rgd.velocity.magnitude * _speedScale;

        // Text
        string strVector = vector.ToString();
        string strSpeed = speed.ToString(_speedFormat);
        _speedText.SetText($"{strSpeed}");
    }

    // Set
    void SliderValueChanged(UnityEngine.UI.Slider slider)
    {
        // ���ݑ��삳��Ă���UI�v�f���X���C�_�[���ǂ������m�F
        // (�v���C���[�����삵���̂��H Or �X�N���v�g�Ȃǂ���̑��삩�H���ׂ�B)
        if (EventSystem.current.currentSelectedGameObject == slider.gameObject)
        {
            GameObject parent = slider.transform.parent.gameObject;
            UIValueSetter com = parent.GetComponent<UIValueSetter>();

            com.ValueChanged(slider.value);
        }
    }
}