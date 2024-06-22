using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ToggleExtension : MonoBehaviour
{
    [SerializeField] public Toggle _targetToggle;
    [SerializeField] public Slider _thisSlider;

    void Start()
    {
        if (_thisSlider != null && _targetToggle != null)
        {
            _thisSlider.onValueChanged.AddListener(ThisSliderValueChanged);
        }
        else
        {
            Debug.LogError("_thisSlider or _targetToggle is not assigned.", this.gameObject);
        }
    }

    void ThisSliderValueChanged(float value)
    {
        // 現在操作されているUI要素がスライダーかどうかを確認
        // (プレイヤーが操作したのか？ Or スクリプトなどからの操作か？)
        if (EventSystem.current.currentSelectedGameObject == _thisSlider.gameObject)
        {
            _targetToggle.isOn = true;
            _targetToggle.Select();
        }
    }
}
