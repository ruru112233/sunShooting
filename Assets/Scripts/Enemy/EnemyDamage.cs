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

    Player player;
    BoxCollider boxCol;

    // Start is called before the first frame update
    void Start()
    {
        enemy_01 = this.enemy.GetComponent<Enemy_01>();
        delEffect.SetActive(false);
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        boxCol = transform.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            enemy_01.EnemyHp -= player.At;

            if (enemy_01.EnemyHp <= 0)
            {
                EnemyLost();
            }
        }
    }

    // ���Ă������̏���
    public async void EnemyLost()
    {
        Debug.Log("����");
        boxCol.enabled = false;

        if (enemy_01.EnemyAt == 5) player.Point += 10;
        player.GekituiCount++;

        delEffect.SetActive(true);
        await Task.Delay(1500);
        ItemDrop();
        boxCol.enabled = true;
        delEffect.SetActive(false);
        enemy.SetActive(false);
    }

    // �A�C�e���h���b�v�̒��I
    void ItemDrop()
    {
        int itemDrop = Random.Range(0, 20);
        Debug.Log(itemDrop);
        if (itemDrop >= 12)
        {
            Debug.Log("�A�C�e���h���b�v");
            Instantiate(SelectItem(), new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }

    // ���Ƃ��A�C�e���̑I��
    GameObject SelectItem()
    {
        int randItemNo = Random.Range(0, itemList.itemList.Count);

        return itemList.itemList[randItemNo];
    }
}
