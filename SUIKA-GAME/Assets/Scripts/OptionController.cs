using UnityEngine;

public class OptionController : MonoBehaviour
{
    [SerializeField] private GameObject[] optionInterface; // �ɼ� ���� ������Ʈ�� �迭�� ������ ���� ������Ʈ ���� ��

    private bool isStated; // �簳 �Ǵ� �Ͻ����� ���¸� �����ϴ� ���� �� or ������ �Ҹ��� ��

    private void Update() // ������Ʈ �޼ҵ�
    {
        OnKeyInput(); // Ű���� ��ư�� �Է¹޴� �޼ҵ�
    }

    private void OnKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC ��ư�� ������ ��
        {
            if (isStated) // isStated�� �Ҹ��� ���� ���̶��
            {
                OnResume(isStated); // ������ ���� ������ isStated�� �ݴ밪�� �����Ѵ�
                isStated = false; // isStated �Ҹ��� ���� �������� �����Ѵ�
            }
            else // ���� �ݴ���
            {
                OnPause(isStated); // ������ ���� ������ isStated�� �����Ѵ�
                isStated = true; // isStated �Ҹ��� ���� ������ �����Ѵ�
            }
        }
    }

    private void OnResume(bool _isActive) // �簳 �޼ҵ�
    {
        isStated = !_isActive; // ���� ���� ���� ���� �ݴ� ������ �����Ѵ�

        Time.timeScale = 1f; // �簳 ��Ű�� ����

        for (int i = 0; i < optionInterface.Length; ++i) // �ɼ� ���� ������Ʈ�� ũ�⸸ŭ �ݺ��ϴ� �ݺ���
        {
            optionInterface[i].SetActive(!_isActive); // Ȱ��ȭ ���¸� ���� ������ �����Ѵ�
        }
    }

    private void OnPause(bool _isActive) // �Ͻ����� �޼ҵ�
    {
        isStated = !_isActive; // ���� ���� ���ڰ��� �ݴ� ������ �����Ѵ�

        Time.timeScale = 0f; // �Ͻ����� ��Ű�� ����

        for (int i = 0; i < optionInterface.Length; ++i) // �ɼ� ���� ������Ʈ�� ũ�⸸ŭ �ݺ��ϴ� �ݺ���
        {
            optionInterface[i].SetActive(!_isActive); // Ȱ��ȭ ���¸� ���� ������ �����Ѵ�
        }
    }
}
