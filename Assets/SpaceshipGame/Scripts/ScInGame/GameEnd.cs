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

    /// <summary>GameEndを開始をする</summary>
    /// <param name="ClearOrOver">GameClear もしくは GameOver</param>
    public void MyStart(IngameManager.GameState ClearOrOver)
    {
        GameObject argTitle = null;

        // GameStateを設定
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

        // Updateを開始する
        if (argTitle != null)
        {
            StartCoroutine(MyUpdate(argTitle));
        }
        else
        {
            Debug.Log($"引数が用意できなかったため、GameEnd.Update()は実行出来ませんでした。", gameObject);
        }
    }

    IEnumerator MyUpdate(GameObject title)
    {
        //objGameEndをOn
        _objGameEnd.SetActive(true);

        // Titleをだんだん表示
        yield return StartCoroutine(TitleColor(title));// コルーチンを開始し、その完了を待機

        // Titleを上部に移動
        yield return StartCoroutine(TitleMove(title));// コルーチンを開始し、その完了を待機

        // Resultを表示
        GameObject result = _objGameEnd.transform.Find("Result").gameObject;
        result.SetActive(true); //表示する

    }

    IEnumerator TitleColor(GameObject title)
    {
        // だんだん表示
        // 変数
        float alphaPace = 0.01f; //このペースで色を表す。
        Image titleImage = title.GetComponent<Image>(); //使うやつ
        float titleBaceAlpha = titleImage.color[3];
        GameObject back = _objGameEnd.transform.Find("background").gameObject;
        Image backImage = back.GetComponent<Image>();
        float backBaceAlpha = backImage.color[3];

        title.SetActive(true); //表示する
        titleImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f); //透明にする
        backImage.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        // だんだんAlpha値を上げる
        float alpha = 0.0f;
        while (alpha < 1.0f)
        {
            alpha += alphaPace;
            titleImage.color = new Color(1.0f, 1.0f, 1.0f, alpha * titleBaceAlpha);
            backImage.color = new Color(0.0f, 0.0f, 0.0f, alpha * backBaceAlpha);
            yield return new WaitForSeconds(0.01f);// 待つ
        }
    }

    IEnumerator TitleMove(GameObject title)
    {
        // titleを上方向へ移動
        Vector3 start = title.transform.localPosition;//開始地点;
        Vector3 target = start + (Vector3.up * 180f);//目標地点

        float duration = 0.5f; //何秒で着くか

        float timer = 0;

        while (title.transform.localPosition != target) //到着するまでループ
        {
            // 目標地点更新
            timer += Time.deltaTime;
            title.transform.localPosition = Vector3.Lerp(start, target, timer / duration);
            yield return null;// 待つ
        }

        // 最終的にはちゃんと目的地に着かせる。
        title.transform.localPosition = target;
    }
}
