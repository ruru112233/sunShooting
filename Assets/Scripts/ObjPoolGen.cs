using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SearchService;

public class ObjPoolGen : MonoBehaviour
{
    [SerializeField]
    private GameObject smallMeteoPool = null
                     , nomalMeteoPool = null
                     , bigMeteoPool = null
                     , rightDropPool = null
                     , leftDropPool = null;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SmallMeteo", 0.0f, 5.0f);
        InvokeRepeating("NomalMeteo", 100.0f, 12.0f);
        InvokeRepeating("BigMeteo", 200.0f, 22.0f);
        InvokeRepeating("GetRightDrop", 0.0f, 1.0f);
        InvokeRepeating("GetLeftDrop", 0.0f, 1.0f);
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

    // ��覐΂𐶐�
    void BigMeteo()
    {
        if (!GameManager.instance.gameOverFlag)
        {
            foreach (Transform t in bigMeteoPool.transform)
            {
                if (!t.gameObject.activeSelf)
                {
                    t.SetPositionAndRotation(InstancePoint(), Quaternion.identity);
                    t.gameObject.SetActive(true);
                    return;
                }
            }

            //GameObject obj = bigMeteoPool.transform.GetChild(0).gameObject;
            //Instantiate(obj, InstancePoint(), Quaternion.identity, bigMeteoPool.transform);
        }


    }

    // �E��]�̐��H�𐶐�
    void GetRightDrop()
    {
        if (!GameManager.instance.gameOverFlag)
        {
            for (int i = 0; i < 3; i++)
            {
                foreach (Transform t in rightDropPool.transform)
                {
                    if (!t.gameObject.activeSelf)
                    {
                        t.SetPositionAndRotation(InstancePoint(), Quaternion.identity);
                        t.gameObject.SetActive(true);
                        return;
                    }
                }

                //GameObject obj = rightDropPool.transform.GetChild(0).gameObject;
                //Instantiate(obj, InstancePoint(), Quaternion.identity, rightDropPool.transform);
            }
        }

        
        
    }

    // ����]�̐��H�𐶐�
    void GetLeftDrop()
    {
        if (!GameManager.instance.gameOverFlag)
        {
            for (int i = 0; i < 3; i++)
            {
                foreach (Transform t in leftDropPool.transform)
                {
                    if (!t.gameObject.activeSelf)
                    {
                        t.SetPositionAndRotation(InstancePoint(), Quaternion.identity);
                        t.gameObject.SetActive(true);
                        return;
                    }
                }

                //GameObject obj = leftDropPool.transform.GetChild(0).gameObject;
                //Instantiate(obj, InstancePoint(), Quaternion.identity, leftDropPool.transform);
            }
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
