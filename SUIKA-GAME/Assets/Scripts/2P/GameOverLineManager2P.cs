using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverLineManager2P : MonoBehaviour
{
    [SerializeField] private GameObject shooter; // ���� ���� ������Ʈ�� �����ϱ� ���� ��
    [SerializeField] private float endTime = 6f; // ������ �ð��� ���ϱ� ���� �Ǽ� ��
    [SerializeField] private float warnTime = 3f; // ��� �ð��� ���ϱ� ���� �Ǽ� ��
    [SerializeField] private float alertTime = 0.5f; // �˶� �ð��� ���ϱ� ���� �Ǽ� ��

    private SpriteRenderer spriteRenderer; // ��������Ʈ ������ ������Ʈ�� �������� ���� ���� ��
    private GameObject hitObject; // �浹�� ���� ������Ʈ�� �����ϱ� ���� ��
    private bool isChecked; // üũ Ȯ�� ���θ� ������ �Ҹ��� ��
    private float overTime = 0; // ��� �ð��� ���� �Ǽ� ��


    private void Start() // ��ŸƮ �޼ҵ�
    {
        OnGetSpriteRenderer(); // ��������Ʈ ������ ȣ�� �޼ҵ�
    }

    private void OnGetSpriteRenderer() // ��������Ʈ ������ ȣ�� �޼ҵ�
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // ��������Ʈ ������ ���� ���� ��������Ʈ ������ ������Ʈ�� �����´�
    }
    
    private void Update() // ������Ʈ �޼ҵ�
    {
        OnTimeSchedule(isChecked, endTime, warnTime, alertTime); // �ð� �����̸� ���� ���� ���� ������ ������ �޼ҵ�
    }

    private void OnTriggerStay2D(Collider2D _hit) // Ʈ���� üũ�� �� �浹ü�� �� ����� �� ����Ǵ� �޼ҵ�
    {
        if (_hit.transform.CompareTag("Planet") && isChecked == false) // �浹�� ������Ʈ�� �±װ� Planet�̰� üũ�� ���� �ȵǾ����� ��
        {
            isChecked = true; // üũ ǥ�ø� ���� �Ҹ��� ��
            hitObject = _hit.gameObject; // �浹 ������Ʈ�� hitObject ������ ����
        }
    }
    private void OnTriggerExit2D(Collider2D _hit) // Ʈ���� üũ�� �� �浹ü�� �� �������� �� ����Ǵ� �޼ҵ�
    {
        if (_hit.transform.CompareTag("Planet") && _hit.gameObject == hitObject) 
            // �浹�� ������Ʈ�� �±װ� Planet�̰� �浹 ������Ʈ�� hitObject�� ����Ǿ� �ִٸ�
        {
            isChecked = false; // üũ ǥ�ø� ���� �Ҹ��� ��
            hitObject = null; // hitObject ������ �η���� ����
        }
    }

    private void OnTimeSchedule(bool _isStabled, float _endTime, float _warnTime, float _alertTime) // �ð� ������ �Լ�
    {
        if (_isStabled == true) // _isStabled ���ڰ� true�� ��
        {
            overTime += Time.deltaTime; // overTime�� Time.deltaTime ���� ��� �����ش�
            
            if (overTime > _endTime) // overTime�� ������ �ð� ���� Ŭ ��
            {
                SoundManager.Play.StopSE("WarnAlert"); // ���� �̸����� ���� ����
                SoundManager.Play.PlayEffect("GameOver"); // ���� �̸����� ���� ���
                OnGameOver(); // ���ӿ��� �޼ҵ� ȣ��
            }
            else if (overTime > _warnTime) // overTime�� ��� �ð����� Ŭ ��
            {
                spriteRenderer.color = new Color(1f, 0f, 0f, 1f); // ���� ǥ�ü��� ���������� ����
                SoundManager.Play.PlayEffect("WarnAlert");
            }
            else if (overTime > _alertTime) // overTime�� �˶� �ð����� Ŭ ��
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // ���� ǥ�ü��� ������� �����Ͽ� ǥ��
            }
            else
            {
                spriteRenderer.color = new Color(0f, 0f, 0f, 0f); // �Ϲ� ���¿����� alpha(����?)���� �ٲ� ǥ������ �ʴ´�
            }
        }
        else
        {
            overTime = 0; // _isStable ���ڰ� false�� �� ��� �ð��� 0���� �ʱ�ȭ�Ѵ�
            spriteRenderer.color = new Color(0f, 0f, 0f, 0f); // �Ϲ� ���¿����� alpha(����?)���� �ٲ� ǥ������ �ʴ´�
            SoundManager.Play.StopSE("WarnAlert");
        }
    }

    private void OnGameOver() // ���ӿ��� �޼ҵ�
    {
        PlayerPrefs.SetInt("Score", ScoreManager.score); // ���ھ� ������ ���� ������ ���ھ� ���� ��
        PlayerPrefs.SetString("IsScored", "true"); // ���ھ� ������ ���� ������ �ѱ�� ���� �� or ���� �Ҹ��� ��
        ParticleManager.Show.ShowParticle("PlanetLevelUp", shooter.transform.position); // ��ƼŬ ����Ʈ �̸��� ��ƼŬ�� ����
        shooter.SetActive(false); // ���� ���� ������Ʈ�� �������� �����Ѵ�
    }
}
