using UnityEngine;

public class Player : MonoBehaviour
{
    public VariableJoystick joy;  // ���̽�ƽ Ŭ������ �޼ҵ�
    public float speed;  // �ӵ� ����
    Rigidbody rigid;  // Rigidbody Ŭ������ �޼ҵ�
    // Animator anim;
    Vector3 moveVec;  // 3���� ���� ����

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        // anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float x = joy.Horizontal;
        float z = joy.Vertical;  // ���̽�ƽ�� ���ο� ���� ���� ��ǥ�� ���� x��� z�� ���������� ġȯ

        moveVec = new Vector3(x, 0, z) * speed * Time.deltaTime;  // �̵� ���Ϳ� �ݵ�� �ӵ��� deltaTime�� ������� ��
        rigid.MovePosition(rigid.position + moveVec);  // Rigidbody�� ���� �����ǿ��� �̵� ���͸� ���ؼ� ������
        if (moveVec.sqrMagnitude == 0) return;  // sqrMagnitude�� ������ �� ���� ������ ��, �̵� ���Ͱ� 0�̸� ����

        Quaternion dirQuat = Quaternion.LookRotation(moveVec);
        Quaternion moveQuat = Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
        rigid.MoveRotation(moveQuat);  // Quaternion Ŭ������ 3���� ���Ͱ��� ȸ������ ���� 4���� Ʃ��, �Ų����� ȸ�� ����
    }

    private void LateUpdate()
    {
        // anim.SetFloat("Move", moveVec.sqrMagnitude);
    }
}
