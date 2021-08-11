using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGeneretor : MonoBehaviour
{
    [SerializeField]
    private GameObject playerBulletPool = null;

    [SerializeField]
    private ShotPosition shotPosition = null;

    Rigidbody rb;

    public float speed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            if (Time.frameCount % 5 == 0)
            {
                RightBullet();
                LeftBullet();
            }
        }
    }

    // âEë§ÇÃíeÇÃê∂ê¨
    void RightBullet()
    {
        if (!GameManager.instance.gameOverFlag)
        {
            foreach (Transform t in playerBulletPool.transform)
            {
                if (!t.gameObject.activeSelf)
                {
                    t.SetPositionAndRotation(RightPos(), Quaternion.identity);
                    t.gameObject.SetActive(true);
                    rb = t.GetComponent<Rigidbody>();
                    rb.velocity = Vector3.zero;
                    rb.AddForce(transform.right * speed, ForceMode.Impulse);
                    return;
                }
            }

            GameObject obj = playerBulletPool.transform.GetChild(0).gameObject;
            rb = Instantiate(obj, RightPos(), Quaternion.identity, playerBulletPool.transform).GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.right * speed, ForceMode.Impulse);
        }
    }

    // ç∂ë§ÇÃíeÇÃê∂ê¨
    void LeftBullet()
    {
        if (!GameManager.instance.gameOverFlag)
        {
            foreach (Transform t in playerBulletPool.transform)
            {
                if (!t.gameObject.activeSelf)
                {
                    t.SetPositionAndRotation(LeftPos(), Quaternion.identity);
                    t.gameObject.SetActive(true);
                    rb = t.GetComponent<Rigidbody>();
                    rb.velocity = Vector3.zero;
                    rb.AddForce(transform.right * speed, ForceMode.Impulse);
                    return;
                }
            }

            GameObject obj = playerBulletPool.transform.GetChild(0).gameObject;
            rb = Instantiate(obj, LeftPos(), Quaternion.identity, playerBulletPool.transform).GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.right * speed, ForceMode.Impulse);
        }
    }

    Vector3 RightPos()
    {
        Vector3 pos = new Vector3(shotPosition.pos2.transform.position.x, shotPosition.pos2.transform.position.y, shotPosition.pos2.transform.position.z);

        return pos;

    }

    Vector3 LeftPos()
    {
        Vector3 pos = new Vector3(shotPosition.pos1.transform.position.x, shotPosition.pos1.transform.position.y, shotPosition.pos1.transform.position.z);

        return pos;

    }

}
