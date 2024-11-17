using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    UFO ufo;

    //设置上层对象
    public void SetDepend(UFO ufo) {
        this.ufo = ufo;
    }

    //设置点击事件
    void OnMouseDown() {
        this.ufo.Click();
    }
}