using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Parm
{
    public int hp = 100;
    public float boost = 1.0f;
}

public class Player : MonoBehaviour
{

    private int dropCount = 1;

    [SerializeField]
    private GameObject particle = null;

    [SerializeField]
    private GameObject spaceShuttle = null;

    public int DropCount
    {
        get { return dropCount; }
    }

    [SerializeField]
    private float speed = 0;

    [SerializeField]
    private float angleSpeed = 0;

    private GameObject sunObj;

    [SerializeField]
    private float inField = 0
                , outField = 0;

    float moveZ = 0f;
    float overHeartTime = 0;
    bool overHeat = false;

    [SerializeField]
    private Slider boostSlider;

    Vector3 ptForward = new Vector3(0, 0, 0);
    Vector3 ptRight = new Vector3(-0.5f, 0, 0);
    Vector3 ptLeft = new Vector3(0.5f, 0, 0);

    float turningSpeed = 20f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        sunObj = GameObject.FindWithTag("SunObj");
        rb = GetComponent<Rigidbody>();
        particle.SetActive(false);

       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!GameManager.instance.gameOverFlag)
        {
            StartPosition();
            if (!overHeat)
            {
                if (Input.GetKeyDown(KeyCode.F) && Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.F) && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Z))
                {
                    AudioManager.instance.PlaySE(1);
                }
                else if (Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.Z))
                {
                    AudioManager.instance.StopSe();
                }

                Move();
                //TurningMove();
            }
            else
            {
                // �I�[�o�[�q�[�g
                OverHeart();
                if(overHeartTime <= 0.2f)
                  AudioManager.instance.StopSe();
            }
            MeteoPosCheck();
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.Space))
            {
                AudioManager.instance.StopSe();
            }
        }

        GameOver();
    }

    private void FixedUpdate()
    {

        rb.velocity = transform.right * moveZ;

        //WS�L�[�A�����L�[�ŏ㉺�̕�����ւ���
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * angleSpeed;
        //transform.Rotate(Vector3.right * -y);
        transform.Rotate(Vector3.forward * -y);
        //AD�L�[�A�����L�[�ŕ�����ւ���
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * angleSpeed;
        transform.Rotate(Vector3.up * x);
    }

    // ���z���痣�ꂷ������A�����ʒu�ɖ߂�
    void StartPosition()
    {
        // ���z�Ƃ̊Ԋu�𑪒�
        float distance = Vector3.Distance(sunObj.transform.position, this.transform.position);

        if (distance > outField)
        {
            this.transform.LookAt(sunObj.transform);
        }
    }

    // �ړ�
    void Move()
    {   
        if (boostSlider.value <= 0)
        {
            overHeat = true;
            return;
        }

        // space�L�[�őO�i
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.F) && boostSlider.value > 0)
        {
            boostSlider.value -= Time.deltaTime / 4f;
            moveZ = speed * 2 * Time.deltaTime;
            particle.GetComponent<ParticleSystemRenderer>().pivot = ptForward;
            particle.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            ForwardMove();

            if (Input.GetKey(KeyCode.C))
            {
                TurningRigheMove();
            }
            else if (Input.GetKey(KeyCode.Z))
            {
                TurningLeftMove();
            }
        }
        else if(Input.GetKey(KeyCode.C))
        {
            TurningRigheMove();
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            TurningLeftMove();
        }
        else
        {
            moveZ = 0;
            boostSlider.value += Time.deltaTime / 5;
            particle.SetActive(false);
        }
    }

    // �O�i
    void ForwardMove()
    {
        moveZ = speed * Time.deltaTime;
        boostSlider.value += Time.deltaTime / 5;
        particle.GetComponent<ParticleSystemRenderer>().pivot = ptForward;
        particle.SetActive(true);
    }
    

    // �E����
    void TurningRigheMove()
    {
        rb.AddForce(transform.forward * -turningSpeed, ForceMode.Impulse);
        boostSlider.value -= Time.deltaTime / 4f;
        particle.GetComponent<ParticleSystemRenderer>().pivot = ptRight;
        particle.SetActive(true);
    }

    // ������
    void TurningLeftMove()
    {
        rb.AddForce(transform.forward * turningSpeed, ForceMode.Impulse);
        boostSlider.value -= Time.deltaTime / 4f;
        particle.GetComponent<ParticleSystemRenderer>().pivot = ptLeft;
        particle.SetActive(true);
        
    }

    // �o�[�X�g��S�Ďg���؂�ƁA�ꎞ�I�ɓ����Ȃ��悤�ɂȂ�
    void OverHeart()
    {
        Debug.Log("�I�[�o�[�q�[�g");
        spaceShuttle.GetComponent<Renderer>().material.color = Color.red;
        transform.localScale = new Vector3(transform.localScale.y, transform.localScale.y, transform.localScale.z);
        moveZ = 0;
        overHeartTime += Time.deltaTime;
        particle.SetActive(false);

        if (overHeartTime >= 2.0)
        {
            overHeat = false;
            overHeartTime = 0;
            boostSlider.value = 0.01f;
            spaceShuttle.GetComponent<Renderer>().material.color = Color.white;
        }

    }

    // 覐΂ƃv���C���[�̈ʒu����A�v���C���[�̋߂���覐΂����邩����
    void MeteoPosCheck()
    {
        if (FindMeteo() != null)
        {
            float distance = Vector3.Distance(FindMeteo().transform.position, this.transform.position);

            if (distance < 150)
            {
                Debug.Log("覐ΐڋߒ�");
                GameManager.instance.emergencyPanel.SetActive(true);
            }
            else
            {
                GameManager.instance.emergencyPanel.SetActive(false);
            }
        }
    }

    // ��ԋ߂�覐΂��擾
    GameObject FindMeteo()
    {
        GameObject[] meteos = GameObject.FindGameObjectsWithTag("Meteorite");
        GameObject closest = null;

        float distance = Mathf.Infinity;

        Vector3 position = transform.position;

        foreach (GameObject meteo in meteos)
        {
            Vector3 diff = meteo.transform.position - position;

            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = meteo;
                distance = curDistance;
            }
        }

        return closest;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WaterDrop")
        {
            transform.localScale = new Vector3(transform.localScale.y, transform.localScale.y, transform.localScale.z);
            transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);
            dropCount++;
            speed += 1.0f;
            angleSpeed += 0.3f;
            AudioManager.instance.PlaySE(0);
        }

        
    }

    // �Q�[���I�[�o�[�̏���
    private async void GameOver()
    {
        var parm = new Parm();

        if (parm.hp <= 0)
        {
            Debug.Log("�Q�[���I�[�o�[");
            GameManager.instance.gameOverPanel.SetActive(true);

            AudioManager.instance.PlaySE(3);
            GameManager.instance.gameOverFlag = true;
            int score = CalcScript.ScoreCalc();
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);

            await Task.Delay(500);
        }

    }

}
