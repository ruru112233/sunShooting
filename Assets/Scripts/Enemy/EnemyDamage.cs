using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy = null
                     , delEffect = null;

    [SerializeField]
    private ItemList itemList = null;

    Enemy_01 enemy_01;

    // Start is called before the first frame update
    void Start()
    {
        enemy_01 = this.enemy.GetComponent<Enemy_01>();
        delEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
            enemy_01.EnemyHp -= player.At;

            if (enemy_01.EnemyHp <= 0)
            {
                EnemyLost();
            }
        }
    }

    // 撃墜した時の処理
    public async void EnemyLost()
    {
        Debug.Log("撃墜");
        transform.GetComponent<BoxCollider>().enabled = false;
        delEffect.SetActive(true);
        await Task.Delay(1500);
        ItemDrop();
        transform.GetComponent<BoxCollider>().enabled = true;
        enemy.SetActive(false);
    }

    // アイテムドロップの抽選
    void ItemDrop()
    {
        int itemDrop = Random.Range(0, 20);
        Debug.Log(itemDrop);
        if (itemDrop >= 12)
        {
            Debug.Log("アイテムドロップ");
            Instantiate(SelectItem(), new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }

    // 落とすアイテムの選定
    GameObject SelectItem()
    {
        int randItemNo = Random.Range(0, itemList.itemList.Count);

        return itemList.itemList[randItemNo];
    }
}
