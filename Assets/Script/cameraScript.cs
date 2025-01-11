using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player; // Player'ýn transform'u
    public Vector3 offset = new Vector3(-1f, 6.4f, -19.4f); // Kameranýn player'a göre pozisyon farký
    public float followSpeed = 10f; // Kameranýn takip hýzý

    void LateUpdate()
    {
        // Kameranýn yeni pozisyonunu hesapla
        Vector3 targetPosition = player.position + offset;

        // Kamerayý yeni pozisyona düzgün bir þekilde hareket ettir
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Kameranýn player'a bakmasýný saðla
        transform.LookAt(player);
    }
}
