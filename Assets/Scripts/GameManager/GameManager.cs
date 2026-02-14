using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private ParticleSystem globalParticles;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (player == null)
            player = FindFirstObjectByType<Player>();
    }

    private void Start()
    {
        Invoke(nameof(LoadGame), 0.1f);
    }

    public void RestartGame()
    {
        SaveSystem.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SaveGame()
    {
        if (player == null) 
        {
            player = FindFirstObjectByType<Player>();
          
        }

        if (player == null) return;

        PlayerData data = player.GetSaveData(); 
        data.areParticlesActive = globalParticles != null && globalParticles.isPlaying;

        SaveSystem.Save(data);
        Debug.Log("Partida guardada");
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.Load();
        if (data == null) return;

        if (player != null)
        {
            player.LoadFromData(data);
        }

        RestoreParticles(data.areParticlesActive);
        Debug.Log("Partida cargada");
    }

    private void RestoreParticles(bool shouldBePlaying)
    {
        if (globalParticles == null) return;

        if (shouldBePlaying && !globalParticles.isPlaying)
        {
            globalParticles.Play();
        }
        else if (!shouldBePlaying && globalParticles.isPlaying)
        {
            globalParticles.Stop();
        }
    }
}
