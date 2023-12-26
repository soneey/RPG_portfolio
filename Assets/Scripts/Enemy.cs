using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum playerMotion
    {
        None,
        IdleLeft,
        IdleRight,
        IdleBack,
        IdleFront,
        LeftDirLeftFoot,
        LeftDirRightFoot,
        RightDirLeftFoot,
        RightDirRightFoot,
        BackDirLeftFoot,
        BackDirRightFoot,
        FrontDirLeftFoot,
        FrontDirRightFoot,
    }
    playerMotion curMotion = playerMotion.None;
    playerMotion beforeMotion = playerMotion.None;
    [SerializeField] private Sprite[] idle;
    [SerializeField] private bool footCheck;


    [Header("�÷��̾� �ൿ������")]
    [SerializeField] private float checkDelayCount = 100.0f;
    private bool checkDelay;

    [Header("��������Ʈ ������")]
    [SerializeField] private float spriteChangeDelay = 0.0f;
    private bool checkChangeSpriteDelay;

    SpriteRenderer sr;
    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {

    }

    void Update()
    {
        moving();
    }

    private void moving()
    {

    }
}
