using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageBox : MonoBehaviour
{
    [SerializeField] Sprite[] Fkey;
    SpriteRenderer sr;
    GameObject npc;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }
}
