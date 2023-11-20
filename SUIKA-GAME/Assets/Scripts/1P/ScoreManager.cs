using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText; // ���ھ� �ý�Ʈ ������ ���� �ؽ�Ʈ ���� ��
    public static Text scoreValue; // ���ھ� ������ �ý�Ʈ ������ ���� �ý�Ʈ ���� ��
    public static int score; // ���ھ� ���� ���� ��

    private void Start() // ��ŸƮ �޼ҵ�
    {
        OnScoreText(); // ���ھ� �ؽ�Ʈ ���� �޼ҵ�
    }

    private void OnScoreText() // ���ھ� �ؽ�Ʈ ���� �޼ҵ�
    {
        score = PlayerPrefs.GetInt("Score"); // ���ھ� ���� ������ ���� �������� �����´�
        scoreValue = GetComponent<Text>(); // ���ھ� �ؽ�Ʈ ���� �����ϱ� ���� Text ������Ʈ�� �����´�
        PlayerPrefs.SetString("IsScored", "true"); // ���� ������ ���ھ� ���� �� or ���� �Ҹ��� ���� true�� �����Ѵ�
        OnSetScore(score); // ���ھ �����Ѵ�
    }

    public static void OnSetScore(int _score) // ���ھ� ���� ���� �޼ҵ�
    {
        if (PlayerPrefs.GetString("IsScored").Equals("true")) // ���� ���� ������ ���ھ� ���� �� or ���� �Ҹ��� ���� true ���
        {
            score = _score; // �� ��ũ��Ʈ�� ���ھ� ���� ���� ���� _score ���� ������ �����Ѵ�
            scoreValue.text = score.ToString();
            // ���ھ� ������ �ؽ�Ʈ ������Ʈ�� ������ �� �ؽ�Ʈ�� ���ھ�� ���ڿ� ����ȯ ��Ų��
            PlayerPrefs.SetString("IsScored", "false");
            // ���� ������ ���ھ� ���� �� or ���� �Ҹ��� ���� false�� �ٲ� ���ھ� ������ �˸���
        }
        else if (PlayerPrefs.GetString("IsScored").Equals("false")) // ���� ���� ������ ���ھ� ���� �� or ���� �Ҹ��� ���� false ���
        {
            score += _score; // �� ��ũ��Ʈ�� ���ھ� ���� ���� ���� _score ���� ������ ���Ѵ�
            scoreValue.text = score.ToString();
            // ���ھ� ������ �ؽ�Ʈ ������Ʈ�� ������ �� �ؽ�Ʈ�� ���ھ�� ���ڿ� ����ȯ ��Ų��
        }
    }
}
