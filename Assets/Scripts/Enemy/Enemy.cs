using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int hp = 0;
    private int at = 0;

    public int EnemyHp
    {
        get { return hp; }
        set { hp = value; }
    }

    public int EnemyAt
    {
        get { return at; }
        set { at = value; }
    }

    GameObject player = null;
    GameObject sunObj;


    [SerializeField]
    private GameObject enemyBulletPool = null;

    [SerializeField]
    private Transform shotPos = null;

    private float speed = 100f;

    private float stopTime = 0;

    Rigidbody rb;
    Rigidbody bulletRb;

    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
        sunObj = GameObject.FindWithTag("SunObj");

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // プレイヤーとの間隔を測定
        float distance = Vector3.SqrMagnitude(player.transform.position - this.transform.position);

        if (EnemyHp > 0 && !GameManager.instance.gameOverFlag)
        {
            TargetPlayr();
            EnemyMove(distance);
            Shoot(distance);
        }
        
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

        if (distance >= 25000)
        {
            transform.RotateAround(sunObj.transform.position, Vector3.up, speed * Time.deltaTime);
        }
        else if (distance >= 8000)
        {
            rb.AddForce(transform.forward * speed);

            stopTime += Time.deltaTime;

            if (stopTime >= 2.0f)
            {
                stopTime = 0;
                rb.velocity = Vector3.zero;
            }
        }
        else if (distance >= 1000f)
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
        if (distance < 18000f)
        {
            if (Time.frameCount % 60 == 0)
            {
                if (!GameManager.instance.gameOverFlag)
                {
                    foreach (Transform t in enemyBulletPool.transform)
                    {
                        if (!t.gameObject.activeSelf)
                        {
                            t.SetPositionAndRotation(ShotPos(), Quaternion.identity);
                            t.GetComponent<EnemyBulletController>().bulletAt = EnemyAt;
                            t.gameObject.SetActive(true);
                            bulletRb = t.GetComponent<Rigidbody>();
                            bulletRb.velocity = Vector3.zero;
                            bulletRb.AddForce(transform.forward * speed, ForceMode.Impulse);
                            return;
                        }
                    }

                    GameObject obj = enemyBulletPool.transform.GetChild(0).gameObject;
                    obj.GetComponent<EnemyBulletController>().bulletAt = EnemyAt;
                    bulletRb = Instantiate(obj, ShotPos(), Quaternion.identity, enemyBulletPool.transform).GetComponent<Rigidbody>();
                    bulletRb.velocity = Vector3.zero;
                    bulletRb.AddForce(transform.right * speed, ForceMode.Impulse);
                }
            }
        }

    }

    Vector3 ShotPos()
    {
        Vector3 pos = new Vector3(shotPos.position.x, shotPos.position.y, shotPos.transform.position.z);

        return pos;

    }
}
