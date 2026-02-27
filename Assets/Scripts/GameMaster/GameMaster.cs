using UnityEngine;
using UnityEngine.UI;
using CamelInvaders.Entity.AI.Enemy;
using CamelInvaders.ScriptableObjects;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

namespace CamelInvaders.GameMaster
{
    [RequireComponent(typeof(AudioSource))]
    public class GameMaster : MonoBehaviour
    {
        public static GameMaster Instance {get ; private set; } 
        private Camera cam;
        private Vector3 direction = Vector3.right;
        private uint _waveIndex = 0;
        private bool _isSpawningEnemies = false;
        private bool _isGamePaused = false;
        private bool _finalSoundPlayed = false;
        private Vector3 originalSpawnpointPosition;

        [Header("LEVEL SETTINGS")]
        [SerializeField] private Transform EnemySpawnPoint;
        [SerializeField] private WaveScriptableObject[] Waves;
        [SerializeField] private float Spacing = 2.0f;
        [SerializeField] private float EnemySpeed = 2.0f;
        [SerializeField] private float TimeBetweenWaves = 2.0f;
        [SerializeField] private float PlayerMaxHealth = 200;
        [SerializeField] private float PlayerMaxShield = 100;

        [Space(10f)]

        [Header("REFERENCES")]
        [SerializeField] private Slider HealthBar;
        [SerializeField] private Slider ShieldBar;
        [SerializeField] private GameObject WinScreen;
        [SerializeField] private GameObject LoseScreen;
        [SerializeField] private GameObject PauseScreen;
        [SerializeField] private TMP_Text WaveText;
        [SerializeField] private AudioSource audioSource;

        [Space(10f)]
        [Header("AUDIO CLIPS")]
        [SerializeField] private AudioClip LoseAudioClip;
        [SerializeField] private AudioClip WinAudioClip;
        
        
        
        private void Awake()
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            {
                if(EnemySpawnPoint == null)
                {
                    Debug.LogError("ERROR : NULL SPAWN POINT!");
                    return;
                }

                cam = Camera.main;

                Instance = this;
                cam = Camera.main;
                EnemySpawnPoint = GameObject.FindGameObjectWithTag("EnemySpawnPoint").transform;
                originalSpawnpointPosition = EnemySpawnPoint.position;
                StartCoroutine(DoNextWave());

                WinScreen.SetActive(false);
                LoseScreen.SetActive(false);

                ShieldBar.gameObject.SetActive(true);
                HealthBar.gameObject.SetActive(true);

                HealthBar.maxValue = PlayerMaxHealth;
                ShieldBar.maxValue = PlayerMaxShield;

                Time.timeScale = 1;
            }
            
        }
        private IEnumerator DoNextWave()
        {
            _isSpawningEnemies = true;
            WaveText.text = "WAVE" + (_waveIndex+1);
            WaveText.alpha = 100;
            yield return new WaitForSeconds(TimeBetweenWaves);
            WaveText.alpha = 0;
            SpawnEnemies(_waveIndex);
        }

        private void SpawnEnemies(uint waveIndex)
        {
            EnemySpawnPoint.position = originalSpawnpointPosition;

            if(waveIndex <= Waves.Length )
            {
                for(int row = 0;row < Waves[waveIndex].Rows;row++)
                {
                    float width = Spacing * (Waves[waveIndex].Columns - 1);
                    float height = Spacing * (Waves[waveIndex].Rows - 1);

                    Vector2 centering = new Vector2(-width/2 , -height /2);
                    Vector3 rowPosition = new Vector3(centering.x,centering.y + (row * Spacing) , 0.0f);

                    for(int col = 0;col < Waves[waveIndex].Columns ; col++)
                    {
                        Enemy enemy = Instantiate(Waves[_waveIndex].Enemies[row] , EnemySpawnPoint);
                        Vector3 position = rowPosition;
                        position.x += col*Spacing;
                        enemy.transform.localPosition = position;
                    }
                }
            }

            _isSpawningEnemies = false;
        }

        
        private void Update()
        {
            HandleGameStates();
            HandleEnemies();
        }

        private void HandleEnemies()
        {
            if(EnemySpawnPoint.childCount == 0 && _waveIndex >= Waves.Length) return;

            EnemySpawnPoint.transform.position += direction * EnemySpeed * Time.deltaTime;

            Vector3 leftEdge = cam.ViewportToWorldPoint(Vector3.zero);
            Vector3 rightEdge = cam.ViewportToWorldPoint(Vector3.right);

            foreach (Transform invader in EnemySpawnPoint)
            {
                if(invader == null)     return;

                if(direction == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f))
                {
                    AdvanceRow();
                }else if(direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f))
                {
                    AdvanceRow();
                }

                if(invader.position.y <= leftEdge.y + 5.0f)
                {
                    PlayersLose();
                }
            }
        }

        private void HandleGameStates()
        {
            if(EnemySpawnPoint.childCount == 0)
            {
                if(_waveIndex >= Waves.Length - 1 && !_isSpawningEnemies)
                {
                    PlayerWin();
                    return;
                }else if(!_isSpawningEnemies)
                {
                    if(Waves[_waveIndex].MysteryShip != null)
                    {
                        Vector3 rightEdge = cam.ViewportToWorldPoint(Vector3.right + Vector3.up);
                        rightEdge.y -= 2f;
                        rightEdge.x += 10f;
                        rightEdge.z = 0;

                        Instantiate(Waves[_waveIndex].MysteryShip , rightEdge , Quaternion.identity);
                    }

                    ++_waveIndex;
                    StartCoroutine(DoNextWave());
                }
            }
        }

        private void AdvanceRow()
        {
            direction.x *= -1.0f;

            Vector3 position = EnemySpawnPoint.position;
            position.y-= 1.0f;
            EnemySpawnPoint.position = position;
        }

        public bool IsGamePaused()
        {
            return _isGamePaused;
        }

        public void PlaySound(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }

        #region UIMethods

        public void UpdateMaxHealthUI(float amount)
        {
            
        }
        public void UpdateHealthUI(float amount)
        {
            HealthBar.value = amount;
        }

        public void UpdateMaxShieldUI(float amount)
        {
            
        }
        public void UpdateShieldUI(float amount)
        {
            ShieldBar.value = amount;
        }

        private void PlayerWin()
        {
            if(!_finalSoundPlayed)
            {
                PlaySound(WinAudioClip);
                _finalSoundPlayed = true;
            }
            WinScreen.SetActive(true);
        }

        public void PlayersLose()
        {
            if(!_finalSoundPlayed)
            {
                PlaySound(LoseAudioClip);
                _finalSoundPlayed = true;
            }
            LoseScreen.SetActive(true);
        }

        public void HandleGamePause()
        {
            _isGamePaused = !_isGamePaused;
            PauseScreen.SetActive(_isGamePaused);

            if(_isGamePaused)   Time.timeScale = 0;
            else                Time.timeScale = 1;
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void Retry()
        {
            Debug.Log("RETRY");
            SceneManager.LoadScene(1);

            WinScreen.SetActive(false);
            LoseScreen.SetActive(false);
        }

        #endregion
    }
}

#region Interfaces

namespace CamelInvaders.Entity
{
    public interface IDamageable
    {
        public void TakeDamage(float amount);
    }
}

namespace CamelInvaders.Entity.Buffs
{
    public interface IBuffable
    {
        public void Heal(float amount);
        public void IncreaseMaxHealth(float amount);
        public void IncreaseMaxShield(float amount);

        public void BuffLaserFireRate(float newFireRate);
        public void BuffRocketFireRate(float newFireRate);
    }
}

#endregion