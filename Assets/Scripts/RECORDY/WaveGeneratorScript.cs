using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class WaveGeneratorScript : MonoBehaviour
{
    [Header("EnemyPrefab")]
    [SerializeField] private GameObject _enemy;
    
    [Header("EnemyCoordinates")]
    [SerializeField] private float _spawnX; // координата X точки спавна врагов 
    [SerializeField] private float _spawnY; // координата Y точки спавна врагов 
    [SerializeField] private Transform spawner;
    [SerializeField] private Transform spawner1;

    [Header("UpgradeCanvas")]
    [SerializeField] private Canvas _UpgradeMenuCanvas;

    [Header("CollDownUpgrade")]
    [SerializeField] private Sprite _noMoreUpgrade;
    [SerializeField] private Button _coolDownButton;
    [SerializeField] private Text _coolDownText;

    private float _enemySpawnDelay = 2f;
    private float _bonusSelectConditionDelay = 1f;

    private void Start()
    {
        SpawnWaveFuncCall();
    }

    private void Update()
    {
        _bonusSelectConditionDelay -= Time.deltaTime;

        if(_bonusSelectConditionDelay <= 0 && playerScript.EnemiesCount <= 0)
        {
            ChooseUpgrade();
        }

        if(Shoot.timeBetweenShots == 1)
        {
            _coolDownButton.image.sprite = _noMoreUpgrade;
            _coolDownText.text = "No more cool down upgrade!";
        }
    }

    public void SpawnWaveFuncCall()
    {
        SpawnWave(_enemySpawnDelay, _enemy);
    }

    private IEnumerator SpawnEnemy(GameObject enemy, int enemiesCount)
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            yield return new WaitForSeconds(_enemySpawnDelay);

            if (i % 2 == 0) // Четные враги на первом спавнере
            {
                GameObject newEnemy = Instantiate(enemy, new Vector2(spawner.transform.position.x, spawner.transform.position.y), Quaternion.identity);
                newEnemy.SetActive(true);
            }
            else // Нечетные враги на втором спавнере
            {
                GameObject newEnemy1 = Instantiate(enemy, new Vector2(spawner1.transform.position.x, spawner1.transform.position.y), Quaternion.identity);
                newEnemy1.SetActive(true);
            }
        }
    }

    private void SpawnWave(float enemySpanwDelay,GameObject enemy)
    {
        _bonusSelectConditionDelay = 1f;
        playerScript.currentWave++; // Счётчик волн
        playerScript.EnemiesCount = 1 + playerScript.currentWave * 2; // Расчёт количества врагов

        StartCoroutine(SpawnEnemy(enemy, playerScript.EnemiesCount));
    }

    public static void DecreaseEnemiesCount()
    {
        playerScript.EnemiesCount--;
    }
  
    public void UpdrageDMg()
    {
        enemy.dmg += 0.5f;
        Time.timeScale = 1;
        _UpgradeMenuCanvas.gameObject.SetActive(false);
        SpawnWaveFuncCall();
        Debug.Log(enemy.dmg);
    }

    public void UpdrageHp()
    {
        GameObject[] aboba = GameObject.FindGameObjectsWithTag("Summon");

        foreach (GameObject enemyes in aboba)
        {
            enemy enemyStats = enemyes.GetComponent<enemy>();
            enemyStats.EnemyCurrentHp = enemyStats.EnemyMaxHp;
        }

        Time.timeScale = 1;
  
        _UpgradeMenuCanvas.gameObject.SetActive(false);
        SpawnWaveFuncCall();
    }

    public void UpdrageHpPlayer()
    {
        playerScript.movSpeed += 2;
        Time.timeScale = 1;
        _UpgradeMenuCanvas.gameObject.SetActive(false);
        SpawnWaveFuncCall();
    }

    public void UpdrageStunColldwon()
    {
        if(Shoot.timeBetweenShots > 1)
        {
            Shoot.timeBetweenShots -= 1;
            Time.timeScale = 1;
            _UpgradeMenuCanvas.gameObject.SetActive(false);
            SpawnWaveFuncCall();
        }
    }

    public void ChooseUpgrade()
    {
       _UpgradeMenuCanvas.gameObject.SetActive(true);
       Time.timeScale = 0;
    }
}