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

    // 隐藏按钮
    Color buttonColor = CurrentSceneController.isStart ? new Color(1, 1, 1, 0) : Color.white;
    
    GUI.color = buttonColor; 
    if(GUI.Button(new Rect(Screen.width*0.40f, Screen.height*0.40f, Screen.width*0.2f, Screen.height*0.1f), "Start")){
        CurrentSceneController.StartNewGame();
    }

    // 重置颜色
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
