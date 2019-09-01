using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Assets.Scripts.Logical;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets.Scripts
{
    public class PlayerManager : MonoBehaviour
    {
 
        public GameObject ThinkEffect;

        public static PlayerManager instance;

        public CharacterController2D controller;

        public SpriteRenderer render;

        public float runSpeed = 50f;
        public float horizontalMove = 0;

        int easy;
        int medium;
        int hard;

        private bool jump = false;
        public bool crouch = false;
        public bool climb = false;
        public bool hurt = false;
        public bool isAllDoorsUnlocked = false;

        public bool canJump = true;
        public bool canClimb = true;
        public bool correctAnswer;

        public Animator animator;

        //public GameObject questionPanel;

        Collision2D enemy;

        public float[] reshuffledOptions;

        public GameObject playerRoot;
        public GameObject player;
        public GameObject emptyGameObject;

        EnnemyManager ennemyManager;
        //PuzzleController puzzleController;
        //PuzzleManager puzzleManager;

        public static EnumBase.Difficulties difficulties;
        public static int level;
        public static int maxAnswerInput = 2;
        public static int highScore;
        public static int scene;

        public Rigidbody2D rigidBody;

        public static float answer;

        public List<string> Operators;

        public EnumBase.AnimationMode animationMode;
        public EnumBase.Pickups getPickUps;
        //public static EnumBase.GameChoice gameChoice;
        static LevelModel model;
        Collision2D gameObstacle;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
                scene = SceneManager.GetActiveScene().buildIndex;
            }
        }

        private void Start()
        {
            //mainCamera = GameObject.FindWithTag("MainCamera");
            render = GetComponent<SpriteRenderer>();
            Operators = new List<string>() { "+", "-", "/", "*" };
            rigidBody = GetComponent<Rigidbody2D>();
            animationMode = EnumBase.AnimationMode.idle;
            //proceedToLvl2Button.onClick.AddListener(delegate { LoadLevel2(); });
        }

        private void Update()
        {
            //mainCamera.transform.position = new Vector3(transform.position.x, 30, transform.position.z);
            horizontalMove = CrossPlatformInputManager.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (CrossPlatformInputManager.GetButton("Jump"))
            {
                if (!canJump)
                {
                    return;
                }
                jump = true; 
                animator.SetBool("Jump", jump);
            }

            if (CrossPlatformInputManager.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (CrossPlatformInputManager.GetButtonUp("Crouch"))
            {
                crouch = false;
            }


            switch (animationMode)
            {
                case EnumBase.AnimationMode.climbing:
                    if (CrossPlatformInputManager.GetButton("Climb"))
                    {
                        if (!canClimb)
                        {
                            return;
                        }
                        UpdateClimb();
                    }
                    else
                    {
                        //canClimb = false;
                        rigidBody.gravityScale = 2.5f;
                        animator.SetBool("Climb", false);
                    }

                    break;
            }

            OnCollisionEnter2D(gameObstacle);
        }

        public void OnLanding()
        {
            animator.SetBool("Jump", false);
    
        }

        public void OnCrouching(bool crouch)
        {
            animator.SetBool("Crouch", crouch);
        }

        private void FixedUpdate()
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
            
        }

        //Saves the current game state the player is in before exiting the game
        public static void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream mathsFile = File.Create(Application.persistentDataPath + "/PlayerMathsInfo.dat");


            PlayerData data = new PlayerData();
            data.highScore = highScore;
            data.level = level;
            data.difficulties = difficulties;

            //DontDestroyOnLoad(instance.gameObject);
            //data.scene = UIManager.scene;
            //data.level = level;

            PlayerPrefs.SetInt("Level", UIManager.scene);
            PlayerPrefs.Save();
            bf.Serialize(mathsFile, data);
            mathsFile.Close();

            Debug.Log(data.highScore);
            Debug.Log(data.scene);
            Debug.Log(data.level);
        }

        public void SaveGameState()
        {

            if (File.Exists(Application.persistentDataPath + "/GameModel.dat"))
            {
                BinaryFormatter saveGameState = new BinaryFormatter();
                FileStream gameFile = File.Open(Application.persistentDataPath + "/GameModel.dat", FileMode.Open);

                //GameChoiceData gameChoiceData = (GameChoiceData)(saveGameState.Deserialize(gameFile));
                GameDifficultyData gameDifficultyData = new GameDifficultyData();
                GameDataFiles levelData = new GameDataFiles();

                //Check the Difficulty
                switch (difficulties)
                {
                    case EnumBase.Difficulties.easy:
                        levelData = gameDifficultyData.easy.Where(item => item.level == level).FirstOrDefault();
                        break;
                    case EnumBase.Difficulties.medium:
                        levelData = gameDifficultyData.medium.Where(item => item.level == level).FirstOrDefault();
                        break;
                    case EnumBase.Difficulties.hard:
                        levelData = gameDifficultyData.hard.Where(item => item.level == level).FirstOrDefault();
                        break;
                }

                levelData.coinsCollected = CoinManager.totalNumberOfPickupsCollected;
                levelData.highScore = highScore;
                levelData.level = level;
                gameFile.Close();

                SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
            }
        }

        //Loads the game state that was saved
        public static void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/PlayerMathsInfo.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/playerMathsInfo.dat", FileMode.Open);
                PlayerData data = (PlayerData)(bf.Deserialize(file));
                file.Close();
                level = data.level;
                highScore = data.highScore;
                //EnumBase.GameChoice gameChoice = data.gameChoice;
                difficulties = data.difficulties;
                SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
            }
            else if (File.Exists(Application.persistentDataPath + "/PlayerPuzzleInfo.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/PlayerPuzzleInfo.dat", FileMode.Open);
                PlayerData data = new PlayerData();
                file.Close();
                level = data.level;
                highScore = data.highScore;
                difficulties = data.difficulties;
                //EnumBase.GameChoice gameChoice = data.gameChoice;
                SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // collect coin or token
            if (other.gameObject.CompareTag("Coin") || other.gameObject.CompareTag("Token"))
            {
                CoinManager coinManager = other.gameObject.GetComponent<CoinManager>();
                coinManager.CollectCoin();
                CheckNumberOfPickupsCollected();
                UIManager.instance.sound.PlayOneShot(AudioManager.instance.coinSound);
                Debug.Log("entered");
            }

            //climb ladder
            if (other.gameObject.CompareTag("Ladder") && controller.m_Grounded)
            {
                canClimb = true;
                climb = true;
                animator.SetBool("Climb", climb);
                controller.m_Grounded = false;
                animationMode = EnumBase.AnimationMode.climbing;
            }
        }

        private IEnumerator OnTriggerExit2D(Collider2D collision)
        {

            if (collision.gameObject.CompareTag("Ladder"))
            {
                Debug.Log("exited");
                canClimb = false;

                collision.isTrigger = false;

                GameObject gameObjectWithLadderTag = GameObject.FindGameObjectWithTag("Ladder");
                gameObjectWithLadderTag.GetComponent<Collider2D>().enabled = false;

                climb = false;
                animator.SetBool("Climb", climb);
                rigidBody.gravityScale = 20f;
                animator.SetBool("Idle", true);

                yield return new WaitForSeconds(0.5f);

                collision.isTrigger = true;
                rigidBody.gravityScale = 2.5f;
                climb = true;
                gameObjectWithLadderTag.GetComponent<Collider2D>().enabled = true;
            }


            //    //emptyGameObject.transform.parent = player.transform;

            //    if (collision.gameObject.CompareTag("Ladder") /**&& controller.m_Grounded*/)
            //    {
            //        GetComponent<BoxCollider2D>().enabled = false;
            //        StartCoroutine(EnableBox(2f));


            //    //    Debug.Log("Left");
            //    //    climb = false;
            //    //    animator.SetBool("Climb", climb);
            //    //    transform.Translate(0, 0, 0);
            //    //    rigidBody.gravityScale = 2.5f;
            //        //controller.m_Grounded = true;

            //        //animationMode = EnumBase.AnimationMode.idle;
            //    }
        }

        public void CheckNumberOfPickupsCollected()
        {
            if (CoinManager.totalNumberOfPickupsCollected == CurrentLevelNumberOfPickups.instance.currentLevelTotalNumberOfPickups)
            {
                AudioManager.instance.backgroundMusic.Stop();
                StartCoroutine(DisplayGameFinishPanel());
                //UIManager.instance.ResetPickupPanel();
            }
        }

        public IEnumerator DisplayGameFinishPanel()
        {
            UIManager.instance.gameFinishHighScore.text = "Total Score : " + highScore;
            yield return new WaitForSeconds(0.5f);
            //rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            runSpeed = 0;
            EnnemyManager.speed = 0;

            yield return new WaitForSeconds(1f);
            UIManager.instance.sound.PlayOneShot(AudioManager.instance.winSound);
            UIManager.instance.GameFinishPanel.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            UIManager.instance.starLeft.SetActive(true);

            yield return new WaitForSeconds(0.5f);
            UIManager.instance.starCenter.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            UIManager.instance.starRight.SetActive(true);
        }

        public void UpdateClimb()
        {
            canClimb = true;
            climb = true;
            animator.SetBool("Climb", climb);
            Debug.Log("Climb");
            rigidBody.gravityScale = 0;
            transform.Translate(0, 5 * Time.deltaTime, 0);

            //climb = false;
        }

        public IEnumerator CorrectAnswer()
        {
            highScore += 5;
            PlayerPrefs.SetInt("HighScore", highScore);
            QuestionDisplayer.instance.correctText.SetActive(false);
            QuestionDisplayer.instance.questionPanel.SetActive(false);
            QuestionDisplayer.instance.correctPanel.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            QuestionDisplayer.instance.correctText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            QuestionDisplayer.instance.correctText.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            QuestionDisplayer.instance.correctText.SetActive(true);
            QuestionDisplayer.instance.inCorrectPanel.SetActive(false);
            QuestionDisplayer.instance.panel.SetActive(false);

            yield return new WaitForSeconds(0.3f);
            StartCoroutine(TransportPlayer(0.1f, 0.2f));
            QuestionDisplayer.instance.questionBackground.SetActive(false);
            UIManager.instance.door.SetActive(false);
            QuestionDisplayer.instance.correctPanel.SetActive(false);
            runSpeed = 50f;
            EnnemyManager.speed = 5.0f;
            canJump = true;
            canClimb = true;
            hurt = false;
            animator.SetBool("Hurt", false);
            QuestionDisplayer.instance.non_QuestionHeader.SetActive(false);
        }

        public IEnumerator InCorrectAnswer()
        {
            QuestionDisplayer.instance.inCorrectPanel.SetActive(true);
            QuestionDisplayer.instance.questionPanel.SetActive(false);
            QuestionDisplayer.instance.correctText.SetActive(false);

            yield return new WaitForSeconds(1);
            QuestionDisplayer.instance.inCorrectPanel.SetActive(false);
            QuestionDisplayer.instance.questionPanel.SetActive(true);
        }

        private IEnumerator OnCollisionEnter2D(Collision2D obstacle)
        {
            gameObstacle = obstacle;
            if (obstacle.gameObject.CompareTag("Door"))
            {
                canJump = false;
                runSpeed = 0f;

                QuestionDisplayer.instance.questionHeader.SetActive(true);
                ShowQuestion();
                UIManager.instance.door = obstacle.gameObject;
            }
            //else if (obstacle.gameObject.CompareTag("Door") && gameChoice == EnumBase.GameChoice.puzzle)
            //{
            //    UIManager.instance.puzzleQuestionPanel.SetActive(true);
            //    canJump = false;
            //    runSpeed = 0f;
            //    UIManager.instance.door = obstacle.gameObject;

            //    PuzzleData puzzldeData = PuzzleManager.instance.GetPuzzleQuestion(difficulties, level);
            //    PuzzleManager.instance.AssignPuzzleData(puzzldeData);
            //    AssignPuzzleButtons();
            //}

            if (obstacle.gameObject.CompareTag("Spike"))
            {
                Debug.Log("Enemy");
                StartCoroutine(WhenPlayerIsHurt());
                //StartCoroutine(TransportPlayer(0.1f, 0.2f, obstacle));
            }
            //else if (obstacle.gameObject.CompareTag("Spike") && gameChoice == EnumBase.GameChoice.puzzle)
            //{
            //    UIManager.instance.puzzleQuestionPanel.SetActive(true);
            //    canJump = false;
            //    runSpeed = 0f;
            //    hurt = true;
            //    animator.SetBool("Hurt", hurt);
            //    EnnemyManager.speed = 0f;
            //    canClimb = false;
            //    PuzzleData puzzleData = PuzzleManager.instance.GetPuzzleQuestion(difficulties, level);
            //    PuzzleManager.instance.AssignPuzzleData(puzzleData);
            //    //StartCoroutine(TransportPlayer(0.1f, 0.2f, obstacle));
            //    AssignPuzzleButtons();
            //}

            if (obstacle.gameObject.CompareTag("Enemy"))
            {
                UIManager.instance.sound.PlayOneShot(AudioManager.instance.enemyAttacksound);
                gameObstacle.collider.enabled = false;
                StartCoroutine(WhenPlayerIsHurt());
                yield return new WaitForSeconds(10);
                gameObstacle.collider.enabled = true;

            }
            else if (obstacle.gameObject.CompareTag("AnimatedTile"))
            {
                player.transform.parent = obstacle.gameObject.transform;
                Debug.Log("animated_tile");
            }
            else
            {
                StopCoroutine("WhenPlayerIsHurt");
            }
        }

        private void OnCollisionExit2D(Collision2D obstacle)
        {
            if (obstacle.gameObject.CompareTag("AnimatedTile"))
            {
                player.transform.parent = GameObject.Find("Parent").transform;
            }
        }


        public IEnumerator WhenPlayerIsHurt()
        {

            canJump = false;
            canClimb = false;
            //animator.SetBool("Climb", climb);
            hurt = true;
            runSpeed = 0f;
            EnnemyManager.speed = 0f;
            //rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            animator.SetBool("Hurt", hurt);
            yield return new WaitForSeconds(1);
            //animator.SetBool("Hurt", false);
            QuestionDisplayer.instance.questionHeader.SetActive(false);
            QuestionDisplayer.instance.non_QuestionHeader.SetActive(true);
            ShowQuestion();
        }

        public IEnumerator EnemyCollisionController()
        {
            if (gameObstacle.gameObject.CompareTag("Enemy"))
            {
                gameObstacle.collider.enabled = false;
                yield return new WaitForSeconds(10);
                gameObstacle.collider.enabled = true;
            }
        }

        public IEnumerator TransportPlayer(float duration, float blinkTime)
        {
            //obstacle.transform.position = new Vector2((int)obstacle.transform.position.x, 0)
            if (gameObstacle.gameObject.CompareTag("Spike"))
            {
                yield return new WaitForSeconds(0.2f);
                while (duration > 0f)
                {

                    duration -= Time.deltaTime;
                    render.enabled = !render.enabled;

                    yield return new WaitForSeconds(blinkTime);
                }
                render.enabled = true;
                transform.position = new Vector2((int)gameObstacle.gameObject.transform.position.x, 0) - new Vector2(15, 0);
            }
        }

        public void ShowQuestion()
        {

            canJump = false;
            QuestionDisplayer.instance.questionPanel.SetActive(true);
            QuestionDisplayer.instance.panel.SetActive(true);
            QuestionDisplayer.instance.questionBackground.SetActive(true);
            QuestionDisplayer.instance.OptionBtn1.onClick.RemoveAllListeners();
            QuestionDisplayer.instance.OptionBtn2.onClick.RemoveAllListeners();
            QuestionDisplayer.instance.OptionBtn3.onClick.RemoveAllListeners();
            LevelModel model = LevelController.LevelDifficulty(difficulties, level);
            List<string> getOperator = GetOperator();

            List<int> values = GenerateRandomNumbers(getOperator);

            List<float> floatValues = convertIntegersTofloat(values);

            QuestionDisplayer.instance.question.text = DataLogic.GenerateQuestionString(values, getOperator);
            answer = GetCorrectAnswer(floatValues, getOperator);
            AssignButtonEvents();
        }

        public static List<int> GenerateRandomNumbers(List<string> operatorValue)
        {
            List<int> values = new List<int>();
            System.Random rnd = new System.Random();

            //Populate List


            model = LevelController.LevelDifficulty(difficulties, level);
            //values[0] = 2 * rnd.Next(model.minNumber / 2, model.maxNUmber / 2);

            for (int i = 0; i < model.operatorIndex; i++)
            {
                values.Add(0);
                values[i] = rnd.Next(model.minNumber, model.maxNUmber);
            }
            values.Add(0);

            for (int i = 0; i < model.operatorIndex; i++)
            {

                if (operatorValue[i] == "/")
                {
                    values[i + 1] = GetDivisibleNumber(values[i], model.minNumber);
                }

                values[i + 1] = rnd.Next(model.minNumber, model.maxNUmber);
            }

            return values;
        }

        public static int GetDivisibleNumber(int NumberToCheck, int minNumber)
        {
            int min = minNumber;
            if (minNumber <= 2) min = 3;

            for (int i = min; i <= NumberToCheck; i++)
            {
                if ((NumberToCheck % i) == 0) return i;
            }
            return NumberToCheck;

        }



        //returns a random operator
        public List<string> GetOperator()
        {
            model = LevelController.LevelDifficulty(difficulties, level);
            int operatorCount = model.operators.Count;


            List<string> operatorInUse = new List<string>();
            for (int i = 0; i < model.operatorIndex; i++)
            {
                System.Random rnd = new System.Random();
                int index = rnd.Next(operatorCount);
                operatorInUse.Add(model.operators[index]);
            }


            return operatorInUse;
        }

        //checks for the current operator, solves the question and returns the answer
        public float GetCorrectAnswer(List<float> values, List<string> OperatorInUse)
        {
            float ans = 0;

            for (int i = 0; i < OperatorInUse.Count(); i++)
            {
                if (OperatorInUse[i] == "/")
                {
                    ans = values[i] / values[i + 1];
                    values[i] = ans;
                    values.Remove(values[i + 1]);
                    OperatorInUse.Remove(OperatorInUse[i]);
                    if (i >= 0) i--;
                }
            }

            for (int i = 0; i < OperatorInUse.Count; i++)
            {
                if (OperatorInUse[i] == "*")
                {
                    ans = values[i] * values[i + 1];
                    values[i] = ans;
                    values.Remove(values[i + 1]);
                    OperatorInUse.Remove(OperatorInUse[i]);
                    if (i >= 0)
                    {
                        i--;
                    }

                }
            }

            for (int i = 0; i < OperatorInUse.Count; i++)
            {
                if (OperatorInUse[i] == "+")
                {
                    ans = values[i] + values[i + 1];
                    values[i] = ans;
                    values.Remove(values[i + 1]);
                    OperatorInUse.Remove(OperatorInUse[i]);
                    if (i >= 0) i--;
                }
            }

            for (int i = 0; i < OperatorInUse.Count; i++)
            {
                if (OperatorInUse[i] == "-")
                {
                    ans = values[i] - values[i + 1];
                    values[i] = ans;
                    values.Remove(values[i + 1]);
                    OperatorInUse.Remove(OperatorInUse[i]);
                    if (i >= 0) i--;
                }
            }

            return ans;
        }

        public void CheckSolution(float solution)
        {
            if (solution == answer)
            {
                StartCoroutine(CorrectAnswer());
            }
            else
            {
                maxAnswerInput--;
                StartCoroutine(InCorrectAnswer());
                if (maxAnswerInput == 0)
                {
                    QuestionDisplayer.instance.questionBackground.SetActive(false);
                    UIManager.instance.gameOverHighScore.text = "Total Score : " + highScore;
                    UIManager.instance.SetGameOverPanelTrue();
                    UIManager.instance.sound.PlayOneShot(AudioManager.instance.gameoverSound);
                    TimeManager.instance.StopCoroutine(TimeManager.instance.CountDown());
                }
            }
        }

        //public void CorrectPuzzleCoroutine()
        //{
        //    StartCoroutine(OnCorrectPuzzleAnswer());
        //}

        //public void IncorrectPuzzleCoroutine()
        //{
        //    maxAnswerInput--;

        //    if (maxAnswerInput == 0)
        //    {
        //        UIManager.instance.puzzleQuestionBackground.SetActive(false);
        //        UIManager.instance.SetGameOverPanelTrue();
        //        UIManager.instance.sound.PlayOneShot(AudioManager.instance.gameoverSound);
        //        TimeManager.countDownTime = 0;
        //        UIManager.instance.gameOverHighScore.text = "Total Score : " + highScore;
        //    }
        //    else
        //    {
        //        StartCoroutine(OnIncorrectPuzzleAnswer());
        //    }
        //}



        //public void checkPuzzleAnswer(string answer)
        //{
        //    if (answer == PuzzleManager.instance.puzzleAnswer)
        //    { 
        //        StartCoroutine(OnCorrectPuzzleAnswer());
        //    }
        //    else
        //    {
        //        maxAnswerInput--;
        //        StartCoroutine(OnIncorrectPuzzleAnswer());
        //        if (maxAnswerInput == 0)
        //        {
        //            UIManager.instance.puzzleQuestionBackground.SetActive(false);
        //            UIManager.instance.SetGameOverPanelTrue();
        //            UIManager.instance.sound.PlayOneShot(AudioManager.instance.gameoverSound);
        //            TimeManager.countDownTime = 0;
        //            UIManager.instance.gameOverHighScore.text = "Total Score : " + highScore;
        //        }              
        //    }
        //}


        //public void AssignPuzzleButtons()
        //{
        //    UIManager.instance.answerButton.onClick.RemoveAllListeners();
        //    UIManager.instance.wrongButton1.onClick.RemoveAllListeners();
        //    UIManager.instance.wrongButton2.onClick.RemoveAllListeners();
        //    UIManager.instance.wrongButton3.onClick.RemoveAllListeners();
        //    UIManager.instance.answerButton.onClick.AddListener(delegate { checkPuzzleAnswer(PuzzleManager.instance.reshuffledData[0]); });
        //    UIManager.instance.wrongButton1.onClick.AddListener(delegate { checkPuzzleAnswer(PuzzleManager.instance.reshuffledData[1]); });
        //    UIManager.instance.wrongButton2.onClick.AddListener(delegate { checkPuzzleAnswer(PuzzleManager.instance.reshuffledData[2]); });
        //    UIManager.instance.wrongButton3.onClick.AddListener(delegate { checkPuzzleAnswer(PuzzleManager.instance.reshuffledData[3]); });



        //    //UIManager.instance.wrongButton1.onClick.RemoveAllListeners();
        //    //UIManager.instance.wrongButton1.onClick.AddListener(delegate { IncorrectPuzzleCoroutine(); });
        //    //UIManager.instance.wrongButton2.onClick.RemoveAllListeners();
        //    //UIManager.instance.wrongButton2.onClick.AddListener(delegate { IncorrectPuzzleCoroutine(); });
        //    //UIManager.instance.answerButton.onClick.AddListener(delegate { CorrectPuzzleCoroutine(); });
        //    //UIManager.instance.wrongButton3.onClick.RemoveAllListeners();
        //    //UIManager.instance.wrongButton3.onClick.AddListener(delegate { IncorrectPuzzleCoroutine(); });
        //    //UIManager.instance.proceedButton.onClick.AddListener(delegate { ClosePuzzlePanel(); });
        //    //UIManager.instance.previousButton.onClick.AddListener(delegate { GoBackToPuzzlePanel(); });           
        //}

        public void AssignButtonEvents()
        {
            System.Random rand = new System.Random();

            //answer options
            float option1;
            float option2;

            //check if the answer has a decimal point and then give other options a decimal point as well
            if ((answer / (int)answer) != 0)
            {
                // The options should be in the same range as the answer
                option1 = answer + rand.Next(-5, 5);
                option2 = answer + rand.Next(-5, 5);

                option1 += rand.Next(1, 9) / 10;
                option2 += rand.Next(1, 9) / 10;
            }
            else
            {
                // The options should be in the same range as the answer
                option1 = answer + rand.Next(-10, 10);
                option2 = answer + rand.Next(-10, 10);
            }


            float[] options = new float[]
            {
                answer,
                option1,
                option2
            };

            //if an answer occurs twice, generate another.
            if ((answer == options[1]) || (answer == options[2]) || (options[2] == options[1]))
            {
                AssignButtonEvents();
                return;
            }

            //shuffle the answers
            reshuffledOptions = options.OrderBy(x => rand.Next()).ToArray();

            QuestionDisplayer.instance.OptionBtn1.GetComponentInChildren<Text>().text = reshuffledOptions[0].ToString();
            QuestionDisplayer.instance.OptionBtn2.GetComponentInChildren<Text>().text = reshuffledOptions[1].ToString();
            QuestionDisplayer.instance.OptionBtn3.GetComponentInChildren<Text>().text = reshuffledOptions[2].ToString();

            QuestionDisplayer.instance.OptionBtn1.onClick.AddListener(delegate { CheckSolution(reshuffledOptions[0]); });
            QuestionDisplayer.instance.OptionBtn2.onClick.AddListener(delegate { CheckSolution(reshuffledOptions[1]); });
            QuestionDisplayer.instance.OptionBtn3.onClick.AddListener(delegate { CheckSolution(reshuffledOptions[2]); });

        }

        List<float> convertIntegersTofloat(List<int> integers)
        {
            List<float> floatingNumbers = new List<float>();

            for (int i = 0; i < integers.Count(); i++)
            {
                floatingNumbers.Add((float)integers[i]);
            }
            return floatingNumbers;
        }
    }

    [Serializable]
    class PlayerData
    {
        public int highScore;
        public int scene;
        //public EnumBase.GameChoice gameChoice;
        public int level;
        public EnumBase.Difficulties difficulties;
    }
}
