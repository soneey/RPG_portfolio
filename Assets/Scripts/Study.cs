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
        //null+1을 앞으로
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

//insert 데이터 넣기, remove 데이터 지우기
//"userInsert";
//insert(int _index, string _userName) 함수
//remove(int _index)
//"0";

//2번 List
//유저5명 등록한 후
//insert remove 사용해보기




/*
//[매개변수 한정자 ! ]
//매개변수 한정자 in(읽기전용 cbr를 전달) out(밖으로 데이터를 전달) ref(주소에 접근) =()
//매개변수 앞에 입력해서 사용 private void 함수명(out )


//[제네릭 ! 자료형이 결정되지 않은 데이터를 넣음]
//          private void 함수명<T>() 정의되지 않은 자료형의 함수
//          코드 재사용성이 매우 높고 성능상 매우 효율적이다

//[딕셔너리(Dictionary) ! ]

//리스트(List) ! 리스트 배열과 같지만 크기를 결정하지 않고 사용, 추가, 삭제 자유로움]
//               리스트<자료형> 리스트명 =  new List<자료형>();
//               Add, Remove, Insert, Clear

//[구조체(struct) ! ]

//[오버라이딩 ! 부모안에 들어있는 함수를 기능을 재정의해서 사용함]
//              부모에는 virtual, 자식에는 override를 선언하고 base.으로 부모의 기능에 접근한다
//              부모는 자식의 데이터를 사용할수 없다
//              부모에게 상속된 기능 외 매개변수를 추가로 받아야 할 경우는 오버로딩하면 된다

//[오버로딩 ! 같은이름의 함수를 사용하지만 매개변수를 달리하는 것]

//[생성자(Constructor) ! 객체가 생성될때 자동으로 호출되는 함수로 객체의 초기화를 위해 사용]

//[class A ! class자료형의 변수A, 콜바이레퍼런스 = 동적할당]
*/

//==============================================================================단축키
//주석 드래그후 Ctrl + k + c / 해제 드래그후 Ctrl + k + u
//Ctrl + r + r = 사용중인 같은이름의 변수를 동시에 변경
//Ctrl + k + d = 줄맞춤
//Ctrl + .(>) = 라이브러리 연결


//==============================================================================오버로딩
//오버로딩 = 같은이름의 함수를 사용하지만 매개변수를 달리하는 것
//public Monster()//오버로딩
//{
//    sName = "no name";
//    iExp = 10;
//    fHp = 20;
//}
//public Monster(string _name)//오버로딩
//{
//    sName = _name;
//    iExp = 10;
//    fHp = 20;
//}
//public Monster(string _name, int _Exp, float _fHp)//생성자
//{
//    sName = _name;
//    iExp = _Exp;
//    fHp = _fHp;
//}

//private Monster Orc = new Monster("Orc", 10, 100);//변수는 항상 private로 동적할당을 받아야 사용 가능
//private Monster Gremlin = new Monster("Gremlin", 5, 20);//변수
//private Monster Skeleton = new Monster("Skeleton");//오버로딩
//private Monster Dragon = new Monster();//오버로딩



//==============================================================================조사식
//Debug.Log(++a);//10
//Debug.Log(a++);//10
//Debug.Log(a);//11
//Debug.Log(b);//9

//int a = 7;
//int b = a;
//int c = b++;
//b = a + b * c;



//==============================================================================형변환
//형변환, 글자를 숫자로, 숫자를 글자로

//string sValue = "글자1234";//string sValue = string.Empty; 같은표현
//string siNum = Regex.Replace(sValue, @"\d", "");//(sValue에서, @"\D"문자를, ""공백으로 처리);
//                                                //(sValue에서, @"\d"숫자를, ""공백으로 처리);

//sValue = iNum.ToString("D8");//int데이터를 string으로 형변환
//iNum = int.Parse(sValue);//string데이터를 int로 형변환

//Debug.Log(siNum);//D8 = 00000010, N2 = 소수점2자리, N0 = 10,000,000표현



//==============================================================================함수 Debug.Log();
//int playerHP = 98;
//playerHP = playerHP - 10;
//playerHP += 5;
//playerHP -= 20;
//playerHP++;
//++playerHP;

//Debug.Log($"확인용 플레이어 체력 = {playerHP}");
//string value = "글자" + playerHP;
//string value2 = $"글자{playerHP}";

//string value3 = "글자" + "글자";
//string value4 = "잘 합";
//string value5 = "쳐 집니다.";

//Debug.Log($"{value4}{value5}");
//Debug.Log($"{2 + 2}");

//int ivalue1 = 12;
//int ivalue2 = 28;

//리치텍스트 구글검색
//Debug.Log($"<color=aqua>결과</color> ivalue1 + ivalue2 = {ivalue1 + ivalue2}");



//==============================================================================조건문 if, else if, else
//int playerHP = 30;
//bool checker = false;
//// < , > , == , != , <= , >=
//// && And연산 : 모든 조건이 참일 때, 왼쪽에서 오른쪽으로 진행하고 첫번째연산이 거짓이면 더이상 진행하지 않는다
//// || Or연산 : 두 조건 중 하나라도 참일 때, 첫번째 연산이 false여도 다음연산을 진행한다
//
////*GameObject objPlayer = null;
////*
////*if (objPlayer != null && objPlayer)
////*{
////*    objPlayer.SetActive(true);
////*}
//
//if (checker == true)//if(조건)이 true면 실행
//{
//    Debug.Log("체커가 트루였습니다.");
//}
//else if (playerHP > 40 && playerHP < 40 + 10)//if(조건)이 false, else if(조건)이 ture면 실행
//{//A && B = A와 B가 true일 때 실행 And연산
//    Debug.Log("플레이어는 10의 데미지를 받았습니다.");
//    playerHP -= 10;
//}
//else if (playerHP < 30 || playerHP > 50)
//{//A || B = A나 B중 하나라도 true일 때 실행 Or연산
//    Debug.Log("플레이어의 체력이 10 회복되었습니다.");
//    playerHP += 10;
//}
//else//if과 else if문이 false면 조건없이 실행
//{
//    Debug.Log("플레이어가 즉사했습니다.");
//    playerHP = 0;
//}
//
//Debug.Log($"플레이어의 체력 = {playerHP}");



//==============================================================================3항 연산자
//int a = 7;
//int b = a;
//int c = b++;
//b = a + b * 7;//63

////3항 연산자를 if문으로 풀면 아래와 같다  [? = 조건식if]
//c = a >= 100 ? b : c / 10;//A = B ? C : D; B가 true면 C를, false면 D를 A에 초기화
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
//string sValue = "abc";//조건이 일치하는 곳으로 바로 접근한다
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
//초기식; 조건문; 증감식
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


//==============================================================================별찍기

//초기식; 조건문; 증감식
//특수문자공백"　"
//특수문자별    "☆"

//string sValue1 = "☆";
//string sValue2 = "☆";
//string sValue3 = "　";

//☆
//☆☆
//☆☆☆
//☆☆☆☆
//☆☆☆☆☆

//for (int a = 0; a < 5; a++)
//{
//string sValue1 = "☆";
//string sValue2 = "☆";
//    Debug.Log(sValue1);
//    sValue1 += sValue2;
//}

//☆☆☆☆☆
//☆☆☆☆
//☆☆☆
//☆☆
//☆

//for (int a = 0; a < 5; a++)
//{
//    string s1 = "☆";
//    string s3 = "　";
//    for (int b = 5; b - a > 0; b--)
//    {
//        s3 += s1;
//    }
//    Debug.Log(s3);
//}

//　　　　☆ 4 1
//　　　☆☆ 3 2
//　　☆☆☆ 2 3
//　☆☆☆☆ 1 4  
//☆☆☆☆☆ 0 5

//for (int a = 0; a < 5; a++)
//{
//    string s1 = "☆";
//    string s2 = "　";
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


//☆☆☆☆☆
//　☆☆☆☆
//　　☆☆☆
//　　　☆☆
//　　　　☆

//for (int a = 0; a < 5; a++)
//{
//    string s1 = "☆";
//    string s2 = "　";
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


//　　　　☆
//　　　☆☆☆
//　　☆☆☆☆☆
//　☆☆☆☆☆☆☆  
//☆☆☆☆☆☆☆☆☆

//for (int a = 0; a < 5; a++)//01234
//{
//    string s1 = "☆";
//    string s2 = "　";
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


//☆☆☆☆☆☆☆☆☆
//　☆☆☆☆☆☆☆  
//　　☆☆☆☆☆
//　　　☆☆☆
//　　　　☆

//for (int a = 0; a < 5; a++)//01234
//{
//    string s1 = "☆";
//    string s2 = "　";
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
//while (za<10)//조건이 true일때만 실행
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
//Debug.Log("구구단");
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



//==============================================================================함수
//콜바이벨류 호출이 들어왔을 때 값만을 복사하는 것 참조
//콜바이레퍼런스 호출이 들어왔을 때 주소를 복사하는 것 복사
//오버로딩

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



//==============================================================================배열
//int[] arrInt = new int[7];//0~6번까지
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
//overPoint(85, 5, 4, 6, 3, 2, 4, 65, 16, 5);//계속 늘릴수 있음



//==============================================================================함수
//예외처리
//나올수 있는 점수가 하나도 없음
//소수가 들어왔을때 처리할수 없음
//배열이 하나도 없는 데이터가 들어올수 있음

//private void overPoint(int[] _value, int _target)
//{
//    if (_target < 0 || _target > 100)
//    {
//        Debug.Log($"점수가 올바르지 않습니다. 입력된 타겟 점수는 {_target}이었습니다.");
//        return;
//    }
//    int count = _value.Length;
//    if (count == 0)
//    {
//        Debug.Log("데이터가 존재하지 않는 배열입니다");
//        return;
//    }
//
//    bool find = false;//점수를 하나라도 출력했는지
//    for (int iNum = 0; iNum < count; iNum++)
//    {
//        int point = _value[iNum];
//        if (point >= _target)
//            find = true;
//            Debug.Log(point);
//    }
//    if (find == false)
//    {
//        Debug.Log($"{_target}점 이상의 점수가 하나도 없었습니다.");
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



//==============================================================================이중배열 문제
//string value = "HelloWorld";//10글자
//char[];
//특정 문자열을 삭제하고 올바로 출력될수 있도록 string 자료형을 만드세요
//l을 삭제해서 문자를 만드세요
//toString
//heoWord;

//swap - temp
//char[];
//이 문자가 거꾸로 나오도록 정리하고 출력될수 있도록 string 자료형을 만드세요
//dlroWolleH

//2차원이상의 배열은 Length를 요청하면 x+y
//2차원배열의 길이는 GetLength

//이중배열, 이차원배열
//int[,] arr2Int = new int[2, 3] { { 0, 1, 2 }, { 3, 4, 5 } };
//                                00 01 02     10  11 12
//이중배열의 숫자를
//0, 1, 2
//3, 4, 5
//swap
//debug로 출력할때
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




//오름차순으로 정렬
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
//    Debug.Log($"arrint{c}번째 = {arrint[c]}");
//}



//내림차순
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
//private class cStudy//콜바이레퍼런스 = 동적할당
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
//    public int _iValue//프로퍼티
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
//        int count = _value.GetLength(0);//b상하이동
//        int count2 = _value.GetLength(1);//a좌우이동
//        for (int TurnRight = 0; TurnRight < _value2; TurnRight++)
//        {
//            temp = _value[0, 0];
//            for (int a = 1; a < count2; a++)//[0,1]부터 시작
//            {
//                _value[0, 0] = _value[0, a];
//                _value[0, a] = temp;
//                temp = _value[0, 0];
//                //Debug.Log($"{a}번째 = {_value[0,a]}");
//            }
//            //Debug.Log($"temp = {temp}");
//            for (int b = 1; b < count; b++)
//            {
//                _value[0, 0] = _value[b, count2 - 1];
//                _value[b, count2 - 1] = temp;
//                temp = _value[0, 0];
//                //Debug.Log($"{b}번째 = {_value[b, count2-1]}");
//            }
//            //Debug.Log($"temp = {temp}");//[2,2] 시작 temp = 5
//            for (int a = count2 - 1; a > 0; a--)
//            {
//                _value[0, 0] = _value[count - 1, a - 1];//a가 1>0
//                _value[count - 1, a - 1] = temp;
//                temp = _value[0, 0];
//                //Debug.Log($"{a}번째 = {_value[count - 1, a - 1]}");
//            }
//            //Debug.Log($"temp = {temp}");//[2,0] 시작 temp = 7
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
//                //Debug.Log($"{b}번째 = {_value[b - 1, 0]}");
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
//public class Monster//콜바이레퍼런스 = 동적할당, 클래스를 정의, private로 만들면 다른 스크립트에서 사용하지 못함
//{
//    public Monster()//오버로딩
//    {
//        sName = "no name";
//        iExp = 10;
//        fHp = 20;
//    }
//    public Monster(string _name)//오버로딩
//    {
//        sName = _name;
//        iExp = 10;
//        fHp = 20;
//    }
//    public Monster(string _name, int _Exp, float _fHp)//생성자
//    {
//        sName = _name;
//        iExp = _Exp;
//        fHp = _fHp;
//    }
//    ~Monster()//소멸자
//    {
//        Debug.Log($"{sName} 데이터가 삭제되었습니다.");
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
//        Debug.Log($"{sName} 몬스터의 경험치는 {iExp}입니다.");
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

//private Monster Orc = new Monster();//변수는 항상 private로 동적할당을 받아야 사용 가능
//private Monster Gremlin = new Monster();//변수
//private Monster Dragon;

//void Start()
//{
//    //Orc.iExp = 20; iExp는 Moster의 것이므로 바로 접근이 불가능
//    Orc.functionGetExp();//public 함수를 통해 접근 가능 * Orc = 10
//    Orc.functionSetExp(100);//오크의 경험치가 100으로 수정 * Orc = 100
//    Gremlin = Orc;//Gremlin에는 Orc의 주소가 들어감 * Gremlin = Orc
//    Gremlin.functionGetExp();//그렘린의 경험치 확인 * Gremlin = Orc = 100

//    Orc.functionSetExp(1);//오크의 경험치가 1로 수정 * Orc = 1
//    Gremlin.functionGetExp();//그렘린의 경험치 확인 * Gremlin = Orc = 1
//                             //콜바이레퍼런스는 주소속의 데이터가 변경되면 같은 주소에 데이터가 입력되므로
//                             //모든 같은주소를 호출하는 데이터는 같은 데이터를 출력함
//    Dragon = Orc;
//    Dragon.functionGetExp();// Dragon = Orc = 1
//    Orc.functionSetExp(20);// Orc = 20
//    Dragon.functionGetExp();// Dragon = Orc = 20
//}




//==============================================================================상속
//public class Monster//콜바이레퍼런스 = 동적할당, 클래스를 정의, private로 만들면 다른 스크립트에서 사용하지 못함
//{
//    public Monster(string _sName, float _fHp)//[생성자(Constructor)는 객체가 생성될때 자동으로 호출되는 함수로 객체의 초기화를 위해 사용]
//    {
//        sName = _sName;
//        fHp = _fHp;
//    }
//    protected string sName;
//    private float fHp;
//
//    public virtual void ShowData()//부모에게는 virtual
//    {
//        Debug.Log($"이름:{sName}");
//        Debug.Log($"체력:{fHp}");
//    }
//}
//
//public class HighMonster : Monster//자식 : 부모 [오버라이딩 부모안에 들어있는 함수를 기능을 달리해서 사용함]
//{
//    protected int iRootNum;
//
//    public HighMonster(string _sName, float _fHp, int _root) : base(_sName, _fHp)
//    {
//        iRootNum = _root;
//    }
//    public override void ShowData()//자식에게는 override
//    {
//        //base.sName = "";//protected로 사용가능
//        //base.fHp; 사용불가
//        base.ShowData();//[base. 부모의 기능을 사용할때 씀]
//        Debug.Log($"루팅:{iRootNum}");
//    }
//    //부모에게 상속된 기능 외 매개변수를 추가로 받아야 할 경우는 오버로딩하면 된다
//    public void ShowData(int _exp)//오버로딩
//    {
//        Debug.Log("재정의된 함수입니다.");
//    }
//}
//
//
//void Start()
//{
//    Monster Orc = new Monster("오크", 100);
//    Orc.ShowData();
//
//    HighMonster spOrc = new HighMonster("특수오크", 200, 177);
//    spOrc.ShowData();
//}




//==============================================================================List
//int count = ListRemainMonster.Count;//List의 Length는 A.Count로 구함
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
//    //    //Monster monster = new Monster();//콜바이레퍼런스라 생성될 때 마다 주소가 다르게 생성
//    //    //ListRemainMonster.Add(monster);
//    //    //monster.HHp = iNum;
//    //    //------------------------------------------------------------위와같음
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
//사전(Key), 사전은 사전이다(value)
//Dictionary<string, string> dicDict = new Dictionary<string, string>();

//string userNumbering = "00000000";
//PlayerData data = new PlayerData();

/*
#region 딕셔너리일때
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
////List.Insert ! Dictionary.Insert는 배열의 구조가 아니어서 사용할 수 없다
////List와 array는 주소를 데이터 주소번호(0~)를 저장해서 순서대로 표현
////List.Clear

//dicDict.Add("사전", "사전은 사전이다");
//dicDict.Remove("사전");
//dicDict.Clear();

//string value = dicDict["사전"];



//==============================================================================배열 List 문제
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
//    //1번 배열
//    //클래스배열을 만드세요
//    //공간5개 0번의 클래스부터 모두 userName이 자신의 번호가 이름이 되도록 만들어주세요
//    //0 유저네임은 "user0"
//    //1 유저네임은 "user1"
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

//==============================================================================제네릭 Generic
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
//    public T _genericValue;//자료형이 선언되지 않은 데이터
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



//==============================================================================매개변수 한정자 out
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
//        Debug.Log("해당 유저가 존재 했습니다.");
//    }
//    else
//    {
//        Debug.Log("해당 유저가 존재하지 않습니다.");
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
//    _c = default;//int = 0, float = 0.0f, string = "" class = null을 제공
//    _c = _a * _b;
//    return _a + _b;
//}



//==============================================================================매개변수 한정자 ref
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



//==============================================================================매개변수 한정자 in
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
//    int temp = _a;//읽기전용이라서 변경이 전달은 가능하나 변경은 불가능하다
//                  //error//_a = _b;
//                  //error//_b = temp;
//}



//==============================================================================매개변수 한정자 =
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




//==============================================================================class배열 Insert만들기
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



//==============================================================================class배열 Remove만들기
//public void Remove(int _index)
//{
//    int count = user.Length;
//    Debug.Log($"======================<color=aqua>Remove</color>({_index})");
//    user[_index] = new PlayerData(null);
//    //null+1을 앞으로
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

