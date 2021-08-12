using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Parm
{
    public int hp = 100;
    public int at = 5;
    public float maxBoost = 1.0f;
    public float speed = 250f;
    
}

public class Player : MonoBehaviour
{
    Parm parm = new Parm();

    public int Hp
    {
        get { return parm.hp; }
        set { parm.hp = value; }
    }

    public int At
    {
        get { return parm.at; }
        set { parm.at = value; }
    }

    public float Speed
    {
        get { return parm.speed; }
        set { parm.speed = value; }
    }

    public float MaxBoost
    {
        get { return parm.maxBoost; }
        set { parm.maxBoost = value; }
    }

    private int dropCount = 1;

    [SerializeField]
    private GameObject particle = null;

    [SerializeField]
    private GameObject spaceShuttle = null;

    public int DropCount
    {
        get { return dropCount; }
    }

    //[SerializeField]
    //private float speed = 0;

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
    private Slider hpSlider = null
                 , boostSlider = null;

    Vector3 ptForward = new Vector3(0, 0, 0);
    Vector3 ptRight = new Vector3(-0.5f, 0, 0);
    Vector3 ptLeft = new Vector3(0.5f, 0, 0);
    Vector3 ptUp = new Vector3(0, -0.5f, 0);
    Vector3 ptDown = new Vector3(0, 0.5f, 0);

    float turningSpeed = 200f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        sunObj = GameObject.FindWithTag("SunObj");
        rb = GetComponent<Rigidbody>();
        particle.SetActive(false);

        // プレイヤーのHPをセット
        

       
    }

    // Update is called once per frame
    void Update()
    {

        if (!GameManager.instance.gameOverFlag)
        {
            ParmsSet();

            StartPosition();
            if (!overHeat)
            {
                if (Input.GetKeyDown(KeyCode.F) && Input.GetKey(KeyCode.Space) || 
                    Input.GetKey(KeyCode.F) && Input.GetKeyDown(KeyCode.Space) || 
                    Input.GetKeyDown(KeyCode.A) || 
                    Input.GetKeyDown(KeyCode.D) ||
                    Input.GetKeyDown(KeyCode.W) ||
                    Input.GetKeyDown(KeyCode.S))
                {
                    AudioManager.instance.PlaySE(1);
                }
                else if (Input.GetKeyUp(KeyCode.F) || 
                         Input.GetKeyUp(KeyCode.Space) || 
                         Input.GetKeyUp(KeyCode.A) || 
                         Input.GetKeyUp(KeyCode.D) ||
                         Input.GetKeyUp(KeyCode.W) ||
                         Input.GetKeyUp(KeyCode.S))
                {
                    AudioManager.instance.StopSe();
                }

                Move();
                //TurningMove();
            }
            else
            {
                // オーバーヒート
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

    // パラメータのセット
    void ParmsSet()
    {
        boostSlider.maxValue = MaxBoost;
    }

    private void FixedUpdate()
    {

        rb.velocity = transform.right * moveZ;

        //WSキー、↑↓キーで上下の方向を替える
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * angleSpeed;
        //transform.Rotate(Vector3.right * -y);
        transform.Rotate(Vector3.forward * -y);
        //ADキー、←→キーで方向を替える
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * angleSpeed;
        transform.Rotate(Vector3.up * x);
    }

    // 太陽から離れすぎたら、初期位置に戻す
    void StartPosition()
    {
        // 太陽との間隔を測定
        float distance = Vector3.Distance(sunObj.transform.position, this.transform.position);

        if (distance > outField)
        {
            this.transform.LookAt(sunObj.transform);
        }
    }

    // 移動
    void Move()
    {   
        if (boostSlider.value <= 0)
        {
            overHeat = true;
            return;
        }

        // spaceキーで前進
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.F) && boostSlider.value > 0)
        {
            boostSlider.value -= Time.deltaTime / 4f;
            moveZ = Speed * 2 * Time.deltaTime;
            particle.GetComponent<ParticleSystemRenderer>().pivot = ptForward;
            particle.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            ForwardMove();

            if (Input.GetKey(KeyCode.D))
            {
                TurningRigheMove();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                TurningLeftMove();
            }
            else if (Input.GetKey(KeyCode.W))
            {
                TurningUpMove();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                TurningDownMove();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            TurningRigheMove();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            TurningLeftMove();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            TurningUpMove();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            TurningDownMove();
        }
        else
        {
            moveZ = 0;
            boostSlider.value += Time.deltaTime / 5;
            particle.SetActive(false);
        }
    }

    // 前進
    void ForwardMove()
    {
        moveZ = Speed * Time.deltaTime;
        boostSlider.value += Time.deltaTime / 5;
        particle.GetComponent<ParticleSystemRenderer>().pivot = ptForward;
        particle.SetActive(true);
    }
    

    // 右旋回
    void TurningRigheMove()
    {
        rb.AddForce(transform.forward * -turningSpeed, ForceMode.Force);
        rb.AddForce(transform.right * turningSpeed * 3, ForceMode.Force);
        transform.Rotate(Vector3.up * 1);
        boostSlider.value -= Time.deltaTime / 4f;
        particle.GetComponent<ParticleSystemRenderer>().pivot = ptRight;
        particle.SetActive(true);
    }

    // 左旋回
    void TurningLeftMove()
    {
        rb.AddForce(transform.forward * turningSpeed, ForceMode.Force);
        rb.AddForce(transform.right * turningSpeed * 3, ForceMode.Force);
        transform.Rotate(Vector3.up * -1);
        boostSlider.value -= Time.deltaTime / 4f;
        particle.GetComponent<ParticleSystemRenderer>().pivot = ptLeft;
        particle.SetActive(true);
        
    }

    // 上旋回
    void TurningUpMove()
    {
        rb.AddForce(transform.up * turningSpeed, ForceMode.Force);
        rb.AddForce(transform.right * turningSpeed * 3, ForceMode.Force);
        transform.Rotate(Vector3.forward * 1);
        boostSlider.value -= Time.deltaTime / 4f;
        particle.GetComponent<ParticleSystemRenderer>().pivot = ptUp;
        particle.SetActive(true);
    }

    // 下旋回
    void TurningDownMove()
    {
        rb.AddForce(transform.up * -turningSpeed, ForceMode.Force);
        rb.AddForce(transform.right * turningSpeed * 3, ForceMode.Force);
        transform.Rotate(Vector3.forward * -1);
        boostSlider.value -= Time.deltaTime / 4f;
        particle.GetComponent<ParticleSystemRenderer>().pivot = ptDown;
        particle.SetActive(true);
    }

    // バーストを全て使い切ると、一時的に動けないようになる
    void OverHeart()
    {
        Debug.Log("オーバーヒート");
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

    // 隕石とプレイヤーの位置から、プレイヤーの近くに隕石があるか判定
    void MeteoPosCheck()
    {
        
        if (SearchScript.FindMeteo(this.transform) != null)
        {
            float distance = Vector3.Distance(SearchScript.FindMeteo(this.transform).transform.position, this.transform.position);

            if (distance < 150)
            {
                Debug.Log("隕石接近中");
                GameManager.instance.emergencyPanel.SetActive(true);
            }
            else
            {
                GameManager.instance.emergencyPanel.SetActive(false);
            }
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        
    }

    // ゲームオーバーの処理
    private async void GameOver()
    {
        var parm = new Parm();

        if (parm.hp <= 0)
        {
            Debug.Log("ゲームオーバー");
            GameManager.instance.gameOverPanel.SetActive(true);

            AudioManager.instance.PlaySE(3);
            GameManager.instance.gameOverFlag = true;
            int score = CalcScript.ScoreCalc();
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);

            await Task.Delay(500);
        }

    }

}
