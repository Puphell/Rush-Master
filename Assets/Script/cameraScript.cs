using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player; // Player'�n transform'u
    public Vector3 offset = new Vector3(-1f, 6.4f, -19.4f); // Kameran�n player'a g�re pozisyon fark�
    public float followSpeed = 10f; // Kameran�n takip h�z�

    void LateUpdate()
    {
        // Kameran�n yeni pozisyonunu hesapla
        Vector3 targetPosition = player.position + offset;

        // Kameray� yeni pozisyona d�zg�n bir �ekilde hareket ettir
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Kameran�n player'a bakmas�n� sa�la
        transform.LookAt(player);
    }
}
