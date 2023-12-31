using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private float checkDelayCount = 3000000.0f;
    private bool checkDelay;

    [Header("몬스터 생성")]
    [SerializeField] private bool createMonster;
    [SerializeField, Tooltip("최대 스폰 갯수")] private int maxRespawnCount;
    private int curRespawnCount;//현재 리스폰 갯수
    [SerializeField] private bool RabbitRespawn;
    [SerializeField] private bool CrazyRabbitRespawn;
    [SerializeField] private List<GameObject> listEnemys;//인스펙터에 프리팹 넣기
    [SerializeField] Transform layerEnemy;
    [SerializeField] Vector3 trsRespawnPos;//리스폰 위치
    private int createCount;
    private float timer = 0.0f;
    private int monsterNumber;

    public static GameManager Instance;//싱글톤
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {

    }

    void Update()
    {
        checkRespawnTime();
        selectMonster();
    }
    private void selectMonster()
    {
        if (RabbitRespawn == true) { monsterNumber = 0; }
        if (CrazyRabbitRespawn == true) { monsterNumber = 1; }
    }

    private void randomRespawn()
    {

    }
    private void checkRespawnTime()
    {
        //if (Enemy.Instance.GetcurRespawnCount() < Enemy.Instance.GetmaxRespawnCount())
        timer += Time.deltaTime;
        //if (timer >= Enemy.Instance.GetRespawnTime())
        if (timer >= 10)
        {
            checkCurRespawnCount();
            enemyRespawn();
            timer = 0.0f;
        }
    }

    private void checkCurRespawnCount()
    {
        if (layerEnemy.childCount != maxRespawnCount)
        {
            curRespawnCount = layerEnemy.childCount;
        }
    }
    private void enemyRespawn()
    {
        if (createMonster == true)
        {
            for (int iNum = 0; iNum < maxRespawnCount - curRespawnCount; iNum++)
            {
                GameObject objEnemy = listEnemys[monsterNumber];
                GameObject obj = Instantiate(objEnemy, trsRespawnPos, Quaternion.identity, layerEnemy);
                Debug.Log(curRespawnCount);
            }
        }
        if (layerEnemy.childCount == maxRespawnCount)
        {
            createMonster = false;
        }
    }




    /// <summary>
    /// Player, Enemy의 모든 행동 후 딜레이를 관리하는 기능
    /// </summary>
    /// <param name="_value"></param>
    private void checkAllDelay(float _value)
    {
        if (checkDelayCount != 3000000.0f && checkDelay == false) { return; }

        checkDelayCount = checkDelayCount - _value;
        if (checkDelay == true && _value != 0)
        {
            checkDelayCount += Time.deltaTime;
        }
        if (checkDelayCount < 3000000.0f)
        {
            checkDelayCount = 3000000.0f;
            checkDelay = false;
        }
    }

}
