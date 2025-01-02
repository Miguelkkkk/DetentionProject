using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineFollowPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera; // Referência para a Cinemachine Virtual Camera

    [SerializeField] private string playerTag = "Player"; // Tag usada para identificar o Player

    void Awake()
    {
        // Obtém a referência para a Cinemachine Virtual Camera anexada a este GameObject
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (virtualCamera == null)
        {
            Debug.LogError("CinemachineVirtualCamera não encontrada! Certifique-se de que o script está anexado a uma Virtual Camera.");
            enabled = false; // Desativa o script para evitar erros futuros
        }
        TryFindAndSetPlayerAsTarget();
    }

    void TryFindAndSetPlayerAsTarget()
    {
        try
        {
            // Procura por um GameObject com a tag do Player
            GameObject player = GameObject.FindGameObjectWithTag(playerTag);

            if (player != null)
            {
                // Define o objeto do Player como o Follow Target da câmera
                virtualCamera.Follow = player.transform;
                virtualCamera.LookAt = player.transform;
                Debug.Log("Player encontrado e definido como alvo da câmera.");
            }
            else
            {
                Debug.LogWarning("Nenhum Player foi encontrado na cena com a tag '" + playerTag + "'.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Erro ao tentar definir o Player como alvo da câmera: {ex.Message}");
        }
    }

    void Update()
    {
        // Verifica constantemente se o Player foi adicionado à cena e ajusta o alvo, se necessário
        if (virtualCamera != null && virtualCamera.Follow == null)
        {
            TryFindAndSetPlayerAsTarget();
        }
    }
}


