using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject[] shooter; // ���� ���� ������Ʈ�� �޴� ���� ��
    [SerializeField] private GameObject player1P; // �÷��̾�1 �¸� �ؽ�Ʈ ������Ʈ�� �޴� ���� ��
    [SerializeField] private GameObject player2P; // �÷��̾�2 �¸� �ؽ�Ʈ ������Ʈ�� �޴� ���� ��
    [SerializeField] private GameObject playerDraw; // �÷��̾�Draw �ؽ�Ʈ ������Ʈ�� �޴� ���� ��
    [SerializeField] private GameObject player2PScore; // �÷��̾�2 ���ھ� ������Ʈ�� �޴� ���� ��
    [SerializeField] private bool isDrawChecked; // üũ ǥ�� Ȱ��ȭ ���ο� ���� �����ϴ� �޼ҵ尡 �޶����� ����
    [SerializeField] private bool isGameOverChecked; // üũ ǥ�� Ȱ��ȭ ���ο� ���� �����ϴ� �޼ҵ尡 �޶����� ����

    private void Start() // ��ŸƮ �޼ҵ�
    {
        OnGameOverControllerInit(isDrawChecked); // ���� ���� ��Ʈ�ѷ� ���� �޼ҵ�
    }

    private void OnGameOverControllerInit(bool _isChecked) // ���� ���� ��Ʈ�ѷ� ���� �޼ҵ�
    {
        if (_isChecked == false)
        {
            return;
        }

        player1P.SetActive(false); // player1P �ؽ�Ʈ�� Ȱ��ȭ ���¸� �������� �Ѵ�
        player2P.SetActive(false); // player2P �ؽ�Ʈ�� Ȱ��ȭ ���¸� �������� �Ѵ�
        playerDraw.SetActive(false); // playerDraw �ؽ�Ʈ�� Ȱ��ȭ ���¸� �������� �Ѵ�

        if (PlayerPrefs.GetInt("Score2P") == 0)
        {
            player2PScore.SetActive(false);
            return;
        }

        if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("Score2P")) // 1P�� ������ 2P�� �������� �� Ŭ ��
        {
            player1P.SetActive(true); //player1P �ؽ�Ʈ�� Ȱ��ȭ ���¸� ������ �Ѵ�
        }
        else if (PlayerPrefs.GetInt("Score") < PlayerPrefs.GetInt("Score2P")) // 1P�� ������ 2P�� �������� �� ���� ��
        {
            player2P.SetActive(true); //player2P �ؽ�Ʈ�� Ȱ��ȭ ���¸� ������ �Ѵ�
        }
        else
        {
            playerDraw.SetActive(true); //playerDraw �ؽ�Ʈ�� Ȱ��ȭ ���¸� ������ �Ѵ�
        }
    }

    private void Update() // ������Ʈ �޼ҵ�
    {
        OnCheckGameOver(isGameOverChecked); // ���� ������ Ȯ���ϴ� �޼ҵ�
    }

    private void OnCheckGameOver(bool _isChecked) // ���� ������ Ȯ���ϴ� �޼ҵ�
    {
        if (_isChecked == false)
        {
            return;
        }

        bool _isCleared = true; // ���� ������ Ȯ���ϴ� �� or ������ �Ҹ��� ���� ������ �����Ѵ�

        for (int i = 0; i < shooter.Length; ++i) // ���� ���� ������Ʈ�� ���� ��ŭ �ݺ��ϴ� �ݺ���
        {
            _isCleared &= !shooter[i].activeSelf; // ���� ���� �Ҹ��� ���� ��� ���� ��쿡 ������ ��ȯ�Ѵ�
        }

        if (_isCleared) // ���� ���� �Ҹ��� ���� ���� ���
        {
            SceneManager.LoadScene("GameOver"); // ���� ���� ���� �ε��Ѵ�
        }
    }
}
