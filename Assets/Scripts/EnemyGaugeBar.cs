using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGaugeBar : MonoBehaviour
{
    private Transform trsEnemy;

    private void Awake()
    {

    }
    private void Update()
    {
        checkEnemyPos();
    }
    void Start()
    {
        GameManager manager = GameManager.Instance;
       // GameObject obj = manager.GetEnemyGameObject();//플레이어
        //Player objSc = obj.GetComponent<Player>();
        //trsEnemy = obj.transform;
    }
    private void checkEnemyPos()
    {
        if (trsEnemy == null) { return; }
        //transform.position = trsEnemy.position;
    }
}
