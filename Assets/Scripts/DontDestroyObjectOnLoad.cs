using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts 
{
    class DontDestroyObjectOnLoad : MonoBehaviour
    {

        public void Awake()
        {
            int numOfObjects = GameObject.FindGameObjectsWithTag("Player").Length;

            if (numOfObjects > 1)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            //player.transform.position = new Vector2(-10f, -1.4f);

        }

    }
}
