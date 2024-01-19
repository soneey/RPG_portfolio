using System;
using UnityEngine;
using static Study;

public class Study : MonoBehaviour
{
    //[SerializeField, Range(0.0f, 100.0f)] private float userHp = 20;
    public class PlayerData
    {
        public PlayerData(string _name)
        {
            userName = _name;
        }
        private string userName;
        public string GetUserName()
        {
            return userName;
        }
    }

    PlayerData[] user = new PlayerData[5];
    void Start()
    {
        int count = user.Length;
        string name = "user";
        for (int iNum = 0; iNum < count; iNum++)
        {
            string number = iNum.ToString();
            user[iNum] = new PlayerData(name + number);
            Debug.Log(user[iNum].GetUserName());
        }
        //Insert(3, "userInsert");
        Remove(2);
    }

    public void Insert(int _index, string _userName)
    {
        int count = user.Length;
        Debug.Log($"======================<color=aqua>Insert</color>({_index})");
        if (user[_index] == user[count - 1])
        {
            user[_index] = new PlayerData(_userName);
        }
        else
        {
            for (int iNum = count - 1; iNum > 0; iNum--)
            {
                user[iNum] = user[iNum - 1];
            }
            if (user[_index] == user[0])
            {
                user[_index] = new PlayerData(_userName);
            }
            else
            {
                for (int iNum = 0; iNum < _index; iNum++)
                {
                    user[iNum] = user[iNum + 1];
                }
                user[_index] = new PlayerData(_userName);
            }
        }

        for (int iNum = 0; iNum < count; iNum++)
        {
            Debug.Log($"user[{iNum}] = {user[iNum].GetUserName()}");
        }
    }

    public void Remove(int _index)
    {
        int count = user.Length;
        Debug.Log($"======================<color=aqua>Remove</color>({_index})");
        user[_index] = new PlayerData(null);
        //null+1�� ������
        for (int iNum = 0; iNum < count-1; iNum++)
        {
            if (user[iNum] == null)
            { 
                
            }
        }
        user[_index] = new PlayerData(null);
        //Debug.Log($"user[{iNum}] = {user[iNum].GetUserName()}");
    }
}

//insert ������ �ֱ�, remove ������ �����
//"userInsert";
//insert(int _index, string _userName) �Լ�
//remove(int _index)
//"0";

//2�� List
//����5�� ����� ��
//insert remove ����غ���




/*
//[�Ű����� ������ ! ]
//�Ű����� ������ in(�б����� cbr�� ����) out(������ �����͸� ����) ref(�ּҿ� ����) =()
//�Ű����� �տ� �Է��ؼ� ��� private void �Լ���(out )


//[���׸� ! �ڷ����� �������� ���� �����͸� ����]
//          private void �Լ���<T>() ���ǵ��� ���� �ڷ����� �Լ�
//          �ڵ� ���뼺�� �ſ� ���� ���ɻ� �ſ� ȿ�����̴�

//[��ųʸ�(Dictionary) ! ]

//����Ʈ(List) ! ����Ʈ �迭�� ������ ũ�⸦ �������� �ʰ� ���, �߰�, ���� �����ο�]
//               ����Ʈ<�ڷ���> ����Ʈ�� =  new List<�ڷ���>();
//               Add, Remove, Insert, Clear

//[����ü(struct) ! ]

//[�������̵� ! �θ�ȿ� ����ִ� �Լ��� ����� �������ؼ� �����]
//              �θ𿡴� virtual, �ڽĿ��� override�� �����ϰ� base.���� �θ��� ��ɿ� �����Ѵ�
//              �θ�� �ڽ��� �����͸� ����Ҽ� ����
//              �θ𿡰� ��ӵ� ��� �� �Ű������� �߰��� �޾ƾ� �� ���� �����ε��ϸ� �ȴ�

//[�����ε� ! �����̸��� �Լ��� ��������� �Ű������� �޸��ϴ� ��]

//[������(Constructor) ! ��ü�� �����ɶ� �ڵ����� ȣ��Ǵ� �Լ��� ��ü�� �ʱ�ȭ�� ���� ���]

//[class A ! class�ڷ����� ����A, �ݹ��̷��۷��� = �����Ҵ�]
*/

//==============================================================================����Ű
//�ּ� �巡���� Ctrl + k + c / ���� �巡���� Ctrl + k + u
//Ctrl + r + r = ������� �����̸��� ������ ���ÿ� ����
//Ctrl + k + d = �ٸ���
//Ctrl + .(>) = ���̺귯�� ����


//==============================================================================�����ε�
//�����ε� = �����̸��� �Լ��� ��������� �Ű������� �޸��ϴ� ��
//public Monster()//�����ε�
//{
//    sName = "no name";
//    iExp = 10;
//    fHp = 20;
//}
//public Monster(string _name)//�����ε�
//{
//    sName = _name;
//    iExp = 10;
//    fHp = 20;
//}
//public Monster(string _name, int _Exp, float _fHp)//������
//{
//    sName = _name;
//    iExp = _Exp;
//    fHp = _fHp;
//}

//private Monster Orc = new Monster("Orc", 10, 100);//������ �׻� private�� �����Ҵ��� �޾ƾ� ��� ����
//private Monster Gremlin = new Monster("Gremlin", 5, 20);//����
//private Monster Skeleton = new Monster("Skeleton");//�����ε�
//private Monster Dragon = new Monster();//�����ε�



//==============================================================================�����
//Debug.Log(++a);//10
//Debug.Log(a++);//10
//Debug.Log(a);//11
//Debug.Log(b);//9

//int a = 7;
//int b = a;
//int c = b++;
//b = a + b * c;



//==============================================================================����ȯ
//����ȯ, ���ڸ� ���ڷ�, ���ڸ� ���ڷ�

//string sValue = "����1234";//string sValue = string.Empty; ����ǥ��
//string siNum = Regex.Replace(sValue, @"\d", "");//(sValue����, @"\D"���ڸ�, ""�������� ó��);
//                                                //(sValue����, @"\d"���ڸ�, ""�������� ó��);

//sValue = iNum.ToString("D8");//int�����͸� string���� ����ȯ
//iNum = int.Parse(sValue);//string�����͸� int�� ����ȯ

//Debug.Log(siNum);//D8 = 00000010, N2 = �Ҽ���2�ڸ�, N0 = 10,000,000ǥ��



//==============================================================================�Լ� Debug.Log();
//int playerHP = 98;
//playerHP = playerHP - 10;
//playerHP += 5;
//playerHP -= 20;
//playerHP++;
//++playerHP;

//Debug.Log($"Ȯ�ο� �÷��̾� ü�� = {playerHP}");
//string value = "����" + playerHP;
//string value2 = $"����{playerHP}";

//string value3 = "����" + "����";
//string value4 = "�� ��";
//string value5 = "�� ���ϴ�.";

//Debug.Log($"{value4}{value5}");
//Debug.Log($"{2 + 2}");

//int ivalue1 = 12;
//int ivalue2 = 28;

//��ġ�ؽ�Ʈ ���۰˻�
//Debug.Log($"<color=aqua>���</color> ivalue1 + ivalue2 = {ivalue1 + ivalue2}");



//==============================================================================���ǹ� if, else if, else
//int playerHP = 30;
//bool checker = false;
//// < , > , == , != , <= , >=
//// && And���� : ��� ������ ���� ��, ���ʿ��� ���������� �����ϰ� ù��°������ �����̸� ���̻� �������� �ʴ´�
//// || Or���� : �� ���� �� �ϳ��� ���� ��, ù��° ������ false���� ���������� �����Ѵ�
//
////*GameObject objPlayer = null;
////*
////*if (objPlayer != null && objPlayer)
////*{
////*    objPlayer.SetActive(true);
////*}
//
//if (checker == true)//if(����)�� true�� ����
//{
//    Debug.Log("üĿ�� Ʈ�翴���ϴ�.");
//}
//else if (playerHP > 40 && playerHP < 40 + 10)//if(����)�� false, else if(����)�� ture�� ����
//{//A && B = A�� B�� true�� �� ���� And����
//    Debug.Log("�÷��̾�� 10�� �������� �޾ҽ��ϴ�.");
//    playerHP -= 10;
//}
//else if (playerHP < 30 || playerHP > 50)
//{//A || B = A�� B�� �ϳ��� true�� �� ���� Or����
//    Debug.Log("�÷��̾��� ü���� 10 ȸ���Ǿ����ϴ�.");
//    playerHP += 10;
//}
//else//if�� else if���� false�� ���Ǿ��� ����
//{
//    Debug.Log("�÷��̾ ����߽��ϴ�.");
//    playerHP = 0;
//}
//
//Debug.Log($"�÷��̾��� ü�� = {playerHP}");



//==============================================================================3�� ������
//int a = 7;
//int b = a;
//int c = b++;
//b = a + b * 7;//63

////3�� �����ڸ� if������ Ǯ�� �Ʒ��� ����  [? = ���ǽ�if]
//c = a >= 100 ? b : c / 10;//A = B ? C : D; B�� true�� C��, false�� D�� A�� �ʱ�ȭ
//        
//if (a >= 100)
//{
//    c = b;
//}
//else
//{
//    c = c / 10;
//}



//==============================================================================switch case break default
//string sValue = "abc";//������ ��ġ�ϴ� ������ �ٷ� �����Ѵ�
//switch (sValue)
//{
//    case "Abc":
//        Debug.Log("Abc");
//        break;
//    case "aBc":
//        Debug.Log("aBc");
//        break;
//    case "abC":
//        Debug.Log("abC");
//        break;
//    case "abc":
//        Debug.Log("abc");
//        break;
//    default:
//        Debug.Log("F");
//        break;
//}



//==============================================================================for
//�ʱ��; ���ǹ�; ������
//for (int iNum = 2; iNum <= 10; iNum++)
//{
//    if (iNum % 2 == 0)
//    {
//        Debug.Log(iNum);
//    }
//}


//for (int iaNum = 0; iaNum < 3; iaNum++)
//{
//    Debug.Log($"iaNum = {iaNum}");
//    for (int ibNum = 3; ibNum < 6; ibNum++)
//    {
//        Debug.Log($"ibNum = {ibNum}");
//        for (int icNum = 6; icNum < 9; icNum++)
//        {
//            Debug.Log($"icNum = {icNum}");
//        }
//    }
//}


//for (int a = 2; a <= 9; a++)
//{
//    for (int b = 1; b <= 9; b++)
//    {
//        Debug.Log($"{a} x {b} = {a*b}");
//    }
//}


//==============================================================================�����

//�ʱ��; ���ǹ�; ������
//Ư�����ڰ���"��"
//Ư�����ں�    "��"

//string sValue1 = "��";
//string sValue2 = "��";
//string sValue3 = "��";

//��
//�١�
//�١١�
//�١١١�
//�١١١١�

//for (int a = 0; a < 5; a++)
//{
//string sValue1 = "��";
//string sValue2 = "��";
//    Debug.Log(sValue1);
//    sValue1 += sValue2;
//}

//�١١١١�
//�١١١�
//�١١�
//�١�
//��

//for (int a = 0; a < 5; a++)
//{
//    string s1 = "��";
//    string s3 = "��";
//    for (int b = 5; b - a > 0; b--)
//    {
//        s3 += s1;
//    }
//    Debug.Log(s3);
//}

//���������� 4 1
//�������١� 3 2
//�����١١� 2 3
//���١١١� 1 4  
//�١١١١� 0 5

//for (int a = 0; a < 5; a++)
//{
//    string s1 = "��";
//    string s2 = "��";
//    string s3 = "";
//    for (int b = 0; a + b <= 3; b++)
//    {
//        s3 += s2;
//    }
//    for (int c = 0; c < a + 1; c++)
//    {
//        s3 += s1;
//    }
//    Debug.Log(s3);
//
//}


//�١١١١�
//���١١١�
//�����١١�
//�������١�
//����������

//for (int a = 0; a < 5; a++)
//{
//    string s1 = "��";
//    string s2 = "��";
//    string s3 = "";
//    for (int b = 0; b < a; b++)
//    {
//        s3 += s2;
//    }
//    for (int c = 5; c > a; c--)
//    {
//        s3 += s1;
//    }
//    Debug.Log(s3);
//
//}


//����������
//�������١١�
//�����١١١١�
//���١١١١١١�  
//�١١١١١١١١�

//for (int a = 0; a < 5; a++)//01234
//{
//    string s1 = "��";
//    string s2 = "��";
//    string s3 = "";
//    for (int b = 4; b > a; b--)//43210
//    {
//        s3 += s2;
//    }
//    for (int c = 0; c < a*2+1; c++)//13579
//    {
//        s3 += s1;
//    }
//    Debug.Log(s3);
//
//}


//�١١١١١١١١�
//���١١١١١١�  
//�����١١١١�
//�������١١�
//����������

//for (int a = 0; a < 5; a++)//01234
//{
//    string s1 = "��";
//    string s2 = "��";
//    string s3 = "";
//    for (int b = 0; b < a; b++)//01234
//    {
//        s3 += s2;
//    }
//    for (int c = 9; c >= a*2+1; c--)//97531
//    {
//        s3 += s1;
//    }
//    Debug.Log(s3);
//
//}



//==============================================================================while
//Debug.Log("1~9");
//int za = 1;
//while (za<10)//������ true�϶��� ����
//{
//    Debug.Log(za);
//    za++;
//}
//
//Debug.Log("9~1");
//int zb = 9;
//while (zb > 0)
//{
//    Debug.Log(zb);
//    zb--;
//}
//
//Debug.Log("������");
//int aa = 2;
//while (aa <= 9)
//{
//    int bb = 1;
//    while (bb <= 9)
//    {
//        Debug.Log($"{aa} x {bb} = {aa * bb}");
//            bb++;
//    }
//    aa++;
//}



//==============================================================================�Լ�
//�ݹ��̺��� ȣ���� ������ �� ������ �����ϴ� �� ����
//�ݹ��̷��۷��� ȣ���� ������ �� �ּҸ� �����ϴ� �� ����
//�����ε�

//private void A()
//{
//    for (int a = 9; a > 1; a--)
//    {
//        for (int b = 1; b <= 9; b++)
//        {
//            Debug.Log($"{a}x{b} = {a * b}");
//        }
//    }
//}


//9 2 8 3 7 4 6 5
//private void B()
//{
//    int dan = 1;
//    int AA = 9;
//    for (int a = 7; a >= 0; a--)
//    {
//        if (a % 2 == 1)
//        {
//            dan = a * -1;
//        }
//        else
//        {
//            dan = a;
//        }
//        for (int c = 1; c <= 9; c++)
//        {
//            Debug.Log($"{AA} x {c} = {AA * c}");
//        }
//        AA = AA + dan;
//    }
//}



//==============================================================================�迭
//int[] arrInt = new int[7];//0~6������
//
//for (int iNum = 0; iNum < arrInt.Length; iNum++)
//{
//    arrInt[iNum] = iNum;
//}
//for (int iNum = 0; iNum < arrInt.Length; iNum++)
//{
//    Debug.Log(arrInt[iNum]);
//}



//==============================================================================params
//params
//private void overPoint(int _target, params int[] _value)
//overPoint(85, 5, 4, 6, 3, 2, 4, 65, 16, 5);//��� �ø��� ����



//==============================================================================�Լ�
//����ó��
//���ü� �ִ� ������ �ϳ��� ����
//�Ҽ��� �������� ó���Ҽ� ����
//�迭�� �ϳ��� ���� �����Ͱ� ���ü� ����

//private void overPoint(int[] _value, int _target)
//{
//    if (_target < 0 || _target > 100)
//    {
//        Debug.Log($"������ �ùٸ��� �ʽ��ϴ�. �Էµ� Ÿ�� ������ {_target}�̾����ϴ�.");
//        return;
//    }
//    int count = _value.Length;
//    if (count == 0)
//    {
//        Debug.Log("�����Ͱ� �������� �ʴ� �迭�Դϴ�");
//        return;
//    }
//
//    bool find = false;//������ �ϳ��� ����ߴ���
//    for (int iNum = 0; iNum < count; iNum++)
//    {
//        int point = _value[iNum];
//        if (point >= _target)
//            find = true;
//            Debug.Log(point);
//    }
//    if (find == false)
//    {
//        Debug.Log($"{_target}�� �̻��� ������ �ϳ��� �������ϴ�.");
//    }
//}
//
//
//private void avg(int[] _value)
//{
//    int sum = 0;
//    int count = _value.Length;
//    for (int iNum = 0; iNum < count; iNum++)
//    {
//        sum += _value[iNum];
//    }
//    Debug.Log(sum / count);
//}



//==============================================================================���߹迭 ����
//string value = "HelloWorld";//10����
//char[];
//Ư�� ���ڿ��� �����ϰ� �ùٷ� ��µɼ� �ֵ��� string �ڷ����� ���弼��
//l�� �����ؼ� ���ڸ� ���弼��
//toString
//heoWord;

//swap - temp
//char[];
//�� ���ڰ� �Ųٷ� �������� �����ϰ� ��µɼ� �ֵ��� string �ڷ����� ���弼��
//dlroWolleH

//2�����̻��� �迭�� Length�� ��û�ϸ� x+y
//2�����迭�� ���̴� GetLength

//���߹迭, �������迭
//int[,] arr2Int = new int[2, 3] { { 0, 1, 2 }, { 3, 4, 5 } };
//                                00 01 02     10  11 12
//���߹迭�� ���ڸ�
//0, 1, 2
//3, 4, 5
//swap
//debug�� ����Ҷ�
//3, 4, 5 \n 0, 1, 2
//for (int a = 0; a < 3; a++)
//{
//        int temp = 0;
//        temp = arr2Int[0, a];
//        arr2Int[0, a] = arr2Int[1, a];
//        arr2Int[1, a] = temp;
//}
//string aValue = "";
//string aValue2 = ", ";
//string aValue3 = "\n";
//for (int bb = 0; bb < 2; bb++)
//{
//    for (int cc = 0; cc < 3; cc++)
//    {
//        if (cc != 2)
//        {
//            aValue += arr2Int[bb, cc].ToString();
//            aValue += aValue2;
//        }
//        else
//        {
//            aValue += arr2Int[bb, cc].ToString();
//            aValue += aValue3;
//        }
//    }
//}
//Debug.Log(aValue);




//������������ ����
//int[] arrint = { 0, 9, 7, 2, 1, 3, 4, 5, 8, 6 };
//int[] arrint = { 3, 4, 1, 0, 9, 2, 7, 5, 8, 6 };
//int temp = 0;
//int count = arrint.Length;
//for (int aa = 0; aa < count; aa++)
//{
//    for (int bb = aa + 1; bb < count; bb++)
//    {
//        if (arrint[aa] > arrint[bb])
//        {
//            temp = arrint[aa];
//            arrint[aa] = arrint[bb];
//            arrint[bb] = temp;
//        }
//    }
//}
//for (int c = 0; c < 10; c++)
//{
//    Debug.Log($"arrint{c}��° = {arrint[c]}");
//}



//��������
//int temp = 0;
//for (int aa = 0; aa < 10; aa++)
//{
//    for (int bb = 0; bb < 10; bb++)
//    {
//        if (arrint[aa] > arrint[bb])
//        {
//            temp = arrint[aa];
//            arrint[aa] = arrint[bb];
//            arrint[bb] = temp;
//        }
//    }
//}




//==============================================================================class
//private class cStudy//�ݹ��̷��۷��� = �����Ҵ�
//{
//    private int iValue = 10;
//    public float fValue = 20.0f;
//
//    public int GetIValue()
//    {
//        return iValue;
//    }
//    public void SetIValue(int _value)
//    {
//        iValue = _value;
//    }
//    public int _iValue//������Ƽ
//    {
//        get { return iValue; }
//        set { iValue = value; }
//    }
//    public float GetFValue()
//    {
//        return fValue;
//    }
//}
//cStudy test = new cStudy();
//**************************************start
//Debug.Log(test.GetIValue());
//test.SetIValue(20);
//Debug.Log(test.GetIValue());






//==============================================================================right()
//    void Start()
//    {
//
//        //int[,] arr3ints = new int[3, 3] { { 1, 0, 2 }, { 0, 0, 0 }, { 3, 0, 4 } };
//        //int[,] arr3ints = new int[3, 3] { { 1, 2, 3 }, { 8, 0, 4 }, { 7, 6, 5 } };
//        //int[,] arr3ints = new int[4, 4] { { 1, 2, 3, 4 }, { 12, 0, 0, 5 }, { 11, 0, 0, 6 }, { 10, 9, 8, 7 } };
//        //int[,] arr3ints = new int[3, 4] { { 1, 2, 3, 4 }, { 10, 0, 0, 5 }, { 9, 8, 7, 6 } };
//        int[,] arr3ints = new int[4, 3] { { 1, 2, 3 }, { 10, 0, 4 }, { 9, 0, 5 }, { 8, 7, 6 } };
//        
//        right(arr3ints, 3);
//
//    }
//
//    private void right(int[,] _value, int _value2)
//    {
//        int temp = 0;
//        int count = _value.GetLength(0);//b�����̵�
//        int count2 = _value.GetLength(1);//a�¿��̵�
//        for (int TurnRight = 0; TurnRight < _value2; TurnRight++)
//        {
//            temp = _value[0, 0];
//            for (int a = 1; a < count2; a++)//[0,1]���� ����
//            {
//                _value[0, 0] = _value[0, a];
//                _value[0, a] = temp;
//                temp = _value[0, 0];
//                //Debug.Log($"{a}��° = {_value[0,a]}");
//            }
//            //Debug.Log($"temp = {temp}");
//            for (int b = 1; b < count; b++)
//            {
//                _value[0, 0] = _value[b, count2 - 1];
//                _value[b, count2 - 1] = temp;
//                temp = _value[0, 0];
//                //Debug.Log($"{b}��° = {_value[b, count2-1]}");
//            }
//            //Debug.Log($"temp = {temp}");//[2,2] ���� temp = 5
//            for (int a = count2 - 1; a > 0; a--)
//            {
//                _value[0, 0] = _value[count - 1, a - 1];//a�� 1>0
//                _value[count - 1, a - 1] = temp;
//                temp = _value[0, 0];
//                //Debug.Log($"{a}��° = {_value[count - 1, a - 1]}");
//            }
//            //Debug.Log($"temp = {temp}");//[2,0] ���� temp = 7
//            //  [0,0] [0,1] [0,2] [1,2] [2,2] [2,1] [2,0] [1,0] [0,0]
//            //  [1,0] [1,1] [1,2]
//            //  [2,0] [2,1] [2,2]
//            //  1   2   3
//            //  8   9   4
//            //  7   6   5
//            for (int b = count - 1; b > 0; b--)
//            {
//                _value[0, 0] = _value[b - 1, 0];
//                _value[b - 1, 0] = temp;
//                temp = _value[0, 0];
//                //Debug.Log($"{b}��° = {_value[b - 1, 0]}");
//            }
//        }
//        string aValue = "";
//        string bValue = "   ";
//        string cValue = "\n";
//        for (int bb = 0; bb < count; bb++)
//        {
//            for (int aa = 0; aa < count2; aa++)
//            {
//                aValue += _value[bb, aa];
//                aValue += bValue;
//            }
//            aValue += cValue;
//        }
//        Debug.Log(aValue);
//    }
//
//}



//==============================================================================left()
//void Start()
//{
//    int[,] arr3ints1 = new int[3, 3] { { 1, 2, 3 }, { 8, 0, 4 }, { 7, 6, 5 } };
//    int[,] arr3ints2 = new int[4, 4] { { 1, 2, 3, 4 }, { 12, 0, 0, 5 }, { 11, 0, 0, 6 }, { 10, 9, 8, 7 } };
//    int[,] arr3ints3 = new int[3, 4] { { 1, 2, 3, 4 }, { 10, 0, 0, 5 }, { 9, 8, 7, 6 } };
//    int[,] arr3ints4 = new int[4, 3] { { 1, 2, 3 }, { 10, 0, 4 }, { 9, 0, 5 }, { 8, 7, 6 } };
//    int[,] arr3ints5 = new int[2, 2] { { 1, 2 }, { 4, 3 } };
//    int[,] arr3ints6 = new int[4, 2] { { 1, 2 }, { 8, 3 }, { 7, 4 }, { 6, 5 } };
//    int[,] arr3ints7 = new int[2, 4] { { 1, 2, 3, 4 }, { 8, 7, 6, 5 } };
//    int[,] arr3ints8 = new int[5, 5] { { 1, 2, 3, 4, 5 }, { 16, 0, 0, 0, 6 }, { 15, 0, 0, 0, 7 }, { 14, 0, 0, 0, 8 }, { 13, 12, 11, 10, 9 } };
//    
//    Left(arr3ints1, 2);
//    Left(arr3ints2, 2);
//    Left(arr3ints3, 2);
//    Left(arr3ints4, 2);
//    Left(arr3ints5, 2);
//    Left(arr3ints6, 2);
//    Left(arr3ints7, 2);
//    Left(arr3ints8, 4);
//}
//
//    private void Left(int[,] _value, int _value2)
//    {
//        int temp = 0;
//        int count = _value.GetLength(0);
//        int count2 = _value.GetLength(1);
//        for (int TurnLeft = 0; TurnLeft < _value2; TurnLeft++)
//        {
//            temp = _value[0, 0];
//            for (int a = 1; a < count; a++)
//            {
//                _value[0, 0] = _value[a, 0];
//                _value[a, 0] = temp;
//                temp = _value[0, 0];
//            }
//            for (int b = 1; b < count2; b++)
//            {
//                _value[0, 0] = _value[count - 1, b];
//                _value[count - 1, b] = temp;
//                temp = _value[0, 0];
//            }
//            for (int a = count - 1; a > 0; a--)
//            {
//                _value[0, 0] = _value[a - 1, count2 - 1];
//                _value[a - 1, count2 - 1] = temp;
//                temp = _value[0, 0];
//            }
//            for (int b = count2 - 1; b > 0; b--)
//            {
//                _value[0, 0] = _value[0, b - 1];
//                _value[0, b - 1] = temp;
//                temp = _value[0, 0];
//            }
//        }
//        string aValue = "";
//        string bValue = "   ";
//        string cValue = "\n";
//        for (int bb = 0; bb < count; bb++)
//        {
//            for (int aa = 0; aa < count2; aa++)
//            {
//                aValue += _value[bb, aa];
//                aValue += bValue;
//            }
//            aValue += cValue;
//        }
//        Debug.Log(aValue);
//    }





//==============================================================================class
//public class Monster//�ݹ��̷��۷��� = �����Ҵ�, Ŭ������ ����, private�� ����� �ٸ� ��ũ��Ʈ���� ������� ����
//{
//    public Monster()//�����ε�
//    {
//        sName = "no name";
//        iExp = 10;
//        fHp = 20;
//    }
//    public Monster(string _name)//�����ε�
//    {
//        sName = _name;
//        iExp = 10;
//        fHp = 20;
//    }
//    public Monster(string _name, int _Exp, float _fHp)//������
//    {
//        sName = _name;
//        iExp = _Exp;
//        fHp = _fHp;
//    }
//    ~Monster()//�Ҹ���
//    {
//        Debug.Log($"{sName} �����Ͱ� �����Ǿ����ϴ�.");
//    }
//
//    private string sName;
//    private int iExp;
//    private float fHp;
//    private float fDamege;
//    private int iLevel;
//    private float fDefence;
//    private float fSpeed;
//
//    public void functionGetExp()
//    {
//        Debug.Log($"{sName} ������ ����ġ�� {iExp}�Դϴ�.");
//    }
//    public void functionSetExp(int _iExp)
//    {
//        iExp = _iExp;
//    }
//    public string GetName()
//    {
//        return sName;
//    }
//}
//
//public class Orc : Monster
//{
//    public void GetName()
//    {
//        base.GetName();
//    }
//}

//private Monster Orc = new Monster();//������ �׻� private�� �����Ҵ��� �޾ƾ� ��� ����
//private Monster Gremlin = new Monster();//����
//private Monster Dragon;

//void Start()
//{
//    //Orc.iExp = 20; iExp�� Moster�� ���̹Ƿ� �ٷ� ������ �Ұ���
//    Orc.functionGetExp();//public �Լ��� ���� ���� ���� * Orc = 10
//    Orc.functionSetExp(100);//��ũ�� ����ġ�� 100���� ���� * Orc = 100
//    Gremlin = Orc;//Gremlin���� Orc�� �ּҰ� �� * Gremlin = Orc
//    Gremlin.functionGetExp();//�׷����� ����ġ Ȯ�� * Gremlin = Orc = 100

//    Orc.functionSetExp(1);//��ũ�� ����ġ�� 1�� ���� * Orc = 1
//    Gremlin.functionGetExp();//�׷����� ����ġ Ȯ�� * Gremlin = Orc = 1
//                             //�ݹ��̷��۷����� �ּҼ��� �����Ͱ� ����Ǹ� ���� �ּҿ� �����Ͱ� �ԷµǹǷ�
//                             //��� �����ּҸ� ȣ���ϴ� �����ʹ� ���� �����͸� �����
//    Dragon = Orc;
//    Dragon.functionGetExp();// Dragon = Orc = 1
//    Orc.functionSetExp(20);// Orc = 20
//    Dragon.functionGetExp();// Dragon = Orc = 20
//}




//==============================================================================���
//public class Monster//�ݹ��̷��۷��� = �����Ҵ�, Ŭ������ ����, private�� ����� �ٸ� ��ũ��Ʈ���� ������� ����
//{
//    public Monster(string _sName, float _fHp)//[������(Constructor)�� ��ü�� �����ɶ� �ڵ����� ȣ��Ǵ� �Լ��� ��ü�� �ʱ�ȭ�� ���� ���]
//    {
//        sName = _sName;
//        fHp = _fHp;
//    }
//    protected string sName;
//    private float fHp;
//
//    public virtual void ShowData()//�θ𿡰Դ� virtual
//    {
//        Debug.Log($"�̸�:{sName}");
//        Debug.Log($"ü��:{fHp}");
//    }
//}
//
//public class HighMonster : Monster//�ڽ� : �θ� [�������̵� �θ�ȿ� ����ִ� �Լ��� ����� �޸��ؼ� �����]
//{
//    protected int iRootNum;
//
//    public HighMonster(string _sName, float _fHp, int _root) : base(_sName, _fHp)
//    {
//        iRootNum = _root;
//    }
//    public override void ShowData()//�ڽĿ��Դ� override
//    {
//        //base.sName = "";//protected�� ��밡��
//        //base.fHp; ���Ұ�
//        base.ShowData();//[base. �θ��� ����� ����Ҷ� ��]
//        Debug.Log($"����:{iRootNum}");
//    }
//    //�θ𿡰� ��ӵ� ��� �� �Ű������� �߰��� �޾ƾ� �� ���� �����ε��ϸ� �ȴ�
//    public void ShowData(int _exp)//�����ε�
//    {
//        Debug.Log("�����ǵ� �Լ��Դϴ�.");
//    }
//}
//
//
//void Start()
//{
//    Monster Orc = new Monster("��ũ", 100);
//    Orc.ShowData();
//
//    HighMonster spOrc = new HighMonster("Ư����ũ", 200, 177);
//    spOrc.ShowData();
//}




//==============================================================================List
//int count = ListRemainMonster.Count;//List�� Length�� A.Count�� ����
//public class Monster
//{
//    private int Hp;
//    private int Mp;
//    private int Exp;

//    public int HHp
//    {
//        get { return Hp; }
//        set { Hp = value; }
//    }
//}


//void Start()
//{
//    //List<int> ListInts = new List<int>();
//    //int count = 10;
//    //for (int iNum = 0; iNum < count; iNum++)
//    //{
//    //    int iValue = 0;
//    //    ListInts.Add(iValue);
//    //    iValue = iNum;
//    //}
//    //if (count == 10) { }


//    //List<Monster> ListRemainMonster = new List<Monster>();
//    //int count = 10;
//    //for (int iNum = 0; iNum < count; iNum++)
//    //{
//    //    //Monster monster = new Monster();//�ݹ��̷��۷����� ������ �� ���� �ּҰ� �ٸ��� ����
//    //    //ListRemainMonster.Add(monster);
//    //    //monster.HHp = iNum;
//    //    //------------------------------------------------------------���Ͱ���
//    //    ListRemainMonster.Add(new Monster());
//    //    //ListRemainMonster[iNum].HHp = iNum;
//    //    Monster monster = ListRemainMonster[iNum];
//    //    monster.HHp = iNum;
//    //    ListRemainMonster[iNum].HHp = iNum;
//    //
//    //}
//    //if (count == 10) { }//debuging

//    //int count = ListRemainMonster.Count;
//}




//==============================================================================Dictionary
//����(Key), ������ �����̴�(value)
//Dictionary<string, string> dicDict = new Dictionary<string, string>();

//string userNumbering = "00000000";
//PlayerData data = new PlayerData();

/*
#region ��ųʸ��϶�
Dictionary<string, PlayerData> dicPlayerData = new Dictionary<string, PlayerData>();

dicPlayerData.Add(userNumbering, data);

PlayerData findData = dicPlayerData[userNumbering];
#endregion
*/

//List<PlayerData> ListPlayerData = new List<PlayerData>();
//ListPlayerData.Add(findData);
//int count = ListPlayerData.Count;
//for (int iNum = 0; iNum < count; iNum++)
//{
//    if (ListPlayerData[iNum].userName == userNumbering)
//    {
//        break;
//    }
//}


////List.Add
////List.Remove
////List.Insert ! Dictionary.Insert�� �迭�� ������ �ƴϾ ����� �� ����
////List�� array�� �ּҸ� ������ �ּҹ�ȣ(0~)�� �����ؼ� ������� ǥ��
////List.Clear

//dicDict.Add("����", "������ �����̴�");
//dicDict.Remove("����");
//dicDict.Clear();

//string value = dicDict["����"];



//==============================================================================�迭 List ����
//public class PlayerData
//{
//    public PlayerData(string _name)
//    {
//        userName = _name;
//    }
//    private string userName;
//    public string GetUserName()
//    {
//        return userName;
//    }
//}
//
//PlayerData[] user = new PlayerData[5];
//void Start()
//{
//    //1�� �迭
//    //Ŭ�����迭�� ���弼��
//    //����5�� 0���� Ŭ�������� ��� userName�� �ڽ��� ��ȣ�� �̸��� �ǵ��� ������ּ���
//    //0 ���������� "user0"
//    //1 ���������� "user1"
//    int count = user.Length;
//    string name = "user";
//    for (int iNum = 0; iNum < count; iNum++)
//    {
//        string number = iNum.ToString();
//        user[iNum] = new PlayerData(name + number);
//        Debug.Log(user[iNum].GetUserName());
//    }
//    Debug.Log("============================");
//    //if (count == 5) { }
//    Insert(2, "userInsert");
//    Remove(2);
//}
//
//public void Insert(int _index, string _userName)
//{
//    int count = user.Length;
//    PlayerData[] userSwap = new PlayerData[count + 1];
//    userSwap[_index] = new PlayerData(_userName);
//    for (int iNum = 0; iNum < count; iNum++)
//    {
//        if (userSwap[iNum] == null)
//        {
//            userSwap[iNum] = user[iNum];
//        }
//        else
//        {
//            userSwap[iNum + 1] = user[iNum];
//        }
//    }
//    int count2 = userSwap.Length;
//    for (int iNum = 0; iNum < count2; iNum++)
//    {
//        Debug.Log(userSwap[iNum].GetUserName());
//    }
//    Debug.Log("============================");
//
//}
//public void Remove(int _index)
//{
//    string name = "0";
//    int count = user.Length;
//    user[_index] = new PlayerData(name);
//    for (int iNum = 0; iNum < count; iNum++)
//    {
//        Debug.Log(user[iNum].GetUserName());
//    }
//    Debug.Log("============================");
//
//}
//

//==============================================================================���׸� Generic
//void Start()
//{
//    TestGeneric<int> testGeneric = new TestGeneric<int>();
//    Debug.Log(testGeneric.GetData());//0
//    TestGeneric<PlayerData> test2 = new TestGeneric<PlayerData>();
//    Debug.Log(test2.GetData());//null
//
//    PlayerData data1 = new PlayerData("abc");
//    PlayerData data2 = new PlayerData("def");

//    swap(ref data1, ref data2);
//
//}
//
//public class TestGeneric<T>
//{
//    public T _genericValue;//�ڷ����� ������� ���� ������
//    public T GetData()
//    {
//        return _genericValue;
//    }
//}
//
//private void swap<T>(ref T _a, ref T _b)
//{
//    Debug.Log(_a);
//    Debug.Log(_b);
//    T temp = _a;
//    _a = _b;
//    _b = temp;
//    Debug.Log(_a);
//    Debug.Log(_b);
//}



//==============================================================================�Ű����� ������ out
//void Start()
//{
//    float fValue = sum(1.5f, 2.5f, out float c);
//    List<PlayerData> ListPlayer = new List<PlayerData>();
//    int counter = 100;
//    for (int iNum = 0; iNum < counter; iNum++)
//    {
//        ListPlayer.Add(new PlayerData(iNum.ToString()));
//    }
//    PlayerData data;
//    bool bValue = findPlayerToList(ListPlayer, "99", out data);//bool bValue = findPlayerToList(ListPlayer, "99", out PlayerData data);
//    if (bValue == true)
//    {
//        Debug.Log("�ش� ������ ���� �߽��ϴ�.");
//    }
//    else
//    {
//        Debug.Log("�ش� ������ �������� �ʽ��ϴ�.");
//    }
//    Debug.Log(data.GetUserName());
//
//}
//private bool findPlayerToList(List<PlayerData> _List, string _findUserName, out PlayerData _player)
//{
//    int count = _List.Count;
//    for (int iNum = 0; iNum < count; iNum++)
//    {
//        if (_List[iNum].GetUserName() == _findUserName)
//        {
//            _player = _List[iNum];
//            return true;
//        }
//    }
//    _player = default;
//    return false;
//}
//public float sum(float _a, float _b, out float _c)
//{
//    _c = default;//int = 0, float = 0.0f, string = "" class = null�� ����
//    _c = _a * _b;
//    return _a + _b;
//}



//==============================================================================�Ű����� ������ ref
//void Start()
//{
//    int valueA = 1;//cbv
//    int valueB = 2;
//    PlayerData ValueA = new PlayerData("A");//cbr
//    PlayerData ValueB = new PlayerData("B");
//
//
//    swap(ref valueA, ref valueB);
//    Debug.Log(valueA);
//    Debug.Log(valueB);
//
//    swap(ref ValueA, ref ValueB);
//    Debug.Log(ValueA);
//    Debug.Log(ValueB);
//}
//
//private void swap(ref int _a, ref int _b)
//{
//    int temp = _a;
//    _a = _b;
//    _b = temp;
//}
//private void swap(ref PlayerData _a, ref PlayerData _b)
//{
//    PlayerData temp = _a;
//    _a = _b;
//    _b = temp;
//}



//==============================================================================�Ű����� ������ in
//void Start()
//{
//    int valueA = 1;
//    int valueB = 2;
//
//    swap(valueA, valueB);
//}
//
//private void swap(in int _a, in int _b)
//{
//    int temp = _a;//�б������̶� ������ ������ �����ϳ� ������ �Ұ����ϴ�
//                  //error//_a = _b;
//                  //error//_b = temp;
//}



//==============================================================================�Ű����� ������ =
//void Start()
//{
//    int valueA = 1;
//    int valueB = 2;
//
//    int value = sum(valueA, valueB, 10);
//    sum(valueA, valueB);
//}
//
//private int sum(int _a, int _b, int _c = 0, int _d = 0)
//{
//    return _a + _b;
//}




//==============================================================================class�迭 Insert�����
//public class PlayerData
//{
//    public PlayerData(string _name)
//    {
//        userName = _name;
//    }
//    private string userName;
//    public string GetUserName()
//    {
//        return userName;
//    }
//}
//
//PlayerData[] user = new PlayerData[5];
//void Start()
//{
//    int count = user.Length;
//    string name = "user";
//    for (int iNum = 0; iNum < count; iNum++)
//    {
//        string number = iNum.ToString();
//        user[iNum] = new PlayerData(name + number);
//        Debug.Log(user[iNum].GetUserName());
//    }
//    Insert(3, "userInsert");
//}
//
//public void Insert(int _index, string _userName)
//{
//    int count = user.Length;
//    Debug.Log($"======================<color=aqua>Insert</color>({_index})");
//    if (user[_index] == user[count - 1])
//    {
//        user[_index] = new PlayerData(_userName);
//    }
//    else
//    {
//        for (int iNum = count - 1; iNum > 0; iNum--)
//        {
//            user[iNum] = user[iNum - 1];
//        }
//        if (user[_index] == user[0])
//        {
//            user[_index] = new PlayerData(_userName);
//        }
//        else
//        {
//            for (int iNum = 0; iNum < _index; iNum++)
//            {
//                user[iNum] = user[iNum + 1];
//            }
//            user[_index] = new PlayerData(_userName);
//        }
//    }
//
//    for (int iNum = 0; iNum < count; iNum++)
//    {
//        Debug.Log($"user[{iNum}] = {user[iNum].GetUserName()}");
//    }
//}



//==============================================================================class�迭 Remove�����
//public void Remove(int _index)
//{
//    int count = user.Length;
//    Debug.Log($"======================<color=aqua>Remove</color>({_index})");
//    user[_index] = new PlayerData(null);
//    //null+1�� ������
//    for (int iNum = 0; iNum < count - 1; iNum++)
//    {
//        if (user[iNum] == null)
//        {
//
//        }
//    }
//    user[_index] = new PlayerData(null);
//    //Debug.Log($"user[{iNum}] = {user[iNum].GetUserName()}");
//}

