using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SearchService;

public class ObjPoolGen : MonoBehaviour
{
    [SerializeField]
    private GameObject smallMeteoPool = null
                     , nomalMeteoPool = null
                     , enemy_01Pool = null
        ;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 35; i++)
        {
            Invoke("SmallMeteo", 0f);
        }

        for (int i = 0; i < 25; i++)
        {
            Invoke("NomalMeteo", 0f);
        }

        for (int i = 0; i < 30; i++)
        {
            Invoke("Enmey01Instance", 0f);
        }

        InvokeRepeating("Enmey01Instance", 30.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ��覐΂𐶐�
    void SmallMeteo()
    {
        if (!GameManager.instance.gameOverFlag)
        {
            foreach (Transform t in smallMeteoPool.transform)
            {
                if (!t.gameObject.activeSelf)
                {
                    t.SetPositionAndRotation(InstancePoint(), Quaternion.identity);
                    t.gameObject.SetActive(true);
                    return;
                }
            }

            //GameObject obj = smallMeteoPool.transform.GetChild(0).gameObject;
            //Instantiate(obj, InstancePoint(), Quaternion.identity, smallMeteoPool.transform);
        }
    }

    // ��覐΂𐶐�
    void NomalMeteo()
    {
        if (!GameManager.instance.gameOverFlag)
        {
            foreach (Transform t in nomalMeteoPool.transform)
            {
                if (!t.gameObject.activeSelf)
                {
                    t.SetPositionAndRotation(InstancePoint(), Quaternion.identity);
                    t.gameObject.SetActive(true);
                    return;
                }
            }

            //GameObject obj = nomalMeteoPool.transform.GetChild(0).gameObject;
            //Instantiate(obj, InstancePoint(), Quaternion.identity, nomalMeteoPool.transform);
        }

    }

    // �G�@(01)�̐���
    void Enmey01Instance()
    {
        if (!GameManager.instance.gameOverFlag)
        {
            foreach (Transform t in enemy_01Pool.transform)
            {
                if (!t.gameObject.activeSelf)
                {
                    t.SetPositionAndRotation(InstancePoint(), Quaternion.identity);
                    t.GetComponent<Enemy_01>().EnemyHp = 10;
                    t.gameObject.SetActive(true);
                    return;
                }
            }

            //GameObject obj = enemy_01Pool.transform.GetChild(0).gameObject;
            //Instantiate(obj, InstancePoint(), Quaternion.identity, enemy_01Pool.transform);
        }
    }

    // �����ʒu�̎Z�o
    Vector3 InstancePoint()
    {
        Vector3 pos = new Vector3();

        // �����̐ݒ�
        float x = Random.Range(80.0f, 450.0f);
        float y = Random.Range(80.0f, 450.0f);
        float z = Random.Range(80.0f, 450.0f);

        bool checkX = false;
        bool checkY = false;
        bool checkZ = false;
        int xx = 0;
        int yy = 0;
        int zz = 0;
        // �����̐ݒ�
        while (!checkX)
        {
            xx = Random.Range(-1, 2);
            if (xx != 0) checkX = true;

        }
        while (!checkY)
        {
            yy = Random.Range(-1, 2);
            if (yy != 0) checkY = true;
        }
        while (!checkZ)
        {
            zz = Random.Range(-1, 2);
            if (zz != 0) checkZ = true;
        }

        // x���W�̐ݒ�
        x = x * xx;
        // y���W�̐ݒ�
        y = y * yy;
        // z���W�̐ݒ�
        z = z * zz;

        pos = new Vector3(x, y, z);

        return pos;
    }
}
