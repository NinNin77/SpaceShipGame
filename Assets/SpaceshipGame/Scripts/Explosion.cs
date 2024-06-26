using System.Drawing;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private float _animScale = 1.0f;
    [SerializeField] private float _sizeWithoutAnim = 0.5f;
    [Header("Particle")]
    [SerializeField] private GameObject _objParticle;
    [SerializeField] private float _parScale = 1.0f;
    [SerializeField] bool _mainLife = true;
    [SerializeField] private float _mainLifeMin = 0.2f;
    [SerializeField] private float _mainLifeMax = 0.8f;
    [SerializeField] bool _mainSpeed = true;
    [SerializeField] private float _mainSpeedMin = 2;
    [SerializeField] private float _mainSpeedMax = 20;
    [SerializeField] bool _mainSize = true;
    [SerializeField] private float _mainSizeMin = 0.01f;
    [SerializeField] private float _mainSizeMax = 0.1f;
    [SerializeField] bool _burst = true;
    [SerializeField] private float _burstMinCount = 15;
    [SerializeField] private float _burstMaxCount = 25;
    [Header("Audio")]
    [SerializeField] private float _audioScale = 1.0f;

    /// <summary>インスタンスを生成し、アニメーションを再生する</summary>
    public void Play(float _size, Vector3 _pos)
    {
        Vector3 orgScale = this.transform.localScale;

        // インスタンス 生成
        GameObject ins = Instantiate(this.gameObject, _pos, transform.rotation);

        // アニメーション
        Animator animator = ins.GetComponent<Animator>();
        if (_size < _sizeWithoutAnim) //最小siza以下だったら、アニメーションを表示しない
        {
            animator.enabled = false;
        }
        ins.transform.localScale = new Vector3(orgScale[0] * _size * _animScale, orgScale[1] * _size * _animScale, orgScale[2]); //スケールを直接いじる

        // 音
        AudioSource audio = ins.GetComponent<AudioSource>();
        audio.volume = Mathf.Clamp(_size * _audioScale, 0f, 1f); //音。仕組みが謎。ナニコレ。

        // パーティクル
        GameObject insPar = Instantiate(_objParticle, _pos, transform.rotation);
        ParticleSystem insPS = insPar.GetComponent<ParticleSystem>();
        var insPSmain = insPS.main;
        //各種設定
        //ライフタイム
        if(_mainLife == true) { insPSmain.startLifetime = new ParticleSystem.MinMaxCurve(_size * _mainLifeMin * _parScale, _size * _mainLifeMax * _parScale); }
        //スピード
        if (_mainSpeed == true) { insPSmain.startSpeed = new ParticleSystem.MinMaxCurve(_size * _mainSpeedMin * _parScale, _size * _mainSpeedMax * _parScale); }
        //サイズ
        if (_mainSize == true) { insPSmain.startSize = new ParticleSystem.MinMaxCurve(_size * _mainSizeMin * _parScale, _size * _mainSizeMax * _parScale); }
        //バースト
        if (_burst == true)
        {
            short burstMinCount = (short)(_size * _burstMinCount * _parScale);
            short burstMaxCount = (short)(_size * _burstMaxCount * _parScale);
            var newBurst = new ParticleSystem.Burst(0f, burstMinCount, burstMaxCount);// 設定をまとめる
            insPS.emission.SetBursts(new ParticleSystem.Burst[0]);// 現在のバースト設定をクリア
            insPS.emission.SetBursts(new ParticleSystem.Burst[] { newBurst });// 新しいバースト設定を追加
        }
    }
}