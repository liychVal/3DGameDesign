using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{

    SceneController CurrentSceneController;
    GUIStyle msgStyle, titleStyle;
    string roundText, bloodText, scoreText;

    void Start(){
        CurrentSceneController = Director.GetInstance().CurrentSceneController;

        msgStyle = new GUIStyle();
        msgStyle.normal.textColor = Color.black;
        msgStyle.alignment = TextAnchor.MiddleCenter;
        msgStyle.fontSize = 30;

        titleStyle = new GUIStyle();
        titleStyle.normal.textColor = Color.black;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.fontSize = 60;
    }

    void OnGUI(){

    roundText = $"Round: {CurrentSceneController.round}";
    bloodText = $"Life: {CurrentSceneController.blood}";
    scoreText = $"Score: {CurrentSceneController.score}";

    // 如果游戏已经开始 (isStart 为 true)，则将按钮设为透明
    Color buttonColor = CurrentSceneController.isStart ? new Color(1, 1, 1, 0) : Color.white;
    
    // 设置按钮颜色，透明时按钮不可见
    GUI.color = buttonColor;  // 如果 isStart 为 true，则将按钮设为透明
    if(GUI.Button(new Rect(Screen.width*0.40f, Screen.height*0.40f, Screen.width*0.2f, Screen.height*0.1f), "Start")){
        CurrentSceneController.StartNewGame();
    }

    // 重置颜色，确保后面的控件不受影响
    GUI.color = Color.white;

    // 显示游戏状态
    GUI.Label(new Rect(0, Screen.height*0.85f, Screen.width*0.15f, Screen.height*0.1f), roundText, msgStyle);
    GUI.Label(new Rect(0, Screen.height*0.85f, Screen.width*0.45f, Screen.height*0.1f), bloodText, msgStyle);
    GUI.Label(new Rect(0, Screen.height*0.85f, Screen.width*0.75f, Screen.height*0.1f), scoreText, msgStyle);
    
    // 判断是否结束游戏
    if(CurrentSceneController.isFailed){
        GUI.Label(new Rect(0, Screen.height*0.4f, Screen.width, Screen.height*0.2f), "Game Over.", titleStyle);
    }
}

}
