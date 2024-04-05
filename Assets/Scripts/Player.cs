using UnityEngine;

public class Player : MonoBehaviour
{
    public VariableJoystick joy;  // 조이스틱 클래스의 메소드
    public float speed;  // 속도 변수
    Rigidbody rigid;  // Rigidbody 클래스의 메소드
    // Animator anim;
    Vector3 moveVec;  // 3차원 벡터 변수

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        // anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float x = joy.Horizontal;
        float z = joy.Vertical;  // 조이스틱의 가로와 세로 방향 좌표는 각각 x축과 z축 움직임으로 치환

        moveVec = new Vector3(x, 0, z) * speed * Time.deltaTime;  // 이동 벡터에 반드시 속도와 deltaTime을 곱해줘야 함
        rigid.MovePosition(rigid.position + moveVec);  // Rigidbody의 원래 포지션에서 이동 벡터를 더해서 움직임
        if (moveVec.sqrMagnitude == 0) return;  // sqrMagnitude는 벡터의 각 값의 제곱의 합, 이동 벡터가 0이면 리턴

        Quaternion dirQuat = Quaternion.LookRotation(moveVec);
        Quaternion moveQuat = Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
        rigid.MoveRotation(moveQuat);  // Quaternion 클래스는 3차원 벡터값과 회전값을 묶은 4차원 튜플, 매끄러운 회전 구현
    }

    private void LateUpdate()
    {
        // anim.SetFloat("Move", moveVec.sqrMagnitude);
    }
}
