using System.Collections;
using UnityEngine;

public class ShooterAnimationController : MonoBehaviour
{
    [SerializeField] private GameObject shooter;
    [SerializeField] private float animationDelay = 2f;
    [SerializeField] private float shooterInitDelay = 1.4f;

    private float fixShooterYValue = 0.09f;
    private Vector3 value = Vector3.zero;
    private bool isLanded;

    private void Start()
    {
        OnShooterAnimationInit(true);
    }

    private void OnShooterAnimationInit(bool _isActive)
    {
        shooter.SetActive(!_isActive);
        gameObject.SetActive(_isActive);
        SoundManager.Play.PlayEffect("ShooterTakeoff");
    }

    private void Update()
    {
        OnAnimationTranslate();
    }

    private void OnAnimationTranslate()
    {
        if (!isLanded && transform.position == shooter.transform.position)
        {
            isLanded = true;
            StartCoroutine(OnShooterInitDelay(false));
        }

        if (transform.position.y <= shooter.transform.position.y + fixShooterYValue)
        {
            transform.position = shooter.transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, shooter.transform.rotation, animationDelay * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(gameObject.transform.position, shooter.transform.position, ref value, animationDelay);
        }
    }

    private IEnumerator OnShooterInitDelay(bool _isActive)
    {
        SoundManager.Play.PlayEffect("ShooterLand");

        yield return new WaitForSeconds(shooterInitDelay);

        shooter.SetActive(!_isActive);
        gameObject.SetActive(_isActive);
    }
}
