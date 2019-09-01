using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CoinManager : MonoBehaviour
    {
        public static CoinManager instance;
        PlayerManager player;

        public static int totalNumberOfPickupsCollected;

        public int numberOfCoins;
        public float percentage;
        //public int noOfPickUps = 27;

        private bool hasCollided = false;

        public Slider slider;
        public Text sliderText;

        public GameObject pickUp;
       
        public static EnumBase.Pickups pickUps;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void Start()
        {
            player = GetComponent<PlayerManager>();
        }

        public void Update()
        {
            
        }

        public void CollectCoin()
        {
            if (hasCollided)
            {
                return;
            }
            hasCollided = true;

            // set the current enum
            if (pickUp.CompareTag("Token"))
            {
                pickUps = EnumBase.Pickups.token;
            }
            else if (pickUp.CompareTag("Coin"))
            {
                pickUps = EnumBase.Pickups.coin;
            }

            switch (pickUps)
            {
                case EnumBase.Pickups.token:
                    slider.value += 1;

                    sliderText.text = "1/2";
                    if (slider.value == 2)
                    {
                        sliderText.text = "2/2";
                    }
                    break;
                case EnumBase.Pickups.coin:
                    float coinPercentage = CalculateCoinPercentage();
                    //Debug.Log(coinPercentage);
                    slider.value += coinPercentage;
                    sliderText.text = slider.value.ToString() + "%";
                                    
                    break;
                default:
                    break;
            }
            pickUp.SetActive(false);
            totalNumberOfPickupsCollected += 1;
            Debug.Log(totalNumberOfPickupsCollected);
            
            
        }

        public float CalculateCoinPercentage()
        {
            float coinPercentage = percentage / numberOfCoins;
            return coinPercentage;
        }

        

       
    }
}
