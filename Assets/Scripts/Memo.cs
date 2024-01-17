/*
 * 1 moving 플레이어의 이동
 * 좌표로 1칸씩 이동, vector에 1씩 더하거나 빼서 상하좌우로 이동
 * 문제 키보드를 1번씩 입력하면 잘 작동했지만 꾹 누르면 한번에 너무빠른 속도로 이동했다
 * Time.Deltatime을 넣고 속도를 입력했는데 원하는 이동기능이 아니었다
 * 이동후 다음 이동간에 딜레이가 있어야겠다고 생각해서 딜레이를 넣었는데 원하는 이동기능이 만들어졌다
 * 추후 딜레이시간을 조절하는 이동속도버프를 만들어야겠다
 * 
 * 2 doAnimation 플레이어 이동에 따른 애니메이션 출력
 * Make Transition으로 연결 count=0이고 SetInteger로 1을 받으면 왼발출력 후 count++로
 * count=1이고 SetInteger로 왼발을0 오른발로1로 만들어 오른발을 출력하려고 했는데 생각처럼 안된다
 * 파라미터를 받는걸로는 잘 안되서 play로 해봤는데도 안된다
 * 
 * 3 turning
 * 애니메이션 동작 후 스프라이트변경에서 충돌이 생겨서 moving을 애니메이션이 아닌 sprite가 교체되는 형식으로 변경
 * turning도 sprite가 교체되는 형식으로 만들었고 gamemanager를 통해 모든 움직임 후 딜레이를 관리하도록 변경
 * 
 * 오브젝 엔에이블 인스펙터 셋엑티브
level
curExp
maxExp
curHp
maxHp
curMp
maxMp
curSp
maxSp
physicalDamage
magicDamage
strength
dexterity
Intelligence
agility
constitution
wisdom
crystal

 
 
 */
