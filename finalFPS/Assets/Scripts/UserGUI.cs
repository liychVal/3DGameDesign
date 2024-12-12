using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public enum GameStatus
{
    Playing,
    GameOver,
}

struct Status
{
    public int score;
    public string tip;
    public float tipShowTime;
    public bool atSpot;
    public int shootNum;
    public GameStatus gameStatus;
    public int spotScore;
}

public class UserGUI : MonoBehaviour
{
    private ISceneController currentSceneController;
    private GUIStyle playInfoStyle;
    private Status status;

    public int crosshairSize = 20;//准星线条长度
    public float zoomSpeed = 5f;//滚轮切换视野大小的速度
    public float minFOV = 20f;//最小视野宽
    public float maxFOV = 60f;//最大视野宽
	

    // Start is called before the first frame update
    void Start()
    {
        Init();
        currentSceneController = Director.GetInstance().CurrentSceneController;

        // set style
        playInfoStyle = new GUIStyle();
        playInfoStyle.normal.textColor = Color.black;
        playInfoStyle.fontSize= 25;

        // init status
        status.shootNum = 0;
        status.score = 0;
        status.tip = string.Empty;
        status.atSpot = false;
        status.gameStatus = GameStatus.Playing;
        status.spotScore = 0;
        status.tipShowTime = 0;
    }

    private void Init()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        status.tipShowTime -= Time.deltaTime;
    }

    private void OnGUI()
    {
        // show user page
        ShowPage();
    }

    public void SetGameState(GameStatus gameStatus)
    {
        status.gameStatus = gameStatus;
    }

    /*
     set property of status
     */

    public void SetScore(int score)
    {
        status.score = score;
    }

    public void SetSpotScore(int score)
    {
        status.spotScore = score;
    }

    public void AddScore(int score)
    {
        status.score+=score;
    }

    public void SetShootNum(int shootNum)
    {
        status.shootNum = shootNum;
    }
    
    public void SetIsAtSpot(bool isAtSpot)
    {
        status.atSpot = isAtSpot;
    }

    private void ShowPage()
    {
        switch (status.gameStatus)
        {
            case GameStatus.Playing:
                ShowPlayingPage();
                break;
            case GameStatus.GameOver:
                ShowGameoverPage();
                break;
        }
    }

    private void ShowGameoverPage()
    {
 
    }

    private void ShowPlayingPage()
{
    // 总分数显示在屏幕正上方中间
    GUI.Label(new Rect(Screen.width / 2 - 100, 10, 200, 100), "Total Score：" + status.score, playInfoStyle);

    // 剩余射击次数和当前分数显示在右上角
    if (status.atSpot)
    {
        GUI.Label(new Rect(Screen.width - 200, 50, 190, 100), "Shoot Count：" + status.shootNum, playInfoStyle);
        GUI.Label(new Rect(Screen.width - 200, 90, 190, 100), "Current Score：" + status.spotScore, playInfoStyle);
    }

    // 显示准星
    float screenWidth = Screen.width;
    float screenHeight = Screen.height;
    float centerX = screenWidth / 2f;
    float centerY = screenHeight / 2f;

    // 设置准星颜色
    GUI.color = Color.red;

    // 绘制准星
    GUI.DrawTexture(new Rect(centerX - crosshairSize / 2f + 15f, centerY + 5f, crosshairSize, 2f), Texture2D.whiteTexture);
    GUI.DrawTexture(new Rect(centerX - 1f + 15f, centerY - crosshairSize / 2f + 5f, 2f, crosshairSize), Texture2D.whiteTexture);

    // 恢复GUI颜色设置
    GUI.color = Color.white;
}



}


