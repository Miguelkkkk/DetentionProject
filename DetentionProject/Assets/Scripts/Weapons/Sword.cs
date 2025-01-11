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
        StartCoroutine(ActivateCollision(0.15f));
        playerHand.SetActive(true);
        SwordTrail.SetActive(true);
        _swordAnimator.SetTrigger("Attack");

        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(player.transform.position);
        Vector3 direction = (mousePos - playerScreenPoint).normalized;

        if (direction.x > 0)
        {
            player.transform.eulerAngles = new Vector2(0f, 0f);
        }
        else if (direction.x < 0)
        {
            player.transform.eulerAngles = new Vector2(0f, 180f);
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        swordCollision.transform.rotation = Quaternion.Euler(0, 0, angle);

        playerAnimator.SetTrigger("Attack");
        input.Disable();
        StartCoroutine(AttackCooldown(0.3f));
    }
    private IEnumerator ActivateCollision(float duration)
    {
        swordCollision.SetActive(true);
        yield return new WaitForSeconds(duration);
        swordCollision.SetActive(false);
    }

    private IEnumerator AttackCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        canAttack = true;
        input.Enable();
        playerHand.SetActive(false);
        SwordTrail.SetActive(false);
        HideWeapon();
        isAttacking = false;
    }

    private void MouseFollowWithOffset()
    {
        if (isAttacking) return;

        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(player.transform.position);

        Vector3 direction = mousePos - playerScreenPoint;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void AdjustWeaponSorting()
    {
        if (playerSpriteRenderer.flipY)
        {
            weaponSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder - 1;
        }
        else
        {
            weaponSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder + 1;
        }
    }

    private void ShowWeapon()
    {
        weaponSpriteRenderer.enabled = true;
    }

    private void HideWeapon()
    {
        weaponSpriteRenderer.enabled = false;
    }
}
