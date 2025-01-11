using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float originalMoveSpeed;
    public float speedIncreaseAmount = 0.5f; // Her 5 saniyede bir artýrýlacak hýz miktarý
    public float speedIncreaseInterval = 5f; // Hýz artýþ aralýðý (saniye)
    private float speedIncreaseTimer;
    private const float maxSpeed = 100f; // Maksimum hýz

    public PlayerController player;
    public GameManager manager;
    public Finish finish;
    private void Start()
    {
        // Load the enemy speed from PlayerPrefs if it exists, otherwise use default value
        moveSpeed = PlayerPrefs.GetFloat("EnemySpeed", moveSpeed);
        originalMoveSpeed = moveSpeed;
        speedIncreaseTimer = speedIncreaseInterval; // Zamanlayýcýyý baþlat
    }

    void Update()
    {
        moveForward();
        HandleSpeedIncrease();
    }

    void moveForward()
    {
        // Düþman sadece ileriye doðru hareket eder
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void HandleSpeedIncrease()
    {
        speedIncreaseTimer -= Time.deltaTime;
        if (speedIncreaseTimer <= 0)
        {
            BoostSpeed(speedIncreaseAmount);
            speedIncreaseTimer = speedIncreaseInterval; // Zamanlayýcýyý sýfýrla
        }
    }

    public void BoostSpeed(float amount)
    {
        moveSpeed = Mathf.Min(moveSpeed + amount, maxSpeed); // Hýzý maksimum deðeri aþmamasý için kontrol et
    }

    public void ReduceSpeed(float amount)
    {
        moveSpeed = Mathf.Max(moveSpeed - amount, 0); // Hýzý sýfýrýn altýna düþmemesi için kontrol et
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