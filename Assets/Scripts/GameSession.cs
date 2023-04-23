using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int PlayerLives = 3;
    [SerializeField] TextMeshProUGUI Lives;
    [SerializeField] Image Heart;
    RectTransform HeartTransform;
    bool HasMove = false;

    void Awake()
    {
        int NumGameSession = FindObjectsOfType<GameSession>().Length;
        if(NumGameSession > 1 )
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        UpdatePlayerLives();
        HeartTransform = Heart.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (PlayerLives != 1) return;
        if(HasMove)
        {
            HasMove = false;
            HeartTransform.anchoredPosition = new Vector2(HeartTransform.anchoredPosition.x - 6f, HeartTransform.anchoredPosition.y);
        }
        else
        {
            HasMove = true;
            HeartTransform.anchoredPosition = new Vector2(HeartTransform.anchoredPosition.x + 6f, HeartTransform.anchoredPosition.y);
        }
    }
    public void ProcessPlayerDeath()
    {
        if(PlayerLives > 1 )
        {
            TakeLife();
        }
        else
        {
            FindObjectOfType<LevelPersistent>().ResetLevelPersist();
            ResetGameSession();
        }
    }

    public void TakeLife()
    {
        PlayerLives--;
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex);
        FindObjectOfType<PlayerMovement>().IsAlive = true;
        UpdatePlayerLives();
    }

    public void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void UpdatePlayerLives()
    {
        Lives.text = PlayerLives.ToString();
    }
}
