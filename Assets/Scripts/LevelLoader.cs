using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class LevelLoader : MonoBehaviour
    {
        //public Button nextLevelButton;
        public static int scene;

        public void Awake()
        {
            scene = SceneManager.GetActiveScene().buildIndex;
        }

        public void LoadNewScene()
        {
            //if (scene == 1)
            //{
            //    UIManager.instance.LoadLevel2();
            //}

            UIManager.instance.LoadLevel(scene+1);

        }

        public void AssignButtonEvents()
        {
           // nextLevelButton.onClick.AddListener(delegate { LoadNewScene(); });
        }
    }
}
