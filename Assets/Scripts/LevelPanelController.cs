using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class LevelPanelController : MonoBehaviour
    {
        public GameObject currentLevelPanel = null;
        public Text currentDifficultyText = null;
        public Text currentLevelText = null;

        public static LevelPanelController instance;

        public void Awake()
        {
            if (instance !=  this)
            {
                instance = this;
            }
        }

        public void LateUpdate()
        {
            SetLevelPanel();
        }

        /** Displays the current Difficulty and level the player is currently playing */
        public void SetLevelPanel()
        {
            switch (PlayerManager.difficulties)
            {
                case EnumBase.Difficulties.easy:
                    if (PlayerManager.level == 1)
                    {
                        currentDifficultyText.text = "EASY";
                        currentLevelText.text = "Level : 1";
                    }
                    else if (PlayerManager.level == 2)
                    {
                        currentDifficultyText.text = "EASY";
                        currentLevelText.text = "Level : 2";
                    }
                    else if (PlayerManager.level == 3)
                    {
                        currentDifficultyText.text = "EASY";
                        currentLevelText.text = "Level : 3";
                    }
                    else if (PlayerManager.level == 4)
                    {
                        currentDifficultyText.text = "EASY";
                        currentLevelText.text = "Level : 4";
                    }
                    else if (PlayerManager.level == 5)
                    {
                        currentDifficultyText.text = "EASY";
                        currentLevelText.text = "Level : 5";
                    }
                    else if (PlayerManager.level == 6)
                    {
                        currentDifficultyText.text = "EASY";
                        currentLevelText.text = "Level : 6";
                    }
                    else if (PlayerManager.level == 7)
                    {
                        currentDifficultyText.text = "EASY";
                        currentLevelText.text = "Level : 7";
                    }
                    else if (PlayerManager.level == 8)
                    {
                        currentDifficultyText.text = "EASY";
                        currentLevelText.text = "Level : 8";
                    }
                    else if (PlayerManager.level == 9)
                    {
                        currentDifficultyText.text = "EASY";
                        currentLevelText.text = "Level : 9";
                    }
                    else if (PlayerManager.level == 10)
                    {
                        currentDifficultyText.text = "EASY";
                        currentLevelText.text = "Level : 10";
                    }
                    else if (PlayerManager.level == 11)
                    {
                        currentDifficultyText.text = "EASY";
                        currentLevelText.text = "Level : 11";
                    }
                    else if (PlayerManager.level == 12)
                    {
                        currentDifficultyText.text = "EASY";
                        currentLevelText.text = "Level : 12";
                    }
                    break;
                case EnumBase.Difficulties.medium:
                    if (PlayerManager.level == 1)
                    {
                        currentDifficultyText.text = "MEDIUM";
                        currentLevelText.text = "Level : 1";
                    }
                    else if (PlayerManager.level == 2)
                    {
                        currentDifficultyText.text = "MEDIUM";
                        currentLevelText.text = "Level : 2";
                    }
                    else if (PlayerManager.level == 3)
                    {
                        currentDifficultyText.text = "MEDIUM";
                        currentLevelText.text = "Level : 3";
                    }
                    else if (PlayerManager.level == 4)
                    {
                        currentDifficultyText.text = "MEDIUM";
                        currentLevelText.text = "Level : 4";
                    }
                    else if (PlayerManager.level == 5)
                    {
                        currentDifficultyText.text = "MEDIUM";
                        currentLevelText.text = "Level : 5";
                    }
                    else if (PlayerManager.level == 6)
                    {
                        currentDifficultyText.text = "MEDIUM";
                        currentLevelText.text = "Level : 6";
                    }
                    else if (PlayerManager.level == 7)
                    {
                        currentDifficultyText.text = "MEDIUM";
                        currentLevelText.text = "Level : 7";
                    }
                    else if (PlayerManager.level == 8)
                    {
                        currentDifficultyText.text = "MEDIUM";
                        currentLevelText.text = "Level : 8";
                    }
                    else if (PlayerManager.level == 9)
                    {
                        currentDifficultyText.text = "MEDIUM";
                        currentLevelText.text = "Level : 9";
                    }
                    else if (PlayerManager.level == 10)
                    {
                        currentDifficultyText.text = "MEDIUM";
                        currentLevelText.text = "Level : 10";
                    }
                    else if (PlayerManager.level == 11)
                    {
                        currentDifficultyText.text = "MEDIUM";
                        currentLevelText.text = "Level : 11";
                    }
                    else if (PlayerManager.level == 12)
                    {
                        currentDifficultyText.text = "MEDIUM";
                        currentLevelText.text = "Level : 12";
                    }
                    break;
                case EnumBase.Difficulties.hard:
                    if (PlayerManager.level == 1)
                    {
                        currentDifficultyText.text = "HARD";
                        currentLevelText.text = "Level : 1";
                    }
                    else if (PlayerManager.level == 2)
                    {
                        currentDifficultyText.text = "HARD";
                        currentLevelText.text = "Level : 2";
                    }
                    else if (PlayerManager.level == 3)
                    {
                        currentDifficultyText.text = "HARD";
                        currentLevelText.text = "Level : 3";
                    }
                    else if (PlayerManager.level == 4)
                    {
                        currentDifficultyText.text = "HARD";
                        currentLevelText.text = "Level : 4";
                    }
                    else if (PlayerManager.level == 5)
                    {
                        currentDifficultyText.text = "HARD";
                        currentLevelText.text = "Level : 5";
                    }
                    else if (PlayerManager.level == 6)
                    {
                        currentDifficultyText.text = "HARD";
                        currentLevelText.text = "Level : 6";
                    }
                    else if (PlayerManager.level == 7)
                    {
                        currentDifficultyText.text = "HARD";
                        currentLevelText.text = "Level : 7";
                    }
                    else if (PlayerManager.level == 8)
                    {
                        currentDifficultyText.text = "HARD";
                        currentLevelText.text = "Level : 8";
                    }
                    else if (PlayerManager.level == 9)
                    {
                        currentDifficultyText.text = "HARD";
                        currentLevelText.text = "Level : 9";
                    }
                    else if (PlayerManager.level == 10)
                    {
                        currentDifficultyText.text = "HARD";
                        currentLevelText.text = "Level : 10";
                    }
                    else if (PlayerManager.level == 11)
                    {
                        currentDifficultyText.text = "HARD";
                        currentLevelText.text = "Level : 11";
                    }
                    else if (PlayerManager.level == 12)
                    {
                        currentDifficultyText.text = "HARD";
                        currentLevelText.text = "Level : 12";
                    }
                    break;
                default:
                    break;
            }
        }

    }
}
