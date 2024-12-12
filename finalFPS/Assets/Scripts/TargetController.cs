using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public int basepoint;//初始分数

    public float aniSpeed = 1f;//动画执行速度
    public int scores;//单个靶点的分数
    // Use this for initialization
    void Start()
    {   
        this.tag = "target";
       
        //初始分数
        scores= 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //打到靶子
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Arrow")
        {
            
            scores += 1;
            //增加游戏总分数
            Singleton<UserGUI>.Instance.AddScore(1);
            
        }
    }

}
