using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;//�̱���
    [SerializeField] private float checkDelayCount = 100.0f;
    [SerializeField] GameObject objPlayer;
    private bool checkDelay;
    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// Player, Enemy�� ��� �ൿ �� �����̸� �����ϴ� ���
    /// </summary>
    /// <param name="_value"></param>
    private void checkAllDelay(float _value)
    {
        if (checkDelayCount != 100.0f && checkDelay == false) { return; }

        checkDelayCount = checkDelayCount - _value;
        if (checkDelay == true && _value != 0)
        {
            checkDelayCount += Time.deltaTime;
        }
        if (checkDelayCount < 100)
        {
            checkDelayCount = 100.0f;
            checkDelay = false;
        }
        //Debug.Log(moveDelayTimer);
    }
    public GameObject GetPlayerGameObject()
    {
        return objPlayer;
    }
}
