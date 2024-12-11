using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputReader input;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject swordCollision;
    [SerializeField] private GameObject playerHand;
    [SerializeField] private GameObject SwordTrail;

    private Animator _swordAnimator;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject activeWeapon;

    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer weaponSpriteRenderer;

    private bool canAttack = true;
    private bool isAttacking = false;

    private void OnEnable()
    {
        input.AttackEvent += OnAttack;
    }

    private void OnDisable()
    {
        input.AttackEvent -= OnAttack;
    }

    private void OnAttack()
    {
        if (canAttack && !isAttacking)
        {
            Attack();
        }
    }

    private void Awake()
    {

        _swordAnimator = GetComponent<Animator>();
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        weaponSpriteRenderer = activeWeapon.GetComponentInChildren<SpriteRenderer>();
        HideWeapon();
        swordCollision.SetActive(false);
        playerHand.SetActive(false);
        SwordTrail.SetActive(false);
    }

    private void Update()
    {
        if (!isAttacking)
        {
            MouseFollowWithOffset();
        }
        AdjustWeaponSorting();
    }

    private void Attack()
    {
        canAttack = false;
        isAttacking = true;
        ShowWeapon();
        swordCollision.SetActive(true);
        playerHand.SetActive(true);
        SwordTrail.SetActive(true);

        _swordAnimator.SetTrigger("Attack");

        // Obtém a posição do mouse na tela e converte para o mundo
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0; // Garantir que a posição Z seja 0

        // Calcula a direção do ataque
        Vector3 direction = (mouseWorldPos - player.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Aponta a espada e a colisão para a direção do mouse
        activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        swordCollision.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Ajusta a orientação do personagem com base na direção do ataque
        playerAnimator.SetFloat("LastHorizontal", direction.x);
        playerAnimator.SetFloat("LastVertical", direction.y);

        if (direction.x > 0)
        {
            playerAnimator.gameObject.transform.eulerAngles = new Vector2(0f, 0f);
        }
        else if (direction.x < 0)
        {
            playerAnimator.gameObject.transform.eulerAngles = new Vector2(0f, 180f);
        }

        input.Disable();
        playerAnimator.SetTrigger("Attack");

        StartCoroutine(AttackCooldown(0.5f));
    }


    private IEnumerator AttackCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime); // Tempo de espera do cooldown
        canAttack = true;
        input.Enable();
        swordCollision.SetActive(false);
        playerHand.SetActive(false);
        SwordTrail.SetActive(false);
        HideWeapon();
        isAttacking = false; // Permite novos ataques
    }

    private void MouseFollowWithOffset()
    {
        if (isAttacking) return; // Evita que a espada rode durante o ataque

        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(player.transform.position);

        Vector3 direction = mousePos - playerScreenPoint;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle > 45 && angle <= 135)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 90);
            swordCollision.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (angle < -45 && angle >= -135)
        {

            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, -90);
            swordCollision.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (angle > -45 && angle <= 45)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            swordCollision.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 180, 0);
            swordCollision.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void AdjustWeaponSorting()
    {
        if (playerSpriteRenderer.flipY) // Assuming 'flipY' indicates the player is facing away
        {
            weaponSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder - 1; // Sword behind player
        }
        else
        {
            weaponSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder + 1; // Sword in front of player
        }
    }

    private void ShowWeapon()
    {
        weaponSpriteRenderer.enabled = true; // Make the weapon visible
    }

    private void HideWeapon()
    {
        weaponSpriteRenderer.enabled = false; // Make the weapon invisible
    }
}