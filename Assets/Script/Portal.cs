using UnityEngine;
using TMPro;
using System.Collections;

public class PortalEffect : MonoBehaviour
{
    public enum PortalType { Good, Bad }
    public PortalType portalType;
    public float effectDuration = 5f; // Etki süresi
    public float speedBoostAmount = 15f; // Hýz artýþý miktarý
    public float speedReductionAmount = 15f; // Hýz azaltma miktarý
    public float reverseDirectionDuration = 3f; // Yön deðiþtirme süresi
    public float stunDuration = 2f; // Sersemletme süresi

    private TextMeshPro textMeshPro;
    private string[] goodEffects = { "Boost Speed", "Reverse Enemy", "Stun Enemy" };
    private string[] badEffects = { "Reduce Speed", "Reverse Player", "Stun Player" };

    void Start()
    {
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        AssignRandomEffect();
    }

    void AssignRandomEffect()
    {
        string effect = "";
        if (portalType == PortalType.Good)
        {
            int randomValue = Random.Range(0, 100);
            if (randomValue < 5)
            {
                int randomEffect = Random.Range(1, goodEffects.Length); // "Boost Speed" hariç diðerleri
                effect = goodEffects[randomEffect];
            }
            else
            {
                effect = "Boost Speed";
            }
        }
        else if (portalType == PortalType.Bad)
        {
            int randomValue = Random.Range(0, 100);
            if (randomValue < 5)
            {
                int randomEffect = Random.Range(1, badEffects.Length); // "Reduce Speed" hariç diðerleri
                effect = badEffects[randomEffect];
            }
            else
            {
                effect = "Reduce Speed";
            }
        }

        // TextMeshPro'ya seçilen etkileri yaz
        if (textMeshPro != null)
        {
            textMeshPro.text = effect;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                ApplyEffectToPlayer(playerController, textMeshPro.text);
            }
        }
    }

    private void ApplyEffectToPlayer(PlayerController player, string effect)
    {
        switch (effect)
        {
            case "Boost Speed":
                player.BoostSpeed(speedBoostAmount);
                break;
            case "Reverse Enemy":
                StartCoroutine(ReverseRotation(player.enemy.transform, reverseDirectionDuration));
                break;
            case "Stun Enemy":
                player.enemy.Stun(stunDuration);
                break;
            case "Reduce Speed":
                player.ReduceSpeed(speedReductionAmount);
                break;
            case "Reverse Player":
                StartCoroutine(ReverseRotation(player.transform, reverseDirectionDuration));
                break;
            case "Stun Player":
                player.Stun(stunDuration);
                break;
        }
    }

    private void ApplyEffectToEnemy(EnemyController enemy, string effect)
    {
        switch (effect)
        {
            case "Boost Speed":
                enemy.BoostSpeed(speedBoostAmount);
                break;
            case "Reverse Enemy":
                StartCoroutine(ReverseRotation(enemy.player.transform, reverseDirectionDuration));
                break;
            case "Stun Enemy":
                enemy.player.Stun(stunDuration);
                break;
            case "Reduce Speed":
                enemy.ReduceSpeed(speedReductionAmount);
                break;
            case "Reverse Player":
                StartCoroutine(ReverseRotation(enemy.transform, reverseDirectionDuration));
                break;
            case "Stun Player":
                enemy.Stun(stunDuration);
                break;
        }
    }

    private IEnumerator ReverseRotation(Transform target, float duration)
    {
        float originalYRotation = target.rotation.eulerAngles.y;
        target.rotation = Quaternion.Euler(target.rotation.eulerAngles.x, originalYRotation + 180f, target.rotation.eulerAngles.z);

        yield return new WaitForSeconds(duration);

        target.rotation = Quaternion.Euler(target.rotation.eulerAngles.x, originalYRotation, target.rotation.eulerAngles.z);
    }
}