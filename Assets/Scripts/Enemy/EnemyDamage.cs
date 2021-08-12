using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy = null;

    [SerializeField]
    private ItemList itemList = null;

    Enemy_01 enemy_01;

    // Start is called before the first frame update
    void Start()
    {
        enemy_01 = this.enemy.GetComponent<Enemy_01>();
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

    // Œ‚’Ä‚µ‚½Žž‚Ìˆ—
    public async void EnemyLost()
    {
        Debug.Log("Œ‚’Ä");
        

        await Task.Delay(1);
        enemy.SetActive(false);
    }
}
