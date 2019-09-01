using System;
using UnityEngine;

namespace Assets.Scripts
{
    class Parallaxing : MonoBehaviour
    {
        public Transform[] backGrounds;         // Array of all the back and foregrounds to be parallaxed
        private float[] parallaxScales;         // The proportions of the camera's movement to the backgrounds by
        public float smoothing;                // How smooth the parallax is going to be. Make sure to set this above zero

        private Transform cam;                  // reference to the main camera's transform
        private Vector3 previousCamPos;         // The position of the camera in the last frame

        void Awake()
        {
            // Set up the camera's reference
            cam = Camera.main.transform;
        }

        void Start()
        {
            // The previous frame has the current frame's camera position
            previousCamPos = cam.position;

            parallaxScales = new float[backGrounds.Length];

            for (int i = 0; i < backGrounds.Length; i++)
            {
                // assigning the corresponding parallaxSCales
                parallaxScales[i] = backGrounds[i].position.z * -1;
            }
        }

        public void Update()
        {
            for (int i = 0; i < backGrounds.Length; i++)
            {
                // The parallax is the opposite of the camera movement because the previous frame multiplied by the scale
                float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

                // Set a target x position which is the current position plus the parallax
                float backGroundTargetPosX = backGrounds[i].position.x + parallax;

                // Create a target position which is the background's current position with it's target x position
                Vector3 backGroundTargetPos = new Vector3(backGroundTargetPosX, backGrounds[i].position.y, backGrounds[i].position.z);

                // Fade between current position and target position using lerp
                backGrounds[i].position = Vector3.Lerp(backGrounds[i].position, backGroundTargetPos, smoothing * Time.deltaTime);
            }

            // Set the previousCamPos to the camera's position at the end of the frame
            previousCamPos = cam.position;
        }
    }
}
