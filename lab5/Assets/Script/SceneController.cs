using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    public int round;
    public int score;
    public int blood;
    public bool isStart;
    public bool isFailed;
    public int currentDifficulty;

    GameObject fatory;

    public void Awake(){
        Director director = Director.GetInstance();
        director.CurrentSceneController = this;
        director.CurrentSceneController.Initialize();
        fatory = new GameObject("UFO fatory");
        fatory.AddComponent<UFOFatory>();
        fatory.GetComponent<UFOFatory>().SetDepend(this);
    }

    //对场景参数进行初始化
    public void Initialize(){
        round = 1;
        score = 0;
        blood = 10;
        currentDifficulty = 0;
        isStart = false;
        isFailed = false;
    }

    //点击飞碟时触发加分
    public void AddScore(){
        score += 1;
    }

    //掉落一个飞碟时触发减分
    public void SubBlood(){
        if(blood > 0){
            blood -= 1;
        }
        if(blood == 0){
            isFailed = true;
            fatory.GetComponent<UFOFatory>().InitializeUFO();
            isStart = false;
        }
    }

    //开始一次新游戏
    public void StartNewGame(){
        Initialize();
        isStart = true;
        StartRound();
    } 

    //开始一轮抛掷飞碟（10个）
    public void StartRound(){
        fatory.GetComponent<UFOFatory>().SetDifficulty(currentDifficulty);
        fatory.GetComponent<UFOFatory>().InitializeUFO();
        fatory.GetComponent<UFOFatory>().StartRound();
    }

    //当一轮游戏结束，通知开始下一轮游戏
    public void RoundDone(){
        round++;
        currentDifficulty++;
        StartRound();
    }
}
