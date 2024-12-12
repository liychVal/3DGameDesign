using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ShootController CrossBow;// 弓对象
    private bool atSpot = false;// 是否到达射击点
    private SpotController spot;// 射击位controller
    private TargetController[] targetControllers;//target controller

    void Start()
    {
        CrossBow = GetComponentInChildren<ShootController>();

    }

    void Update()
    {
        // 进入射击位置
        if (atSpot)
        {   
            // 对应参数
            Singleton<UserGUI>.Instance.SetIsAtSpot(true);
            Singleton<UserGUI>.Instance.SetShootNum(spot.shootNum);
            if (targetControllers != null)
            {
                //获取这个射击位置对应靶子的分数信息
                int sumSpotScore = 0;
                foreach (TargetController targetController in targetControllers)
                {
                    sumSpotScore += targetController.scores;
                }
                Singleton<UserGUI>.Instance.SetSpotScore(sumSpotScore);
            }
        }
        else
        {
            Singleton<UserGUI>.Instance.SetIsAtSpot(false);
            Singleton<UserGUI>.Instance.SetShootNum(0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 与射击点位撞击

        if (collision.gameObject.tag == "spot")
        {
            
            spot = collision.gameObject.GetComponentInChildren<SpotController>();
            atSpot = true;

            if (spot.shootNum > 0)
            {
                CrossBow.GetComponentInChildren<ShootController>().readyToShoot = true;
                CrossBow.GetComponentInChildren<ShootController>().shootNum = spot.shootNum;
                CrossBow.GetComponentInChildren<ShootController>().currentSpotController = spot;
            }

            
            targetControllers = spot.targetControllers; 
            if (targetControllers != null)
            {
                int sumSpotScore = 0;
                foreach (TargetController targetController in targetControllers)
                {
                    sumSpotScore += targetController.scores;
                }
                Singleton<UserGUI>.Instance.SetSpotScore(sumSpotScore);
            }
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "spot")
        {
            Debug.Log("collideExit with spot");
            CrossBow.GetComponentInChildren<ShootController>().readyToShoot = false;
            atSpot = false;
        }
    }

}

