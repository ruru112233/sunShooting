using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player = null;

    [SerializeField]
    private GameObject enemyBulletPool = null;

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
        TargetPlayr();
        EnemyMove();

    }

    // プレイヤーの方向を向く
    void TargetPlayr()
    {
        float speed = 0.1f;

        Vector3 relativePos = player.transform.position - this.transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos);

        transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, speed);

    }

    void EnemyMove()
    {
        // プレイヤーとの間隔を測定
        float distance = Vector3.Distance(player.transform.position, this.transform.position);

        float speed = 1.5f;

        Debug.Log(distance);

        if (distance >= 18f)
        {
            rb.AddForce(transform.forward * speed);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

    }
}
