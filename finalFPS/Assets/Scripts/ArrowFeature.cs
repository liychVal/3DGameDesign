using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFeature : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 startDir;
    public Transform target;//collider transform
    public float speed;
    public float destoryTime;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        destoryTime = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        destoryTime -= Time.deltaTime;
        if (destoryTime < 0)
        {
            Destroy(this.transform.gameObject);
        }
    }
    void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("target")) // 确认击中靶子
    {
        // 锁定箭矢
        if (!rb.isKinematic)
        {
            rb.isKinematic = true;
            target = collision.gameObject.transform;
            transform.SetParent(target); // 将箭矢附加到靶子上
        }
        destoryTime = 5f;

        // 播放靶子倒下动画
        Animator targetAnimator = collision.gameObject.GetComponent<Animator>();
        if (targetAnimator != null)
        {
            targetAnimator.SetTrigger("isHit"); // Trigger转换条件
        }
    }
}

}
