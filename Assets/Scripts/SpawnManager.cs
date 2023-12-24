using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemy;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _spawnPos;
    [SerializeField] private GameObject _powerup;
    private bool _spawnWaveOne, _spawnWaveTwo, _spawnWaveThree, _spawnWaveFour;


    private void Start()
    {
        StartCoroutine(StartGameCoroutine());
        StartCoroutine(SpawnPowerupCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(3);
        _spawnWaveOne = true;
        StartCoroutine(EnemySpawn());
        StartCoroutine(WaveIncrease());
    }

    IEnumerator SpawnPowerupCoroutine()
    {
        while (true)
        {
            int randomRange = Random.Range(15, 40);
            Vector3 randomLocation = new Vector3(Random.Range(-6, 6), Random.Range(-3, 3), 0);
            yield return new WaitForSeconds(randomRange);
            Instantiate(_powerup, randomLocation, Quaternion.identity);
        }
    }

    IEnumerator EnemySpawn()
    {
        while (_spawnWaveOne == true)
        {
            //Spawns Enemy at random location
            int randomLocation = Random.Range(0, _spawnPos.Length);
            GameObject newEnemy = Instantiate(_enemy[0], _spawnPos[randomLocation].transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1f);
        }
        while (_spawnWaveTwo == true)
        {
            //Spawns Enemy at random location
            int randomLocation = Random.Range(0, _spawnPos.Length);
            int randomEnemy = Random.Range(0, 2);
            GameObject newEnemy = Instantiate(_enemy[randomEnemy], _spawnPos[randomLocation].transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(0.9f);
        }
        while (_spawnWaveThree == true)
        {
            //Spawns Enemy at random location
            int randomLocation = Random.Range(0, _spawnPos.Length);
            int randomEnemy = Random.Range(0, 3);
            GameObject newEnemy = Instantiate(_enemy[randomEnemy], _spawnPos[randomLocation].transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(0.8f);
        }
        while (_spawnWaveFour == true)
        {
            //Spawns Enemy at random location
            int randomLocation = Random.Range(0, _spawnPos.Length);
            int randomEnemy = Random.Range(0, _enemy.Length);
            GameObject newEnemy = Instantiate(_enemy[randomEnemy], _spawnPos[randomLocation].transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(0.8f);
        }
    }

    IEnumerator WaveIncrease()
    {
        yield return new WaitForSeconds(30f);
        Debug.Log("Wave 2");
        _spawnWaveOne = false;
        _spawnWaveTwo = true;
        yield return new WaitForSeconds(30f);
        _spawnWaveTwo = false;
        _spawnWaveThree = true;
        Debug.Log("Wave 3");
        yield return new WaitForSeconds(30f);
        _spawnWaveThree = false;
        _spawnWaveFour = true;
        Debug.Log("Wave 4");
    }
}
