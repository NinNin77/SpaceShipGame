using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
    [SerializeField] GameObject _objGameEnd;

    IngameManager _ingameManager;

    private void Start()
    {
        _ingameManager = GetComponent<IngameManager>();
    }

    /// <summary>GameEnd���J�n������</summary>
    /// <param name="ClearOrOver">GameClear �������� GameOver</param>
    public void MyStart(IngameManager.GameState ClearOrOver)
    {
        GameObject argTitle = null;

        // GameState��ݒ�
        _ingameManager._gameState = ClearOrOver;

        // GameOver or GameClear
        if (_ingameManager._gameState == IngameManager.GameState.GameClear)
        {
            argTitle = _objGameEnd.transform.Find("GameClear").gameObject;
        }
        if (_ingameManager._gameState == IngameManager.GameState.GameOver)
        {
            argTitle = _objGameEnd.transform.Find("GameOver").gameObject;
        }

        // Update���J�n����
        if (argTitle != null)
        {
            StartCoroutine(MyUpdate(argTitle));
        }
        else
        {
            Debug.Log($"�������p�ӂł��Ȃ��������߁AGameEnd.Update()�͎��s�o���܂���ł����B", gameObject);
        }
    }

    IEnumerator MyUpdate(GameObject title)
    {
        //objGameEnd��On
        _objGameEnd.SetActive(true);

        // Title�����񂾂�\��
        yield return StartCoroutine(TitleColor(title));// �R���[�`�����J�n���A���̊�����ҋ@

        // Title���㕔�Ɉړ�
        yield return StartCoroutine(TitleMove(title));// �R���[�`�����J�n���A���̊�����ҋ@

        // Result��\��
        GameObject result = _objGameEnd.transform.Find("Result").gameObject;
        result.SetActive(true); //�\������

    }

    IEnumerator TitleColor(GameObject title)
    {
        // ���񂾂�\��
        // �ϐ�
        float alphaPace = 0.01f; //���̃y�[�X�ŐF��\���B
        Image titleImage = title.GetComponent<Image>(); //�g�����
        float titleBaceAlpha = titleImage.color[3];
        GameObject back = _objGameEnd.transform.Find("background").gameObject;
        Image backImage = back.GetComponent<Image>();
        float backBaceAlpha = backImage.color[3];

        title.SetActive(true); //�\������
        titleImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f); //�����ɂ���
        backImage.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        // ���񂾂�Alpha�l���グ��
        float alpha = 0.0f;
        while (alpha < 1.0f)
        {
            alpha += alphaPace;
            titleImage.color = new Color(1.0f, 1.0f, 1.0f, alpha * titleBaceAlpha);
            backImage.color = new Color(0.0f, 0.0f, 0.0f, alpha * backBaceAlpha);
            yield return new WaitForSeconds(0.01f);// �҂�
        }
    }

    IEnumerator TitleMove(GameObject title)
    {
        // title��������ֈړ�
        Vector3 start = title.transform.localPosition;//�J�n�n�_;
        Vector3 target = start + (Vector3.up * 180f);//�ڕW�n�_

        float duration = 0.5f; //���b�Œ�����

        float timer = 0;

        while (title.transform.localPosition != target) //��������܂Ń��[�v
        {
            // �ڕW�n�_�X�V
            timer += Time.deltaTime;
            title.transform.localPosition = Vector3.Lerp(start, target, timer / duration);
            yield return null;// �҂�
        }

        // �ŏI�I�ɂ͂����ƖړI�n�ɒ�������B
        title.transform.localPosition = target;
    }
}
