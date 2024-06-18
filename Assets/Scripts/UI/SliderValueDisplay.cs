using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshProを使用

public class SliderValueDisplay : MonoBehaviour
{
    [SerializeField] public Slider _slider;  // スライダー
    [SerializeField] public TextMeshProUGUI _textTMP;  // TextMeshPro
    [SerializeField] public Text _textLegacy;  // 通常のText

    public string _unit = "%";  // 単位を指定
    public float _scale = 1.0f; // スケーリングを指定
    public string _format = "F1"; // フォーマットを指定

    void Start()
    {
        // スライダーの初期値をテキストに表示
        UpdateValueText();

        // スライダーの値が変更された時に呼ばれるメソッドを登録
        _slider.onValueChanged.AddListener(delegate { UpdateValueText(); });
    }

    // テキストを更新するメソッド
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
            Debug.LogError("TextMeshProまたはTextLegacyがアタッチされていません。");
        }
    }
}
