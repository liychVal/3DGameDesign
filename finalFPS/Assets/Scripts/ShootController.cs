using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour
{
    float force; // 蓄力力量
    const float maxForce = 1f; // 最大力量
    const float chargeRate = 0.1f; // 每0.3秒蓄力的量
    Animator animator; // 弓动画控制
    float mouseDownTime; // 记录鼠标蓄力时间
    bool isCharging = false; // 是否正在蓄力

    bool isFired = true; // 是否已经将蓄力的箭发射
    public Slider Powerslider; // 蓄力条
    public SpotController currentSpotController;

    public bool readyToShoot = false; // 是否可以开始射击，通过检测是否进入射击位置来设置
    public int shootNum = 0; // 剩余设计次数

    public AudioClip shootSound; // 射箭音效
    private AudioSource audioSource; // 音频播放器

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("power", 1f);

        // 获取 AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource组件未找到");
        }
    }

    void Update()
    {
        if (!readyToShoot)
        {
            Powerslider.gameObject.SetActive(false);
            return;
        }

        if (Input.GetMouseButtonDown(0) && isFired) // 0表示鼠标左键
        {
            isFired = false;
            mouseDownTime = Time.time; // 记录鼠标按下的时间
            isCharging = true; // 开始蓄力
            Powerslider.gameObject.SetActive(true); // 显示蓄力条
            animator.SetTrigger("start");
        }

        if (isCharging)
        {
            float holdTime = Time.time - mouseDownTime; // 计算鼠标按下的时间
            force = Mathf.Min(holdTime / 0.3f * chargeRate, maxForce); // 计算蓄力的量，最大为0.5
            Powerslider.value = force / maxForce; // 更新力量条的值
            animator.SetFloat("power", force);
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetTrigger("hold");
            isCharging = false;
        }

        if (Input.GetMouseButtonDown(1) && readyToShoot)
        {
            isFired = true;
            animator.SetTrigger("shoot");
            animator.SetFloat("power", force); // 将蓄力的量加到animator的power属性上
            StartCoroutine(DelayedFireCoroutine(force)); // 延迟0.5s后射击
            Powerslider.value = 0; // 清零蓄力条
            animator.SetFloat("power", 0f);

            shootNum--;
            currentSpotController.shootNum--;
            Singleton<UserGUI>.Instance.SetShootNum(shootNum);
            if (shootNum == 0)
            {
                readyToShoot = false;
            }

            // 播放射箭音效
            PlayShootSound();
        }
    }

    // 播放射箭音效
    private void PlayShootSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound); // 播放音效
        }
        else
        {
            Debug.LogWarning("未设置音效");
        }
    }

    IEnumerator DelayedFireCoroutine(float f)
    {
        yield return new WaitForSeconds(0.2f); // 等待0.2s后
        fire(f);
    }

    public void fire(float f)
    {
        GameObject arrow = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Arrow"));
        ArrowFeature arrowFeature = arrow.AddComponent<ArrowFeature>();
        Transform originArrowTransform = transform.Find("mark");

        arrow.transform.position = originArrowTransform.position;
        arrow.transform.rotation = transform.rotation;

        Rigidbody arrow_db = arrow.GetComponent<Rigidbody>();

        arrowFeature.startPos = arrowFeature.transform.position;
        arrow.tag = "Arrow";
        arrow_db.velocity = transform.forward * 100 * f;
    }
}
