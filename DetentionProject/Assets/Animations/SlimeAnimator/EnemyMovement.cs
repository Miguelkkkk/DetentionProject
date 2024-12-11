using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed; // Velocidade de movimento
    [SerializeField] private bool isInSlimeRange; // Verifica se o inimigo est� dentro do alcance de detec��o
    [SerializeField] private bool isPlayerSpotted; // Verifica se o inimigo viu o jogador
    [SerializeField] private Transform target; // Alvo do inimigo (provavelmente o jogador)

    private Rigidbody2D _enemyRigidBody; // Rigidbody do inimigo
    private Animator _enemyAnimator; // Animator do inimigo

    private void Awake()
    {
        // Inicializa os componentes
        _enemyAnimator = GetComponent<Animator>();
        _enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Verifica se o inimigo est� em alcance do slime (provavelmente � um detector de proximidade)
        isInSlimeRange = GetComponentInChildren<Interactor>().isInRange;

        // Se o inimigo detectou o jogador, ele come�a a persegui-lo
        if (isInSlimeRange)
        {
            isPlayerSpotted = true;
        }

        // Se o jogador foi visto, o inimigo se move, caso contr�rio, ele para
        if (isPlayerSpotted)
        {
            Move();
        }
        else
        {
            StopMove();
        }
    }

    // Fun��o que faz o inimigo se mover em dire��o ao alvo (jogador)
    private void Move()
    {
        // Verifica se o alvo (jogador) foi atribu�do corretamente
        if (target == null)
        {
            Debug.LogError("Target not assigned!");
            return;
        }

        // Calcula a dire��o do movimento em dire��o ao alvo
        Vector2 targetPos = target.position;
        Vector2 currentPos = transform.position;

        Vector2 direction = targetPos - currentPos;
        direction.Normalize(); // Normaliza a dire��o para garantir que o inimigo mova a uma velocidade constante

        // Aplica a velocidade e a dire��o ao Rigidbody2D
        _enemyRigidBody.velocity = direction * speed;

        // Atualiza a anima��o para indicar que o inimigo est� se movendo
        _enemyAnimator.SetBool("IsMoving", true);
    }

    // Fun��o que faz o inimigo parar
    private void StopMove()
    {
        _enemyRigidBody.velocity = Vector2.zero; // Para o movimento
        _enemyAnimator.SetBool("IsMoving", false); // Atualiza a anima��o para indicar que o inimigo parou
    }
}
