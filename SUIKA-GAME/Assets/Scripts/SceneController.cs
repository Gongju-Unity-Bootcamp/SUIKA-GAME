using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void OnStartClick() // ��ŸƮ ��ư�� ������ ���� �޼ҵ�
    {
        PlayerPrefs.SetInt("Score", 0); // ���ھ� Ű�� ���ھ� ������ ã�� �����Ѵ�
        SoundManager.Play.PlayEffect("MouseClick"); // ���� �̸����� ���� ���
        SceneManager.LoadScene("Ingame"); // �ΰ��� ���� �ε��Ѵ�
    }

    public void OnStart2PClick() // ��ŸƮ 2P ��ư�� ������ ���� �޼ҵ�
    {
        PlayerPrefs.SetInt("Score", 0); // ���ھ� Ű�� ���ھ� ������ ã�� �����Ѵ�
        PlayerPrefs.SetInt("Score2P", 0); // ���ھ� Ű�� ���ھ� ������ ã�� �����Ѵ�
        SoundManager.Play.PlayEffect("MouseClick"); // ���� �̸����� ���� ���
        SceneManager.LoadScene("Ingame2P"); // �ΰ��� ���� �ε��Ѵ�
    }

    public void OnOptionClick() // �ɼ� ��ư�� ������ ���� �޼ҵ�
    {
        PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name);
        // ���� ���� �̸��� ������ ������ �ɼǿ��� ���� �� �ǵ��ư��� �뵵�� �����Ѵ�
        SoundManager.Play.PlayEffect("MouseClick"); // ���� �̸����� ���� ���
        SceneManager.LoadScene("Option"); // �ɼ� ���� �ε��Ѵ�
    }

    public void OnExitClick() // ���� ��ư�� ������ ���� �޼ҵ�
    {
        SoundManager.Play.PlayEffect("MouseClick"); // ���� �̸����� ���� ���
        Application.Quit(); // ���� ���α׷��� ��� �����Ѵ�
    }

    public void OnBackClick() // ��� ��ư�� ������ ���� �޼ҵ�
    {
        SoundManager.Play.PlayEffect("MouseClick"); // ���� �̸����� ���� ���
        SceneManager.LoadScene(PlayerPrefs.GetString("SceneName"));
        // ���� ���� �̸��� ������ ������ �ɼǿ��� ���� �� �ǵ��ư��� �뵵�� �����Ѵ�
    }
}
