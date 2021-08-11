using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player = null;

    [SerializeField]
    private GameObject enemyBulletPool = null;

    [SerializeField]
    private Transform shotPos = null;

    private float speed = 100f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーとの間隔を測定
        float distance = Vector3.Distance(player.transform.position, this.transform.position);

        TargetPlayr();
        EnemyMove(distance);
        Shoot(distance);
        MeteoAvoidance();

    }

    // プレイヤーの方向を向く
    void TargetPlayr()
    {
        float speed = 0.1f;

        Vector3 relativePos = player.transform.position - this.transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos);

        transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, speed);

    }

    void EnemyMove(float distance)
    {
        float speed = 1.5f;

        if (distance >= 18f)
        {
            rb.AddForce(transform.forward * speed);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

    }

    // 攻撃
    void Shoot(float distance)
    {
        if (distance < 50f)
        {
            if (Time.frameCount % 50 == 0)
            {
                if (!GameManager.instance.gameOverFlag)
                {
                    Rigidbody bulletRb = new Rigidbody();
                    foreach (Transform t in enemyBulletPool.transform)
                    {
                        if (!t.gameObject.activeSelf)
                        {
                            t.SetPositionAndRotation(ShotPos(), Quaternion.identity);
                            t.gameObject.SetActive(true);
                            bulletRb = t.GetComponent<Rigidbody>();
                            bulletRb.velocity = Vector3.zero;
                            bulletRb.AddForce(transform.forward * speed, ForceMode.Impulse);
                            return;
                        }
                    }

                    GameObject obj = enemyBulletPool.transform.GetChild(0).gameObject;
                    bulletRb = Instantiate(obj, ShotPos(), Quaternion.identity, enemyBulletPool.transform).GetComponent<Rigidbody>();
                    bulletRb.velocity = Vector3.zero;
                    bulletRb.AddForce(transform.right * speed, ForceMode.Impulse);
                }
            }
        }

    }

    // 隕石の回避
    void MeteoAvoidance()
    {
        float distance = Vector3.Distance(SearchScript.FindMeteo(this.transform).transform.position, this.transform.position);

        Debug.Log(distance);

        //if (distance < 100.0f)
        //{
        //    Rigidbody avoidanceRb = GetComponent<Rigidbody>();


        //    avoidanceRb.AddForce(transform.up * -1f, ForceMode.Force);
        //    if (this.transform.position.y < player.transform.position.y)
        //    {
        //        Debug.Log("上に移動");
        //        //avoidanceRb.AddForce(transform.up * speed, ForceMode.Force);
        //    }
        //    else
        //    {
        //        Debug.Log("下に移動");
        //        //avoidanceRb.AddForce(transform.up * -speed, ForceMode.Force);
        //    }
        //}

    }

    Vector3 ShotPos()
    {
        Vector3 pos = new Vector3(shotPos.position.x, shotPos.position.y, shotPos.transform.position.z);

        return pos;

    }
}
