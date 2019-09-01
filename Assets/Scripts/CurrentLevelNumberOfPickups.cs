using System;
using UnityEngine;

namespace Assets.Scripts
{
    class CurrentLevelNumberOfPickups : MonoBehaviour
    {
        public static CurrentLevelNumberOfPickups instance;
        public int currentLevelTotalNumberOfPickups;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
    }
}
