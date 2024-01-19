using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetBox : MonoBehaviour
{
    GameObject layerEnemy;
    SpriteRenderer sr;
    List<GameObject> listTarget;
    List<GameObject> listTemp;
    GameObject player;
    GameManager manager;
    GameObject target;
    GameObject temp;

    public static TargetBox Instance;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.transform.gameObject;
        Debug.Log(target);
    }
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
        player = GameObject.Find("Player");
        transform.position = new Vector2(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y);
        sr = GetComponent<SpriteRenderer>();
        layerEnemy = GameObject.Find("LayerEnemy");
    }
    void Start()
    {
        manager = GameManager.Instance;
        createList();
    }

    void Update()
    {
        //GameObject obj = Instantiate(objExplosion, transform.position, Quaternion.identity, layerDynamic);
        //Explosion objSc = obj.GetComponent<Explosion>();
        //float sizeWidth = sr.sprite.rect.width;//A.sprite.rect.width ! sprite의 size 중 width값을 가져옴
        //objSc.SetAnimationSize(sizeWidth);
    }

    bool boolCreateList;
    private void LateUpdate()
    {
        if (boolCreateList == false) { return; }
        if (boolCreateList == true)
        {
            int count = layerEnemy.transform.childCount;
            for (int iNum = 0; iNum < count; iNum++)
            {
                //Debug.Log(Vector3.Distance(player.transform.position, layerEnemy.transform.GetChild(iNum).transform.position));
                listTemp.Insert(iNum, layerEnemy.transform.GetChild(iNum).gameObject);
                //Debug.Log(listTemp[iNum].transform.position);
            }
        }
        boolCreateList = false;
        saveTargetList();
    }

    public List<GameObject> GetTargetList()
    {
        return listTemp;
    }
    public void SetCreateList()
    {
        boolCreateList = true;
    }
    private void createList()
    {
        listTarget = new List<GameObject>();
        listTemp = new List<GameObject>();
        listTarget.Insert(0, player);
        Debug.Log($"Save listTarget[0] = {listTarget[0].name}");
    }
    private void saveTargetList()
    {
        listTarget = new List<GameObject>();
        listTemp = new List<GameObject>();
        listTarget.Insert(0, player);
        for (int i = 0; i < listTemp.Count; i++)
        {
            for (int j = 0; j < listTemp.Count; j++)
            {
                if (Vector2.Distance(player.transform.position, listTemp[i].gameObject.transform.position) <
                    Vector2.Distance(player.transform.position, listTemp[j].gameObject.transform.position))
                {
                    temp = listTemp[i];
                    listTemp[i] = listTemp[j];
                    listTemp[j] = temp;
                }
            }
        }
        for (int i = 0; i < listTemp.Count; i++)
        {
            Debug.Log((Vector2.Distance(player.transform.position, listTemp[i].gameObject.transform.position)));
        }
    }
}
