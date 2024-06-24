using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject _objParticle;
    [SerializeField] private float _mainLifeMin = 0.2f;
    [SerializeField] private float _mainLifeMax = 0.8f;
    [SerializeField] private float _mainSpeedMin = 2;
    [SerializeField] private float _mainSpeedMax = 20;
    [SerializeField] private float _mainSizeMin = 0.01f;
    [SerializeField] private float _mainSizeMax = 0.1f;
    [SerializeField] private float _burstMinCount = 15;
    [SerializeField] private float _burstMaxCount = 25;

    /// <summary>インスタンスを生成し、アニメーションを再生する</summary>
    public void Play(float _size, Vector3 _pos)
    {
        Vector3 orgScale = this.transform.localScale;

        // インスタンス
        GameObject ins = Instantiate(this.gameObject, _pos, transform.rotation);
        ins.transform.localScale = new Vector3(orgScale[0] * _size, orgScale[1] * _size, orgScale[2]); //スケール
        AudioSource audio = ins.GetComponent<AudioSource>();
        audio.volume = Mathf.Clamp(_size, 0f, 1f); //音。仕組みが謎。ナニコレ。

        // パーティクル
        GameObject insPar = Instantiate(_objParticle, _pos, transform.rotation);
        ParticleSystem insPS = insPar.GetComponent<ParticleSystem>();
        var insPSmain = insPS.main;
        // 各種設定
        // ライフタイム
        insPSmain.startLifetime = new ParticleSystem.MinMaxCurve(_size * _mainLifeMin, _size * _mainLifeMax);
        // スピード
        insPSmain.startSpeed = new ParticleSystem.MinMaxCurve(_size * _mainSpeedMin, _size * _mainSpeedMax);
        // サイズ
        insPSmain.startSize = new ParticleSystem.MinMaxCurve(_size * _mainSizeMin, _size * _mainSizeMax);
        // バースト
        short burstMinCount = (short)(_size * _burstMinCount);
        short burstMaxCount = (short)(_size * _burstMaxCount);
        var newBurst = new ParticleSystem.Burst(0f, burstMinCount, burstMaxCount);
        //現在のバースト設定をクリア
        insPS.emission.SetBursts(new ParticleSystem.Burst[0]);
        //新しいバースト設定を追加
        insPS.emission.SetBursts(new ParticleSystem.Burst[] { newBurst });
    }
}