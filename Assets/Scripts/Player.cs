using UnityEngine;

public class Player : MonoBehaviour
{
    public VariableJoystick joy;  // ���̽�ƽ Ŭ������ �޼ҵ�
    public float speed;  // �ӵ� ����
    Rigidbody rigid;  // Rigidbody Ŭ������ �޼ҵ�
    Animator anim;  // Animator Ŭ������ �޼ҵ�
    Vector3 moveVec;  // 3���� ���� ����
    RunButton runButton;  // RunButton Ŭ������ �޼ҵ�

    private void Start()
    {
        Application.targetFrameRate = 60;  // ��ǥ FPS       
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();  // ������Ʈ�� ������ (anim�� �ڽ� ������Ʈ�� �־��� ������ GetComponentInChildren�� ����ؾ� ��)
        runButton = GameObject.Find("Canvas").GetComponent<RunButton>();  // Rigidbody�� Animator�� �� ��ũ��Ʈ�� �����ϴ� ������Ʈ ���� �ֱ� ������ GetComponent�� �״�� ����ϸ� ������, GetButton�� �׷��� �ʱ� ������ �ݵ�� GetButton�� ���Ե� ������Ʈ�� �̸��� ã�ƾ� ��!
    }

    private void FixedUpdate()
    {
        anim.SetBool("isWalk", moveVec != Vector3.zero);  // �ִϸ��̼��� �ȱⰡ �⺻��, �̵� ���Ͱ� 0�� �ƴ϶�� �ȱ�
        anim.SetBool("isRun", runButton.click);  // runButton�� ������ �ִ� ���� �޸���

        float x = joy.Horizontal;
        float z = joy.Vertical;  // ���̽�ƽ�� ���ο� ���� ���� ��ǥ�� ���� x��� z�� ���������� ġȯ

        moveVec = new Vector3(x, 0, z) * speed * Time.deltaTime * (runButton.click ? 2 : 1);  // �̵� ���Ϳ� �ݵ�� �ӵ��� deltaTime�� ������� ��
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
