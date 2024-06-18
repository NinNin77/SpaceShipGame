using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshPro���g�p

public class SliderValueDisplay : MonoBehaviour
{
    [SerializeField] public Slider _slider;  // �X���C�_�[
    [SerializeField] public TextMeshProUGUI _textTMP;  // TextMeshPro
    [SerializeField] public Text _textLegacy;  // �ʏ��Text

    public string _unit = "%";  // �P�ʂ��w��
    public float _scale = 1.0f; // �X�P�[�����O���w��
    public string _format = "F1"; // �t�H�[�}�b�g���w��

    void Start()
    {
        // �X���C�_�[�̏����l���e�L�X�g�ɕ\��
        UpdateValueText();

        // �X���C�_�[�̒l���ύX���ꂽ���ɌĂ΂�郁�\�b�h��o�^
        _slider.onValueChanged.AddListener(delegate { UpdateValueText(); });
    }

    // �e�L�X�g���X�V���郁�\�b�h
    private void UpdateValueText()
    {
        float scaledValue = _slider.value * _scale;
        string tmpText = scaledValue.ToString(_format) + _unit;

        if (_textTMP != null)
        {
            _textTMP.text = tmpText;
        }
        else if (_textLegacy != null)
        {
            _textLegacy.text = tmpText;
        }
        else
        {
            Debug.LogError("TextMeshPro�܂���TextLegacy���A�^�b�`����Ă��܂���B");
        }
    }
}
