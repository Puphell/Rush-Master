using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float knockbackDistance = 5f; // -Z yönünde geri hareket mesafesi

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                Vector3 knockbackPosition = collision.transform.position - new Vector3(0, 0, knockbackDistance);
                player.transform.position = knockbackPosition;
                player.Stun(0.5f); // Oyuncuyu 1 saniye sersemlet
            }
        }
    }
}