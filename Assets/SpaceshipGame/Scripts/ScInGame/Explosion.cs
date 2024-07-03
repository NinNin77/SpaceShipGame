using System;
using System.ComponentModel;
using System.Drawing;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [Header("Animator")]
    [SerializeField] private float _animScale = 1.5f;
    [SerializeField] private float _sizeWithoutAnim = 0.5f;
    [Header("Particle")]
    [SerializeField] private GameObject _objParticle;
    [SerializeField] private float _parScale = 1.5f;
    [Tooltip("ライフタイム")]
    [SerializeField] bool _mainLife = true;
    [SerializeField] private float _mainLifeMin = 0.1f;
    [SerializeField] private float _mainLifeMax = 0.4f;
    [Tooltip("速度")]
    [SerializeField] bool _mainSpeed = true;
    [SerializeField] private float _mainSpeedMin = 0.5f;
    [SerializeField] private float _mainSpeedMax = 5f;
    [Tooltip("大きさ")]
    [SerializeField] bool _mainSize = true;
    [SerializeField] private float _mainSizeMin = 0.005f;
    [SerializeField] private float _mainSizeMax = 0.05f;
    [Tooltip("個数")]
    [SerializeField] bool _burst = true;
    [SerializeField] private float _burstMinCount = 15;
    [SerializeField] private float _burstMaxCount = 25;
    [Header("Audio")]
    [SerializeField] private GameObject _objAudio;
    [SerializeField] private float _audioVolume = 1.0f;
    [SerializeField] private float _audioPitch = 1.5f;

    private void Start()
    {
        if (_parent == null)// 不安定で再現性のない意味不明エラー
        {
            Debug.LogWarning("Start: _parent is null", gameObject);
        }
    }

    /// <summary>インスタンスを生成し、アニメーションを再生する</summary>
    public void Play(float size, Vector3 pos)
    {
        Vector3 orgScale = this.transform.localScale;

        // インスタンス 生成
        if (_parent == null)
        {
            Debug.LogWarning("_parent is null",gameObject);
            _parent = GameObject.Find("ExplosionEffects");//_parent を再設定 *唯一バグが治った？処理
        }
        GameObject ins = Instantiate(this.gameObject, pos, transform.rotation, _parent.transform);

        // Onにする
        ins.SetActive(true);

        // アニメーション
        Animator animator = ins.GetComponent<Animator>();
        if (size < _sizeWithoutAnim) //最小siza以下だったら、アニメーションを表示しない
        {
            animator.enabled = false;
        }
        ins.transform.localScale = new Vector3(orgScale[0] * size * _animScale, orgScale[1] * size * _animScale, orgScale[2]); //スケールを直接いじる

        // 音
        Audio(size, pos);

        // パーティクル
        Particle(size, pos);
    }

    void Particle(float size, Vector3 pos)
    {
        GameObject insPar = Instantiate(_objParticle, pos, transform.rotation, _parent.transform); //インスタンス生成
        //変数
        ParticleSystem insPS = insPar.GetComponent<ParticleSystem>();
        var insPSmain = insPS.main;

        //各種設定
        //ライフタイム
        if (_mainLife == true) { insPSmain.startLifetime = new ParticleSystem.MinMaxCurve(size * _mainLifeMin * _parScale, size * _mainLifeMax * _parScale); }
        //スピード
        if (_mainSpeed == true) { insPSmain.startSpeed = new ParticleSystem.MinMaxCurve(size * _mainSpeedMin * _parScale, size * _mainSpeedMax * _parScale); }
        //サイズ
        if (_mainSize == true) { insPSmain.startSize = new ParticleSystem.MinMaxCurve(size * _mainSizeMin * _parScale, size * _mainSizeMax * _parScale); }
        //バースト
        if (_burst == true)
        {
            short burstMinCount = (short)(size * _burstMinCount * _parScale);
            short burstMaxCount = (short)(size * _burstMaxCount * _parScale);
            var newBurst = new ParticleSystem.Burst(0f, burstMinCount, burstMaxCount);// 設定をまとめる

            insPS.emission.SetBursts(new ParticleSystem.Burst[0]);// 現在のバースト設定をクリア
            insPS.emission.SetBursts(new ParticleSystem.Burst[] { newBurst });// 新しいバースト設定を追加
        }
    }

    void Audio(float size, Vector3 pos)
    {
        float myVolume = 0.0f;
        float myPicth = 0.0f;

        GameObject insAud = Instantiate(_objAudio, pos, transform.rotation, _parent.transform); //インスタンス生成
        AudioSource audio = insAud.GetComponent<AudioSource>();

        // ボリューム
        myVolume = (0.5f * size) * _audioVolume;
        audio.volume = Mathf.Clamp(myVolume, 0f, 1f); //音、下限、上限
        //Debug.Log("audio.volume: " + audio.volume);

        // ピッチ
        myPicth = (1.5f - size) * _audioPitch;
        audio.pitch = Mathf.Clamp(myPicth, 0.8f, 1.5f); //下限と上限の範囲内に制限する関数
    }
}