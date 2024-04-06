using UnityEngine;

public class Player : MonoBehaviour
{
    public VariableJoystick joy;  // 조이스틱 클래스의 메소드
    public float speed;  // 속도 변수
    Rigidbody rigid;  // Rigidbody 클래스의 메소드
    Animator anim;  // Animator 클래스의 메소드
    Vector3 moveVec;  // 3차원 벡터 변수
    RunButton runButton;  // RunButton 클래스의 메소드
    [SerializeField] Transform cam;  // Transform 클래스의 메소드 (카메라)

    private void Start()
    {
        Application.targetFrameRate = 60;  // 목표 FPS       
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();  // 컴포넌트를 가져옴 (anim은 자식 오브젝트에 넣었기 때문에 GetComponentInChildren을 사용해야 함)
        runButton = GameObject.Find("Canvas").GetComponent<RunButton>();  // Rigidbody와 Animator는 이 스크립트를 포함하는 오브젝트 내에 있기 때문에 GetComponent를 그대로 사용하면 되지만, GetButton은 그렇지 않기 때문에 반드시 GetButton이 포함된 오브젝트의 이름을 찾아야 함!
    }

    private void FixedUpdate()
    {
        anim.SetBool("isWalk", moveVec != Vector3.zero);  // 애니메이션은 걷기가 기본값, 이동 벡터가 0이 아니라면 걷기
        anim.SetBool("isRun", runButton.click);  // runButton을 누르고 있는 동안 달리기

        float x = joy.Horizontal; float z = joy.Vertical;  // 조이스틱의 가로와 세로 방향 좌표는 각각 x축과 z축 움직임으로 치환
        Vector3 camForward = cam.forward; Vector3 camRight = cam.right;  // 카메라의 상대적인 방향벡터
        camForward.y = 0; camRight.y = 0;  // 카메라의 y축 방행벡터는 사용하지 않을 것이므로 0으로 설정

        Vector3 forwardRelative = z * camForward.normalized; Vector3 rightRelative = x * camRight;  // 카메라의 forward를 z축, right를 x축의 움직임과 곱함 (카메라가 높은 각도에 있으면, 즉 카메라의 y축 절댓값이 크면 camForward의 값이 작아지고 플레이어의 조이스틱 세로 이동이 느려지는 현상 발생, 이를 해결하기 위해 camForward에 벡터의 값이 항상 1이 되게 하는 normalized 메소드를 사용하면 camForward = (0, -1, 0)이 아닌 이상 문제가 발생하지 않음)
        Vector3 moveDir = forwardRelative + rightRelative;  // 조이스틱을 움직일 때 플레이어가 실제로 움직일 방향은 카메라의 상대적인 방향에 의해 결정

        moveVec = new Vector3(moveDir.x, 0, moveDir.z) * speed * Time.deltaTime * (runButton.click ? 2 : 1);  // 이동 벡터에 반드시 속도와 deltaTime을 곱해줘야 함
        rigid.MovePosition(rigid.position + moveVec);  // Rigidbody의 원래 포지션에서 이동 벡터를 더해서 움직임
        if (moveVec.sqrMagnitude == 0) return;  // sqrMagnitude는 벡터의 각 값의 제곱의 합, 이동 벡터가 0이면 리턴

        Quaternion dirQuat = Quaternion.LookRotation(moveVec);
        Quaternion moveQuat = Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
        rigid.MoveRotation(moveQuat);  // Quaternion 클래스는 3차원 벡터값과 회전값을 묶은 4차원 튜플, 매끄러운 회전 구현
    }
}
