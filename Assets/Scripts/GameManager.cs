using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private float checkDelayCount = 3000000.0f;
    private bool checkDelay;
    [SerializeField] GameObject objPlayer;
    Transform Player;

    [Header("몬스터 생성")]
    [SerializeField] private bool createMonster;
    [SerializeField, Tooltip("최대 스폰 갯수")] private int maxRespawnCount;
    private int curRespawnCount;//현재 리스폰 갯수
    [SerializeField] private bool RabbitRespawn;
    [SerializeField] private bool CrazyRabbitRespawn;
    [SerializeField] private List<GameObject> listEnemys;//인스펙터에 프리팹 넣기
    [SerializeField] Transform layerEnemy;
    Vector3 trsRespawnPos;//몬스터 리스폰 위치
    private int createCount;
    private float timer = 0.0f;
    private int monsterNumber;
    private List<Transform> listRespawnPos;
    [SerializeField] GameObject GaugeBar;
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
    bool boolCreatRespawnPos;
    private void randomRespawnPos()
    {
        int posX = UnityEngine.Random.Range(-5, 6);
        int posY = UnityEngine.Random.Range(-5, 6);
        trsRespawnPos = new Vector3(posX, posY, 0);

    }
    private void checkRespawnTime()
    {
        //if (Enemy.Instance.GetcurRespawnCount() < Enemy.Instance.GetmaxRespawnCount())
        timer += Time.deltaTime;
        //if (timer >= Enemy.Instance.GetRespawnTime())
        if (timer >= 2)
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
            createMonster = true;

            curRespawnCount = layerEnemy.childCount;
        }
    }
    private void enemyRespawn()
    {
        Player = objPlayer.transform;
        Player playerSc = Player.GetComponent<Player>();
        if (createMonster == true)
        {
            int count = maxRespawnCount - curRespawnCount;
            for (int iNum = 0; iNum < count; iNum++)
            {
                int posX = UnityEngine.Random.Range(-5, 6);
                int posY = UnityEngine.Random.Range(-5, 6);
                trsRespawnPos = new Vector3(posX, posY, 0);
                if (trsRespawnPos == Player.transform.position)
                {
                    posX = UnityEngine.Random.Range(-5, 6);
                    posY = UnityEngine.Random.Range(-5, 6);
                    trsRespawnPos = new Vector3(posX, posY, 0);
                }
                else if (trsRespawnPos != transform.position)
                {
                    GameObject objEnemy = listEnemys[monsterNumber];
                    GameObject obj = Instantiate(objEnemy, trsRespawnPos, Quaternion.identity, layerEnemy);
                    //Enemy objSc = obj.GetComponent<Enemy>();
                }
            }
        }
        if (layerEnemy.childCount == maxRespawnCount)
        {
            createMonster = false;
        }
    }
    public int GetMaxRespawnCount()
    {
        return maxRespawnCount;
    }
    public int GetMonsterNumber()
    {
        return monsterNumber;
    }
    public GameObject GetGaugeBar()
    {
        return GaugeBar;
    }

}
