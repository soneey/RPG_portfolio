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

    [Header("���� ����")]
    [SerializeField] private bool createMonster;
    [SerializeField, Tooltip("�ִ� ���� ����")] private int maxRespawnCount;
    private int curRespawnCount;//���� ������ ����
    [SerializeField] private bool RabbitRespawn;
    [SerializeField] private bool CrazyRabbitRespawn;
    [SerializeField] private List<GameObject> listEnemys;//�ν����Ϳ� ������ �ֱ�
    [SerializeField] Transform layerEnemy;
    Vector3 trsRespawnPos;//���� ������ ��ġ
    private float timer = 0.0f;
    private int monsterNumber;
    [SerializeField] GameObject GaugeBar;
    [SerializeField] GameObject MpGaugeBar;
    public static GameManager Instance;//�̱���
    int no0MonsterKillCount;
    int no1MonsterKillCount;
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
        no0MonsterKillCount = 0;
        no1MonsterKillCount = 0;
    }
    void Start()
    {

    }
    void Update()
    {
        checkRespawnTime();
        selectMonster();
        nextStage();
    }
    public void killPlus(int _value, int _value2)
    {
        if (_value == 0)
        {
            no0MonsterKillCount += _value2;
            Debug.Log($"killCount = {no0MonsterKillCount}");
        }
        if (_value == 1)
        {
            no1MonsterKillCount += _value2;
            Debug.Log($"killCount = {no1MonsterKillCount}");
        }
    }
    private void nextStage()
    {
        if (no0MonsterKillCount == 5)
        {
            int count = layerEnemy.transform.childCount;
            for (int i = count; i > 0; i--)
            {
                Destroy(layerEnemy.transform.GetChild(i - 1).gameObject);
            }
            CrazyRabbitRespawn = true;
            createMonster = true;
            no0MonsterKillCount = 0;
        }
    }
    private void selectMonster()
    {
        if (RabbitRespawn == true)
        {
            monsterNumber = 0;
            maxRespawnCount = 10;
        }
        if (CrazyRabbitRespawn == true)
        {
            monsterNumber = 1;
            maxRespawnCount = 2;
        }
    }

    private void checkRespawnTime()
    {
        //if (Enemy.Instance.GetcurRespawnCount() < Enemy.Instance.GetmaxRespawnCount())
        timer += Time.deltaTime;
        //if (timer >= Enemy.Instance.GetRespawnTime())
        if (timer >= 5)
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
        if (createMonster == true)
        {
            int count = maxRespawnCount - curRespawnCount;
            for (int iNum = 0; iNum < count; iNum++)
            {
                int posX = UnityEngine.Random.Range(-5, 6);
                int posY = UnityEngine.Random.Range(-5, 6);
                trsRespawnPos.x = posX;
                trsRespawnPos.y = posY;
                GameObject obj = Instantiate(listEnemys[monsterNumber], new Vector3(transform.position.x + posX, transform.position.y + posY, 0), Quaternion.identity, layerEnemy);
                Enemy objSc = obj.GetComponent<Enemy>();
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
    public GameObject GetMpGaugeBar()
    {
        return MpGaugeBar;
    }

}
