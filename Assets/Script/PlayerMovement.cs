using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sideMoveSpeed = 2f; // Yanlara hareket hýzý
    private float originalMoveSpeed;
    private bool isStunned = false; // Flag to check if the player is stunned
    private const float maxSpeed = 100f; // Maksimum hýz

    private Rigidbody rb;

    public EnemyController enemy;
    public GameManager manager;
    public Finish finish;

    private void Start()
    {
        originalMoveSpeed = moveSpeed;
        moveSpeed = PlayerPrefs.GetFloat("PlayerSpeed", moveSpeed); // Hýzý yükle
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void Update()
    {
        if (!isStunned) // Only move forward if the player is not stunned
        {
            MoveForward();
        }
        HandleTouchInput(); // Always handle touch input for side movement
        HandleKeyboardInput(); // Handle keyboard input for side movementd
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.x < Screen.width / 2)
            {
                MoveLeft();
            }
            else if (touch.position.x > Screen.width / 2)
            {
                MoveRight();
            }
        }
    }

    void HandleKeyboardInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
    }

    void MoveLeft()
    {
        transform.Translate(Vector3.left * sideMoveSpeed * Time.deltaTime);
    }

    void MoveRight()
    {
        transform.Translate(Vector3.right * sideMoveSpeed * Time.deltaTime);
    }

    public void BoostSpeed(float amount)
    {
        moveSpeed = Mathf.Min(moveSpeed + amount, maxSpeed); // Hýzý maksimum deðeri aþmamasý için kontrol et
    }

    public void ReduceSpeed(float amount)
    {
        moveSpeed = Mathf.Max(moveSpeed - amount, 0); // Hýzý sýfýrýn altýna düþmemesi için kontrol et
    }

    public void RestoreOriginalSpeed()
    {
        moveSpeed = originalMoveSpeed;
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
        isStunned = true; // Set the stun flag
        yield return new WaitForSeconds(duration);
        isStunned = false; // Clear the stun flag after the stun duration
    }
}