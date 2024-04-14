using UnityEngine;

public class Player : MonoBehaviour
{
    public VariableJoystick joy;  // ���̽�ƽ Ŭ������ �޼ҵ�
    public float speed;  // �ӵ� ����
    public float jumpPower;  // ���� �� ����
    bool isJump = false;  // ���� ���� �Ҹ���

    Rigidbody rigid;  // Rigidbody Ŭ������ �޼ҵ�
    Animator anim;  // Animator Ŭ������ �޼ҵ�
    RunButton runButton;  // RunButton Ŭ������ �޼ҵ�
    JumpButton jumpButton;  // JumpButton Ŭ������ �޼ҵ�

    Vector3 moveVec;  // 3���� ���� ����
    [SerializeField] Transform cam;  // Transform Ŭ������ �޼ҵ� (ī�޶�)

    private void Start()
    {
        Application.targetFrameRate = 60;  // ��ǥ FPS
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();  // ������Ʈ�� ������ (anim�� �ڽ� ������Ʈ�� �־��� ������ GetComponentInChildren�� ����ؾ� ��)
        joy = GameObject.Find("Variable Joystick").GetComponent<VariableJoystick>();
        runButton = GameObject.Find("Canvas").GetComponent<RunButton>();
        jumpButton = GameObject.Find("Canvas").GetComponent<JumpButton>();  // Rigidbody�� Animator�� �� ��ũ��Ʈ�� �����ϴ� ������Ʈ ���� �ֱ� ������ GetComponent�� �״�� ����ϸ� ������, ��ư�� �׷��� �ʱ� ������ �ݵ�� ��ư�� ���Ե� ������Ʈ�� �̸��� ã�ƾ� ��!
    }

    private void Move()
    {
        anim.SetBool("isWalk", moveVec != Vector3.zero);  // �ִϸ��̼��� �ȱⰡ �⺻��, �̵� ���Ͱ� 0�� �ƴ϶�� �ȱ�
        anim.SetBool("isRun", runButton.click);  // runButton�� ������ �ִ� ���� �޸���

        float x = joy.Horizontal; float z = joy.Vertical;  // ���̽�ƽ�� ���ο� ���� ���� ��ǥ�� ���� x��� z�� ���������� ġȯ
        Vector3 camForward = cam.forward; Vector3 camRight = cam.right;  // ī�޶��� ������� ���⺤��
        camForward.y = 0; camRight.y = 0;  // ī�޶��� y�� ���຤�ʹ� ������� ���� ���̹Ƿ� 0���� ����

        Vector3 forwardRelative = z * camForward.normalized; Vector3 rightRelative = x * camRight;  // ī�޶��� forward�� z��, right�� x���� �����Ӱ� ���� (ī�޶� ���� ������ ������, �� ī�޶��� y�� ������ ũ�� camForward�� ���� �۾����� �÷��̾��� ���̽�ƽ ���� �̵��� �������� ���� �߻�, �̸� �ذ��ϱ� ���� camForward�� ������ ���� �׻� 1�� �ǰ� �ϴ� normalized �޼ҵ带 ����ϸ� camForward = (0, -1, 0)�� �ƴ� �̻� ������ �߻����� ����)
        Vector3 moveDir = forwardRelative + rightRelative;  // ���̽�ƽ�� ������ �� �÷��̾ ������ ������ ������ ī�޶��� ������� ���⿡ ���� ����

        moveVec = new Vector3(moveDir.x, 0, moveDir.z) * speed * Time.deltaTime * (runButton.click ? 2 : 1);  // �̵� ���Ϳ� �ݵ�� �ӵ��� deltaTime�� ������� ��
        rigid.MovePosition(rigid.position + moveVec);  // Rigidbody�� ���� �����ǿ��� �̵� ���͸� ���ؼ� ������
        if (moveVec.sqrMagnitude == 0) return;  // sqrMagnitude�� ������ �� ���� ������ ��, �̵� ���Ͱ� 0�̸� ����
    }

    private void Jump()
    {
        if (jumpButton.click && !isJump)  // runButton�� ������ �ְ� �̹� ���� ���� �ƴ� ��
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);  // Vector3.up == (0, 1, 0), Impulse�� Rigidbody�� ������ �����ؼ� �������� ���� ����
            isJump = true;  // ���� ���¸� true�� ��ȯ
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");  // isJump�� true�� �ǰ� doJump�� Ʈ���ŵ� �� ���� �ִϸ��̼� ����
        }
    }

    private void OnCollisionEnter(Collision collision)  // collision�� �� ������Ʈ�� �浹�ϴ� �ٸ� ������Ʈ
    {
        if (collision.gameObject.tag == "Floor")  // ������Ʈ Floor�� �±� Floor�� ����
        {
            isJump = false;  // ���� ���¸� false�� ��ȯ
            anim.SetBool("isJump", false);  // isJump�� false�� �� �� ���� �ִϸ��̼� ����
        }
    }

    private void Turn()
    {
        if (moveVec != Vector3.zero)
        {
            Quaternion dirQuat = Quaternion.LookRotation(moveVec);
            Quaternion moveQuat = Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
            rigid.MoveRotation(moveQuat);  // Quaternion Ŭ������ 3���� ���Ͱ��� ȸ������ ���� 4���� Ʃ��, �Ų����� ȸ�� ����
        }
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        Turn();  // �Լ� ����
    }
}
