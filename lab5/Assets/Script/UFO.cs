using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO
{
    int id;
    public bool isOver;
    public GameObject ufoObject;
    private UFOFatory myFatory;

    public UFO(UFOFatory fatory, int id){

        this.myFatory = fatory;
        this.id = id;
        this.isOver = false;

        ufoObject = GameObject.Instantiate(Resources.Load("Prefabs/UFO", typeof(GameObject))) as GameObject;
        ufoObject.AddComponent<Click>();
        ufoObject.GetComponent<Click>().SetDepend(this);
        ufoObject.AddComponent<MoveOut>();
        ufoObject.GetComponent<MoveOut>().SetDepend(this);
    }


    //设置飞碟对象是否可见
    public void SetUFOActive(bool boolean){
        ufoObject.SetActive(boolean);
    }

    //处理飞碟点击事件
    public void Click(){
        this.isOver = true;
        myFatory.ClickUFO(this.id);
    }

    //该飞碟已经飞出屏幕时触发
    public void Fail(){
        this.isOver = true;
        myFatory.FailUFO(this.id);
    }

    //飞碟开始移动
    public void Fly(Color c, float scale, Vector3 pos, Vector3 force){
        ufoObject.SetActive(true);
        ufoObject.GetComponent<Renderer>().material.color = c;
        ufoObject.transform.localScale = ufoObject.transform.localScale * scale;
        ufoObject.transform.position = pos;
        if(!ufoObject.GetComponent<Rigidbody>()){
            ufoObject.AddComponent<Rigidbody>();
        }
        ufoObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }
}
