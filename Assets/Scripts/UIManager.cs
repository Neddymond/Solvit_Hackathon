using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        CoinManager coinManager;

        public AudioSource sound;

        //public int currentLevelTotalNumberOfPickups;
        public static int scene;
        public Button pauseButton;
        public Button resumeButton;
        public Button mainMenuButton;
        public Button restartButton;
        public Button restartOnPause;
        public Button closePausepanelButton;
        public Button quitButton;
        public Button quitButton2;
        public Button exitButton;
        public Button dontExitButton;
        public Button menuButton;
        public Button settingInPause;
        public Button closeExitPanelButton;
        //public Button nextLevelButton;

        public Button RestartOnLevelFinish;

        public Button backToPausePanel;

        public GameObject door;
        public GameObject exitPanel;
        public GameObject pausePanel;
        public GameObject GameFinishPanel;
        public GameObject starLeft;
        public GameObject starCenter;
        public GameObject starRight;
        public GameObject gameOverPanel;
        public GameObject SettingPanel;

        public Text explanationHeader;
        public Text gameOverHighScore;
        public Text gameFinishHighScore;

        public static bool isGamePaused;
        public bool isGameOver;

        public void Awake()
        {
            
            if (instance == null)
            {
                instance = this;
                scene = SceneManager.GetActiveScene().buildIndex;
            }

            
        }
        public void Start()
        {  
            AssignButtonEvents();
            coinManager = GetComponent<CoinManager>();

            //PlayerManager.instance.rigidBody.gravityScale = 3f;
            AudioManager.instance.backgroundMusic.Play();
            TimeManager.countDownTime = 179;
            TimeManager.instance.Start();
        }

        public void ResumeGame()
        {
            if (isGamePaused)
            {
                isGamePaused = false;
                Time.timeScale = 1;
                pausePanel.SetActive(false);
                AudioManager.instance.backgroundMusic.UnPause();
            }
        }

        public void ResumeFromSettings()
        {
            SettingPanel.SetActive(false);
        }

        public void DontExitGameOnGameOver()
        {
            exitPanel.SetActive(false);
        }

        public void PauseGame()
        {
            if (!isGamePaused)
            {
                isGamePaused = true;
                Time.timeScale = 0;
                AudioManager.instance.backgroundMusic.Pause();
                pausePanel.SetActive(true);
            }
        }

        public void ReloadGame()
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }

            if (GameFinishPanel.activeInHierarchy)
            {
                GameFinishPanel.SetActive(false);
            }
            else if (gameOverPanel.activeInHierarchy && QuestionDisplayer.instance.panel.activeInHierarchy)
            {
                QuestionDisplayer.instance.panel.SetActive(false);
                gameOverPanel.SetActive(false);
            }
            
            isGamePaused = false;
            TimeManager.countDownTime = 179;
            TimeManager.instance.Start(); 
            PlayerManager.maxAnswerInput = 2;
            CoinManager.totalNumberOfPickupsCollected = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            PlayerManager.instance.animator.SetBool("Hurt", false);
            EnnemyManager.speed = 5.0f;
            //OptionBtn1.onClick.AddListener(delegate { PlayerManager.instance.CheckSolution(PlayerManager.instance.reshuffledOptions[0]); });
            //OptionBtn2.onClick.AddListener(delegate { PlayerManager.instance.CheckSolution(PlayerManager.instance.reshuffledOptions[1]); });
            //OptionBtn3.onClick.AddListener(delegate { PlayerManager.instance.CheckSolution(PlayerManager.instance.reshuffledOptions[2]); });
            //gameOverPanel.SetActive(false);
        }

        public void SetGameOverPanelTrue()
        {
            AudioManager.instance.backgroundMusic.Stop();          
            gameOverPanel.SetActive(true);
            gameOverHighScore.text = PlayerManager.highScore.ToString();
        }

        public void DisplayExitPromptPanel()
        {
            //pausePanel.SetActive(false);
            exitPanel.SetActive(true);
        }

        public void ExitGame()
        {
            PlayerManager.Save();
            Application.Quit();
        }

        public void DontExitGame()
        {
            exitPanel.SetActive(false);
            //pausePanel.SetActive(true);
        }

        public void OPenSettingPanel()
        {
            Time.timeScale = 0;
            SettingPanel.SetActive(true);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);

            //if (GameFinishPanel.activeInHierarchy)
            //{
            //    GameFinishPanel.SetActive(false);
            //}

            //if (gameOverPanel.activeInHierarchy)
            //{
            //    gameOverPanel.SetActive(false);
            //}

            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }

            GameObject timePanelFinder = GameObject.Find("TimerPanel");
            if (pausePanel.activeInHierarchy && timePanelFinder.activeInHierarchy)
            {
                pausePanel.SetActive(false);
                timePanelFinder.SetActive(false);
            }

            isGamePaused = false;
            TimeManager.countDownTime = 299;
            PlayerManager.maxAnswerInput = 2;
            CoinManager.totalNumberOfPickupsCollected = 0;
            PlayerManager.instance.transform.position = new Vector2(-10, -1.410966f);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            PlayerManager.instance.animator.SetBool("Hurt", false);
            PlayerManager.instance.rigidBody.gravityScale = 0f;

            //SceneController.instance.startGameButton.onClick.AddListener(delegate { SceneController.instance.StartGame(); });
            //SceneController.instance.exitButton.onClick.AddListener(delegate { SceneController.instance.DisplayExitPanel(); });
        }

        public void LoadLevel2()
        {
            GameFinishPanel.SetActive(false);

            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(2);
            PlayerManager.level = 2;
            PlayerManager.maxAnswerInput = 2;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel3()
        {
            //GameFinishPanel.SetActive(false);
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(3);
            PlayerManager.level = 3;
            PlayerManager.maxAnswerInput = 2;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel4()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(4);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 4;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel5()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(5);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 5;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel6()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(6);
            PlayerManager.level = 6;
            PlayerManager.maxAnswerInput = 2;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel7()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(7);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 7;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel8()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(8);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 8;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel9()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(9);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 9;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel10()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(10);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 10;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel11()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(11);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 11;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel12()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(12);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 12;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel13()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(13);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 1;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel14()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(14);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 2;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel15()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(15);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 3;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel16()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(16);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 4;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel17()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(17);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 5;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel18()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(18);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 6;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel19()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(19);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 7;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel20()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(20);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 8;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel21()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(21);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 9;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel22()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(22);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 10;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel23()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(23);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 11;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel24()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(24);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 12;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel25()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(25);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 1;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel26()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(26);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 2;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel27()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(27);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 3;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel28()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(28);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 4;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel29()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(12);
            PlayerManager.level = 5;
            PlayerManager.maxAnswerInput = 2;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel30()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(30);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 6;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel31()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(31);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 7;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel32()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(32);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 8;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel33()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(34);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 9;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel34()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(34);
            PlayerManager.level = 10;
            PlayerManager.maxAnswerInput = 2;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel35()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(35);
            PlayerManager.maxAnswerInput = 2;
            PlayerManager.level = 11;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        public void LoadLevel36()
        {
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            SceneManager.LoadScene(36);
            PlayerManager.level = 12;
            PlayerManager.maxAnswerInput = 2;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
        }

        // Reset the value and Text of the Token and Coin Panel
        public void ResetPickupPanel()
        {
            GameObject tokenSliderFinder = GameObject.Find("TokenSlider");
            GameObject coinSliderFinder = GameObject.Find("CoinSlider");
            GameObject textFinder = GameObject.Find("TokenSliderText");
            GameObject coinTextFinder = GameObject.Find("CoinSliderText");

            Slider tokenSlider = tokenSliderFinder.GetComponent<Slider>();
            tokenSlider.value = 0;

            Text toks = tokenSlider.GetComponentInChildren<Text>();
            toks.text = "0/2";

            Slider coinSlider = coinSliderFinder.GetComponent<Slider>();
            coinSlider.value = 0;

            Text coins = coinSlider.GetComponentInChildren<Text>();
            coins.text = "0%";
        }

        public void LoadLevel(int LevelIndex)
        {
            ResetPickupPanel();
            GameFinishPanel.SetActive(false);
            CoinManager.totalNumberOfPickupsCollected = 0;
            TimeManager.countDownTime = 299;
            TimeManager.instance.Start();
            SceneManager.LoadScene(LevelIndex);

            int currentLevel = LevelIndex;
            if (currentLevel >12)
            {
                currentLevel = 1;
            }
            PlayerManager.level = currentLevel;
            PlayerManager.maxAnswerInput = 2;
            AudioManager.instance.backgroundMusic.Play();
            PlayerManager.instance.player.transform.position = new Vector2(-10, 0);
            PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
            PlayerManager.instance.canJump = true;
            PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
            PlayerManager.instance.runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
            LevelLoader.scene = LevelIndex;
        }

        //public void LoadLevel2()
        //{
        //    SceneManager.LoadScene(2);
        //    PlayerManager.maxAnswerInput = 2;
        //    AudioManager.instance.backgroundMusic.Play();
        //    player.transform.position = new Vector2(-10, 0);
        //    PlayerManager.instance.animationMode = EnumBase.AnimationMode.idle;
        //    PlayerManager.instance.canJump = true;
        //    PlayerManager.instance.animator.SetFloat("Speed", Mathf.Abs(PlayerManager.instance.horizontalMove));
        //    PlayerManager.instance.runSpeed = 50f;
        //}
        public void AssignButtonEvents()
        {
            resumeButton.onClick.AddListener(delegate { ResumeGame(); });
            pauseButton.onClick.AddListener(delegate { PauseGame(); });
            restartButton.onClick.AddListener(delegate { ReloadGame(); });
            restartOnPause.onClick.AddListener(delegate { ReloadGame(); });
            RestartOnLevelFinish.onClick.AddListener(delegate { ReloadGame(); });
            closePausepanelButton.onClick.AddListener(delegate { ResumeGame(); });
            quitButton.onClick.AddListener(delegate { DisplayExitPromptPanel(); });
            quitButton2.onClick.AddListener(delegate { DisplayExitPromptPanel(); });
            exitButton.onClick.AddListener(delegate { ExitGame(); });
            dontExitButton.onClick.AddListener(delegate { DontExitGame(); });
            closeExitPanelButton.onClick.AddListener(delegate { DontExitGame(); });
            mainMenuButton.onClick.AddListener(delegate { LoadMainMenu(); });
            menuButton.onClick.AddListener(delegate { LoadMainMenu(); });
            backToPausePanel.onClick.AddListener(delegate { ResumeFromSettings(); });
            settingInPause.onClick.AddListener(delegate { OPenSettingPanel(); });
            //nextLevelButton.onClick.AddListener(delegate { LoadNextlevel(); });
        }

        

        //public void StartDisplayGameFinishPanel()
        //{
        //    StartCoroutine(DisplayGameFinishPanel());
        //}
    }
}
