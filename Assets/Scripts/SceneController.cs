using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
     public class SceneController : MonoBehaviour
     {

        public static SceneController instance;

        public Button startGameButton;
        public Button exitButton;
        public Button cancelExitButton;
        public Button yesButton;
        public Button noButton;
        public Button goBackToMainPanelButton;
        //public Button goBackToChallengePanelButton;
        //public Button easyToDifficultyPanelButton;
        //public Button mediumToDifficultyPanelButton;
        //public Button hardToDifficultyPanelButton;
        //public Button easyButton;
        //public Button mediumButton;
        //public Button hardButton;
        //public Button level1Button;
        //public Button level2Button;
        //public Button level3Button;
        //public Button level4Button;
        //public Button level5Button;
        //public Button level6Button;
        //public Button level7Button;
        //public Button level8Button;
        //public Button level9Button;
        //public Button level10Button;
        //public Button level11Button;
        //public Button level12Button;

        //public Button mediumLevel1Button;
        //public Button hardLevel1Button;

        //public Text text;

        //public Text level2Text;
        //public Text level3Text;
        //public Text level4Text;
        //public Text level5Text;
        //public Text level6Text;
        //public Text level7Text;
        //public Text level8Text;
        //public Text level9Text;
        //public Text level10Text;
        //public Text level11Text;
        //public Text level12Text;

        public bool boolean;

        public bool isLevelFinished;

        public GameObject exitGamepanel;
        public GameObject mainMenuPanel;
        //public GameObject chooseDifficultyPanel;
        //public GameObject easyPanel;
        //public GameObject mediumPanel;
        //public GameObject hardPanel;

        //public GameObject easyHeader;
        //public GameObject mediumHeader;
        //public GameObject hardHeader;

        public void Start()
        {
            AssignButtons();

        }

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void Update()
        {
            //UpdateMainPanel();
        }

        public void StartGame()
        {
            //mainMenuPanel.SetActive(false);
            PlayerManager.difficulties = EnumBase.Difficulties.easy;
            PlayerManager.level = 1;
            SceneManager.LoadScene(1);
            if (PlayerManager.instance.rigidBody.gravityScale == 0)
            {
                PlayerManager.instance.rigidBody.gravityScale = 2.5f;
            }
            TimeManager.instance.Start();
            //chooseDifficultyPanel.SetActive(true);
        }

        public void DisplayExitPanel()
        {
            exitGamepanel.SetActive(true);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void DontExitGame()
        {
            exitGamepanel.SetActive(false);
        }
                      
        //public void GoBackToMainPanel()
        //{
        //    chooseDifficultyPanel.SetActive(false);
        //    mainMenuPanel.SetActive(true);
        //}

        //public void GoBackToChallengePanel()
        //{
        //    //gameStatePanel.SetActive(false);
        //    chooseDifficultyPanel.SetActive(true);
        //    mainMenuPanel.SetActive(false);
        //}

        //public void ActivateEasyPanel()
        //{
        //    chooseDifficultyPanel.SetActive(false);
        //    easyPanel.SetActive(true);
        //    Debug.Log("Easy Button was Clicked");
        //}

        //public void ActivateMediumPanel()
        //{
        //    mediumPanel.SetActive(true);
        //}

        //public void ActivateHardPanel()
        //{
        //    hardPanel.SetActive(true);
        //}

        //public void GoBackToDifficultyPanel()
        //{
        //    easyPanel.SetActive(false);
        //    mediumPanel.SetActive(false);
        //    hardPanel.SetActive(false);
        //    chooseDifficultyPanel.SetActive(true);
        //}

        public void LoadLevel()
        {
            if (PlayerManager.difficulties == EnumBase.Difficulties.easy)
            {
                SceneManager.LoadScene(1);
            }
            else if (PlayerManager.difficulties == EnumBase.Difficulties.medium)
            {
                SceneManager.LoadScene(13);
                Debug.Log(PlayerManager.level);
            }
            else if (PlayerManager.difficulties == EnumBase.Difficulties.hard)
            {
                SceneManager.LoadScene(25);
            }
            
        }

        public void LoadLeveel1()
        {
            PlayerManager.level = 1;
        }

        public void LoadLeveel2()
        {
            PlayerManager.level = 2;
        }

        public void LoadLeveel3()
        {
            PlayerManager.level = 3;
        }

        public void LoadLeveel4()
        {
            PlayerManager.level = 4;
        }

        public void LoadLeveel5()
        {
            PlayerManager.level = 5;
        }

        public void LoadLeveel6()
        {
            PlayerManager.level = 6;
        }
        public void LoadLeveel7()
        {
            PlayerManager.level = 7;
        }
        public void LoadLeveel8()
        {
            PlayerManager.level = 8;
        }
        public void LoadLeveel9()
        {
            PlayerManager.level = 9;
        }
        public void LoadLeveel10()
        {
            PlayerManager.level = 10;
        }
        public void LoadLeveel11()
        {
            PlayerManager.level = 11;
        }
        public void LoadLeveel12()
        {
            PlayerManager.level = 12;
        }

        //public void ActivateEasyHeader()
        //{
        //    PlayerManager.difficulties = EnumBase.Difficulties.easy;
        //    //numberOfDoorCollisions = 2;
        //    easyHeader.SetActive(true);
        //    mediumHeader.SetActive(false);
        //    hardHeader.SetActive(false);
        //    chooseDifficultyPanel.SetActive(false);
        //    easyPanel.SetActive(true);
        //}

        //public void ActivateMediumHeader()
        //{
        //    PlayerManager.difficulties = EnumBase.Difficulties.medium;
        //    //numberOfDoorCollisions = 3;
        //    easyHeader.SetActive(false);
        //    mediumHeader.SetActive(true);
        //    hardHeader.SetActive(false);
        //    chooseDifficultyPanel.SetActive(false);
        //    mediumPanel.SetActive(true);
        //}

        //public void ActivateHardHeader()
        //{
        //    PlayerManager.difficulties = EnumBase.Difficulties.hard;
        //    //numberOfDoorCollisions = 4;
        //    easyHeader.SetActive(false);
        //    mediumHeader.SetActive(false);
        //    hardHeader.SetActive(true);
        //    chooseDifficultyPanel.SetActive(false);
        //    hardPanel.SetActive(true);
        //}


        public void AssignButtons()
        {
            startGameButton.onClick.AddListener(delegate { StartGame(); });
            exitButton.onClick.AddListener(delegate { DisplayExitPanel(); });
            yesButton.onClick.AddListener(delegate { ExitGame(); });
            noButton.onClick.AddListener(delegate { DontExitGame(); });
            cancelExitButton.onClick.AddListener(delegate { DontExitGame(); });
            //goBackToChallengePanelButton.onClick.AddListener(delegate { GoBackToMainPanel(); });

            //easyButton.onClick.AddListener(delegate { ActivateEasyHeader(); });
            //mediumButton.onClick.AddListener(delegate { ActivateMediumHeader(); });
            //hardButton.onClick.AddListener(delegate { ActivateHardHeader(); });
            //easyToDifficultyPanelButton.onClick.AddListener(delegate { GoBackToDifficultyPanel(); });
            //mediumToDifficultyPanelButton.onClick.AddListener(delegate { GoBackToDifficultyPanel(); });
            //hardToDifficultyPanelButton.onClick.AddListener(delegate { GoBackToDifficultyPanel(); });

            //level1Button.onClick.AddListener(delegate { LoadLevel(); });
            //mediumLevel1Button.onClick.AddListener(delegate { LoadLevel(); });
            //hardLevel1Button.onClick.AddListener(delegate { LoadLevel(); });
        }
     }
}
