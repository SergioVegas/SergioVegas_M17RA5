using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public bool hasWeapon;
    public bool isWeaponEquipped;
    public bool areParticlesActive;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    public GameObject playerObject;
    public ParticleSystem globalParticles; 

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

        if (playerObject == null)
            playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    { 
        Invoke("LoadGame", 0.1f);
    }

    public void RestartGame()
    {
        PlayerPrefs.DeleteKey("SaveGame");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SaveGame()
    {
        if (playerObject == null) return;

        EquipWeapon equipScript = playerObject.GetComponent<EquipWeapon>();
        
        bool hasWeapon = equipScript != null && equipScript.HasWeapon;
        bool weaponEquipped = equipScript != null && equipScript.IsEquipped;
        bool particleState = globalParticles != null && globalParticles.isPlaying;

        PlayerData data = new PlayerData();
        data.position = new float[3];
        data.position[0] = playerObject.transform.position.x;
        data.position[1] = playerObject.transform.position.y;
        data.position[2] = playerObject.transform.position.z;

        data.hasWeapon = hasWeapon;
        data.isWeaponEquipped = weaponEquipped;
        data.areParticlesActive = particleState;

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("SaveGame", json);
        PlayerPrefs.Save();
        Debug.Log("Partida guardada");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("SaveGame"))
        {
            string json = PlayerPrefs.GetString("SaveGame");
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            if (playerObject != null)
            {
                CharacterController cc = playerObject.GetComponent<CharacterController>();
                if (cc != null) cc.enabled = false;
                
                playerObject.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
                
                if (cc != null) cc.enabled = true;

                // Restore Weapon
                EquipWeapon equipScript = playerObject.GetComponent<EquipWeapon>();
                if (equipScript != null)
                {
                    if (data.hasWeapon)
                    {
                        equipScript.CreateWeapon(); 
                        
                        if (data.isWeaponEquipped)
                        {
                            equipScript.SetEquipped(true);
                        }
                        else
                        {
                            equipScript.SetEquipped(false); 
                        }
                    }
                }
            }

            // Restore Particles
            if (globalParticles != null)
            {
                if (data.areParticlesActive && !globalParticles.isPlaying)
                {
                    globalParticles.Play();
                }
                else if (!data.areParticlesActive && globalParticles.isPlaying)
                {
                    globalParticles.Stop();
                }
            }

            Debug.Log("Partida cargada!");
        }
    }
}
