using TMPro;
using UnityEngine;
using UnityEngine.UI;

// กำหนดให้เป็น sealed เพื่อไม่ให้สืบทอด
public sealed class GameManager : MonoBehaviour
{
    // Singleton Instance
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager instance is null! Is it in the scene?");
            }
            return _instance;
        }
    }

    [Header("Game State")]
    public int currentScore = 0;  // ใช้เป็น "เงิน" ด้วย
    public bool isGamePaused = false;

    [Header("UI Game")]
    public GameObject pauseMenuUI;
    public TMP_Text scoreText;   // แสดงเงิน (currentScore)
    public Slider HPBar;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // ให้ GameManager อยู่ข้าม Scene
            Debug.Log("GameManager Singleton Initialized.");
        }
        else
        {
            Debug.Log("Duplicate GameManager found. Destroying self.");
            Destroy(gameObject);
        }
    }

    // ------------------- GAMEPLAY & UI UPDATE -------------------

    // อัปเดต UI หลอดเลือด
    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if (HPBar != null)
        {
            HPBar.maxValue = maxHealth;
            HPBar.value = currentHealth;
            Debug.Log($"Health updated: {currentHealth}/{maxHealth}");
        }
        else
        {
            Debug.LogWarning("HPBar reference is missing in GameManager.");
        }
    }

    // ใช้เพิ่ม "เงิน" + อัปเดต UI
    public void AddScore(int amount)
    {
        currentScore += amount;

        if (scoreText != null)
            scoreText.text = currentScore.ToString();

        Debug.Log($"Money updated: {currentScore}");
    }

    // ใช้ตัด "เงิน" ตอนซื้อของ
    public bool SpendScore(int cost)
    {
        if (currentScore >= cost)
        {
            currentScore -= cost;

            if (scoreText != null)
                scoreText.text = currentScore.ToString();

            Debug.Log($"Buy Success. Money Left: {currentScore}");
            return true;
        }

        Debug.Log("Not enough money!");
        return false;
    }

    // เปิด/ปิดเมนู Pause
    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0f : 1f;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(isGamePaused);

        Debug.Log($"Game Paused: {isGamePaused}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
}
