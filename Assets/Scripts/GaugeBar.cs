using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    [SerializeField] private Image imgFrontHp;
    [SerializeField] private Image imgMidHp;
    private void Awake()
    {

    }
    void Start()
    {
        GameObject target = transform.parent.gameObject;
        if (target.gameObject.tag == "Player")
        {
            Player obj = target.GetComponent<Player>();
            obj.SetHp(this);
        }
        else if (target.gameObject.tag == "Enemy")
        {
            Enemy obj = target.GetComponent<Enemy>();
            obj.SetHp(this);
        }
        else if (target.gameObject.tag == "Ai")
        {
            Ai obj = target.GetComponent<Ai>();
            obj.SetHp(this);
        }

    }
    private void Update()
    {
        checkHp();
        //isDestroying();
    }
    private void checkHp()
    {
        float amountFront = imgFrontHp.fillAmount;
        float amountMid = imgMidHp.fillAmount;

        if (amountFront < amountMid)//mid°¡ ±ð¿©¾ß ÇÔ
        {
            imgMidHp.fillAmount -= Time.deltaTime * 0.5f;
            if (imgMidHp.fillAmount <= imgFrontHp.fillAmount)
            {
                imgMidHp.fillAmount = imgFrontHp.fillAmount;
            }
            else if (amountFront > amountMid)
            {
                imgMidHp.fillAmount = imgFrontHp.fillAmount;
            }
        }
    }
    private void isDestroying()
    {
        if (imgMidHp.fillAmount <= 0)
        {
            Debug.Log("Dead");
            Destroy(gameObject);
        }
    }

    public void SetHp(float _curHp, float _maxHp)
    {
        imgFrontHp.fillAmount = (float)_curHp / _maxHp;
    }
    
}