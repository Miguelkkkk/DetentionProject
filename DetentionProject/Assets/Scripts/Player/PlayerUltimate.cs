using System.Collections;
using UnityEngine;

public class PlayerUltimate : MonoBehaviour
{
    [Header("Fireball Settings")]
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float fireballSpeed = 10f;
    public float cooldownTime = 5f;
    public float fadeoutDuration = 2f;

    private bool canCast = true;

    [Header("Input")]
    [SerializeField] private PlayerInputReader input;

    private void OnEnable()
    {
        input.UltimateEvent += OnUltimate;
    }

    private void OnDisable()
    {
        input.UltimateEvent -= OnUltimate;
    }

    private void OnUltimate()
    {
        if (canCast)
        {
            StartCoroutine(CastFireball());
        }
    }

    IEnumerator CastFireball()
    {
        canCast = false;

        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = (mousePosition - fireballSpawnPoint.position);
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        fireball.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            StartCoroutine(FadeOutVelocity(rb, direction));
        }

        yield return new WaitForSeconds(cooldownTime);
        canCast = true;
    }

    IEnumerator FadeOutVelocity(Rigidbody2D rb, Vector2 initialDirection)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeoutDuration)
        {
            float t = elapsedTime / fadeoutDuration;
            rb.velocity = initialDirection * Mathf.Lerp(fireballSpeed, 0f, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rb.velocity = Vector2.zero; 
    }
}