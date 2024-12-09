using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    public static PlayerLoader Instance { get; private set; }

    [SerializeField] private GameEvent onPlayerDataLoadedEvent; 
    [SerializeField] private GameEvent onPlayerDataUpdatedEvent; 

    private PlayerData data = null;

    private void Awake()
    {
        // Configuração do Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Carregar os dados do jogador ao iniciar
        LoadGame();
    }

    public void SaveGame()
    {
        if (data != null)
        {
            JsonDataManager.Save(data, "PlayerData");
            Debug.Log("Jogo salvo com sucesso!");
        }
        else
        {
            Debug.LogWarning("Nenhum dado para salvar!");
        }
    }

    public void LoadGame()
    {
        if (JsonDataManager.SaveExists("PlayerData"))
        {
            data = JsonDataManager.Load<PlayerData>("PlayerData");
            Debug.Log("Dados carregados com sucesso!");
            onPlayerDataLoadedEvent?.Raise(data); // Dispara o evento de dados carregados
        }
        else
        {
            Debug.LogWarning("Nenhum arquivo de salvamento encontrado. Criando novos dados.");
            data = new PlayerData("Hero", 1, 100f);
            onPlayerDataLoadedEvent?.Raise(data); // Dispara o evento mesmo para novos dados
        }
    }

    public PlayerData GetPlayerData()
    {
        return data;
    }

    public void UpdatePlayerData(string playerName, int maxHealth, float maxStamina)
    {
        if (data != null)
        {
            data.playerName = playerName;
            data.maxHealth = maxHealth;
            data.maxStamina = maxStamina;
            onPlayerDataUpdatedEvent?.Raise(data); // Dispara o evento de dados atualizados
        }
    }
}
