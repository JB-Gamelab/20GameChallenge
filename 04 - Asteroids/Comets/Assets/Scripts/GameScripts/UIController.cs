using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image life1Image;
    [SerializeField] private Image life2Image;
    [SerializeField] private Image life3Image;
    [SerializeField] private Canvas pauseMenu;
    [SerializeField] private Slider musicVolSlider;
    [SerializeField] private Slider sFXVolSlider;


    private void Awake()
    {
        ScoreManager.onScoreChanged += ScoreManagerOnScoreChanged;
        GameManager.onLifeLost += GameManagerOnLifeLost;
        GameManager.onPause += GameManagerOnPause;
    }

    private void OnDestroy()
    {
        ScoreManager.onScoreChanged -= ScoreManagerOnScoreChanged;
        GameManager.onLifeLost -= GameManagerOnLifeLost;
        GameManager.onPause -= GameManagerOnPause;
    }

    private void Start()
    {
        scoreText.text = "SCORE: 0";
        life1Image.gameObject.SetActive(true);
        life2Image.gameObject.SetActive(true);
        life3Image.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
        musicVolSlider.value = PlayerPrefsManager.GetMusicVolume();
        sFXVolSlider.value = PlayerPrefsManager.GetSFXVolume();
    }

    private void GameManagerOnLifeLost(int lives)
    {
        if (lives >= 3)
        {
            life1Image.gameObject.SetActive(true);
            life2Image.gameObject.SetActive(true);
            life3Image.gameObject.SetActive(true);
        }
        if (lives == 2)
        {
            life1Image.gameObject.SetActive(true);
            life2Image.gameObject.SetActive(true);
            life3Image.gameObject.SetActive(false);
        }
        if (lives == 1)
        {
            life1Image.gameObject.SetActive(true);
            life2Image.gameObject.SetActive(false);
            life3Image.gameObject.SetActive(false);
        }
        if (lives == 0)
        {
            life1Image.gameObject.SetActive(false);
            life2Image.gameObject.SetActive(false);
            life3Image.gameObject.SetActive(false);
        }
    }

    private void ScoreManagerOnScoreChanged(int score)
    {
        scoreText.text = "SCORE: " + score;
    }

    private void GameManagerOnPause(bool paused)
    {
        pauseMenu.gameObject.SetActive(paused);
        
        if (!paused)
        {
            PlayerPrefsManager.SetVolumes(musicVolSlider.value, sFXVolSlider.value);
        }
    }

    public float GetMusicVolume()
    {
        return musicVolSlider.value;
    }

    public float GetSFXVolume()
    {
        return sFXVolSlider.value;
    }
}
