using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float originalMoveSpeed;
    public float speedIncreaseAmount = 0.5f; // Her 5 saniyede bir art�r�lacak h�z miktar�
    public float speedIncreaseInterval = 5f; // H�z art�� aral��� (saniye)
    private float speedIncreaseTimer;
    private const float maxSpeed = 100f; // Maksimum h�z

    public PlayerController player;
    public GameManager manager;
    public Finish finish;
    private void Start()
    {
        // Load the enemy speed from PlayerPrefs if it exists, otherwise use default value
        moveSpeed = PlayerPrefs.GetFloat("EnemySpeed", moveSpeed);
        originalMoveSpeed = moveSpeed;
        speedIncreaseTimer = speedIncreaseInterval; // Zamanlay�c�y� ba�lat
    }

    void Update()
    {
        moveForward();
        HandleSpeedIncrease();
    }

    void moveForward()
    {
        // D��man sadece ileriye do�ru hareket eder
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void HandleSpeedIncrease()
    {
        speedIncreaseTimer -= Time.deltaTime;
        if (speedIncreaseTimer <= 0)
        {
            BoostSpeed(speedIncreaseAmount);
            speedIncreaseTimer = speedIncreaseInterval; // Zamanlay�c�y� s�f�rla
        }
    }

    public void BoostSpeed(float amount)
    {
        moveSpeed = Mathf.Min(moveSpeed + amount, maxSpeed); // H�z� maksimum de�eri a�mamas� i�in kontrol et
    }

    public void ReduceSpeed(float amount)
    {
        moveSpeed = Mathf.Max(moveSpeed - amount, 0); // H�z� s�f�r�n alt�na d��memesi i�in kontrol et
    }

    public void Freeze(float duration)
    {
        StartCoroutine(FreezeCoroutine(duration));
    }

    public void ReverseDirection(float duration)
    {
        StartCoroutine(ReverseDirectionCoroutine(duration));
    }

    public void Stun(float duration)
    {
        StartCoroutine(StunCoroutine(duration));
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        moveSpeed = 0;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalMoveSpeed;
    }

    private IEnumerator ReverseDirectionCoroutine(float duration)
    {
        moveSpeed = -moveSpeed;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalMoveSpeed;
    }

    private IEnumerator StunCoroutine(float duration)
    {
        float originalSpeed = moveSpeed;
        moveSpeed = 0;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
    }
}