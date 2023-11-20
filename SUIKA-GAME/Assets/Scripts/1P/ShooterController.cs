using System.Collections;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] private GameObject planet; // ���� ������Ʈ ���·� planet ���� �� ����
    [SerializeField] private Transform spawnPoint; // ������ ���� ����Ʈ�� �༺�� �߻��ϱ� ���� ������ �Ǵ� ��ǥ

    [SerializeField] private float shooterSpeed = 100f; // ���͸� ���콺�� �̵����� ��, �ӵ� �Ǽ� ��
    [SerializeField] private float shooterMinRange = 4.3f; // 2D ȯ�濡���� 2���� x, y ��ǥ �� x ��ǥ�� ���� ������ �̵� ���� ������ ���� ���� �ּ� �Ǽ� ��
    [SerializeField] private float shooterMaxRange = 4.3f; // 2D ȯ�濡���� 2���� x, y ��ǥ �� x ��ǥ�� ���� ������ �̵� ���� ������ ���� ���� �ִ� �Ǽ� ��

    [SerializeField] private float shootPower = 1500f; // ���ͷ� ��ƿø� �༺�� �� ũ�� �Ǽ� ��
    [SerializeField] private float shootDelay = 1.2f; // ���� �����̸� �����ϴ� �Ǽ� ��

    private int currentPlanetNumber; // ���� �� 1 ~ 3���� ������ ���� �༺�� �ε��� ���� ���� ���� ������ �����ϱ� ���� ��
    private float fixShooterSpeed = 10f; // ���콺 �ӵ� ������(���콺 �ӵ��� 0�� �Ǹ� ���Ͱ� ������ �� ���� ������ �����Ѵ�)
    private bool isShootDelayed; // ���� ������ ���θ� �����ϴ� �� or ������ �Ҹ��� ��
    private GameObject stuff; // ���� ������Ʈ�� ������ ���� ��

    private void Start() // ��ŸƮ �޼ҵ�
    {
        OnShooterInit(); // ���� ���� �޼ҵ�
    }

    private void OnShooterInit() // ���� ���� �޼ҵ�
    {
        stuff = new GameObject("Planet"); // �� ������Ʈ�� �̸��� Planet�̶� ������ �� stuff ���� ������Ʈ ������ �����Ѵ�
        shooterSpeed = fixShooterSpeed + shooterSpeed * PlayerPrefs.GetFloat("MouseSpeed");
        // ���콺 �ӵ� �������� �ɼǿ��� ������ ���콺 �� ����
    }

    private void Update() // ������Ʈ �޼ҵ�
    {
        OnMouseCursor(); // ���콺 Ŀ���� �Է¹޴� �޼ҵ�
        OnMouseInput(); // ���콺 ��ư�� �Է¹޴� �޼ҵ�
    }

    private void OnMouseCursor() // ���콺 Ŀ�� �Է��� ���� ���͸� �̵���Ű�� �޼ҵ�
    {
        float _horizontal = Input.GetAxis("Mouse X"); // Mouse X => (���� Ű���� �Է��̸�...) Horizontal, ���콺 Ŀ�� �Է� �Ǽ� ��
        float _speed = _horizontal * Time.deltaTime * shooterSpeed; // ������ �ӵ��� �ð� ������ �����̱� ���� ��

        transform.Translate(new Vector3(_speed, 0, 0)); // ���Ͱ� �¿�θ� �̵��Ͽ��� �ϱ� ������ x������ ���ǵ带 �����ϰ� y,z���� �����ǰ� 0���� ������

        if (transform.position.x > shooterMaxRange) // ������ x��ǥ���� shooterMaxRange���� Ŭ ��
        {
            transform.position = new Vector3(shooterMaxRange, transform.position.y, 0); // ������ ��ġ�� shooterRange��ǥ�� ������ y��ǥ z = 0 ���� �����Ѵ�
        }
        if (transform.position.x < shooterMinRange) // ������ -x��ǥ���� shooerMinRange���� ���� ��
        {
            transform.position = new Vector3(shooterMinRange, transform.position.y, 0); // ������ ��ġ�� shooterRange��ǥ�� ������ y��ǥ z = 0 ���� �����Ѵ� 
        }
    }

    private void OnMouseInput() // ���콺 ��,���ư �Է¹޴� �޼ҵ�
    {
        if (isShootDelayed == false && Input.GetKeyDown(KeyCode.Mouse0)) // ���콺 ���� ��ư�� ������ ��
        {
            OnSetPlanet(); // �༺ �ʱ� ���� �޼ҵ带 ȣ���Ѵ�
            StartCoroutine(OnShootDelay()); // �ڷ�ƾ�� �����Ѵ�
        }
    }

    private void OnSetPlanet() // �༺ �ʱ� ���� �޼ҵ�
    {
        GameObject _planet = Instantiate(planet, spawnPoint.position, spawnPoint.rotation);
        // ���� ������Ʈ _planet�� ��������Ʈ ��ġ���� ��������Ʈ ȸ������ ������ �� ��ü �����Ѵ�
        _planet.transform.parent = stuff.transform; // �༺�� ���� ������Ʈ ��ġ�� stuff�� �ű��
        PlanetController _planetController = _planet.GetComponent<PlanetController>();
        // _planet ���� ������Ʈ�� _planetController ������Ʈ�� �����´�
        _planet.tag = "Untagged"; // �༺�� �±״� Untagged(�±׾���)
        _planetController.is2Played = false; // 2P ���¸� �������� �����Ѵ�
        _planetController.OnPlanetInit(currentPlanetNumber);
        // ���ӿ�����Ʈ�� �༺�� �༺������ 1~3���� ��������Ʈ ��ǥ�� ��������Ʈ ��(ȸ����)���� �����Ѵ�

        OnShoot(_planet); // �༺ �߻� �޼ҵ�
    }

    private void OnShoot(GameObject _planet) // �༺ �߻� �޼ҵ�
    {
        SoundManager.Play.PlayEffect("PlanetShoot"); // ���� �̸����� ���� ���
        _planet.GetComponent<Rigidbody2D>().AddForce(_planet.transform.up * shootPower, ForceMode2D.Impulse);
        // �༺ ���� ������Ʈ���� ������ٵ�2D ������Ʈ�� �ҷ��� �� �༺�� �������� �������� shootPower ��ŭ�� ���� ���Ѵ�
        currentPlanetNumber = Random.Range(0, PlanetDatabase.planetIndex / 3);
        // 1~3���� �༺�� �����ϰ� �����ϱ� ���� 0���� �������� ���� ������ ���� ���� ��ȯ
        ShowPrevPlanetUI.OnSpriteChange(currentPlanetNumber); // ��������Ʈ ü���� ���¸� true�� ����
    }

    private IEnumerator OnShootDelay() // ���� ������ �ڷ�ƾ �޼ҵ�
    {
        isShootDelayed = true; // ���� ������ ���¸� �� or �������� ��Ÿ�� �Ҹ��� ������ ������ �����Ѵ�

        yield return new WaitForSeconds(shootDelay); // shootDelay�� ��ŭ ��� �ð��� ���´�

        isShootDelayed = false; // ���� ������ ���¸� �� or �������� ��Ÿ�� �Ҹ��� ������ �������� �����Ѵ�
    }
}
