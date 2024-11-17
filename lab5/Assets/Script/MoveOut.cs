using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOut : MonoBehaviour
{
    UFO ufo;
    bool onScreen = true;
    Vector3 position;

    //设置上层对象
    public void SetDepend(UFO ufo){
        this.ufo = ufo;
    }

    //判断飞碟是否已经飞出屏幕外
    void Update(){
        if(!onScreen) return;
        this.position = ufo.ufoObject.transform.position;
        if(this.position.x < -15 || this.position.x > 15 || this.position.y < -8 || this.position.y > 8){
            onScreen = false;
            ufo.Fail();
        }
    }
}
