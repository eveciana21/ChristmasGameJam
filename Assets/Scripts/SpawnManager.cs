using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemy;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _spawnPos;
    [SerializeField] private GameObject _present;
    private bool _spawnWaveOne, _spawnWaveTwo, _spawnWaveThree;


    private void Start()
    {
        SpawnPresents();
        _spawnWaveOne = true;
        StartCoroutine(EnemySpawn());
        StartCoroutine(WaveIncrease());
    }

    private void SpawnPresents()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 presentSpawnLocation = new Vector3(Random.Range(-2, 2), (Random.Range(-2, 2)), 0);
            Instantiate(_present, presentSpawnLocation, Quaternion.identity);

            //spawns 3 presents randomly in a 4x4 square in the middle. Called once in Start
            //note: left presents unparented outside a container since enemy is going to make them their child to move
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
            yield return new WaitForSeconds(1f);
        }
        while (_spawnWaveThree == true)
        {
            //Spawns Enemy at random location
            int randomLocation = Random.Range(0, _spawnPos.Length);
            int randomEnemy = Random.Range(0, _enemy.Length);
            GameObject newEnemy = Instantiate(_enemy[randomEnemy], _spawnPos[randomLocation].transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1f);
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
    }
}
