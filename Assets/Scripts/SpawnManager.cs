using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _spawnPos;

    private void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        while (true)
        {
            //Spawns Enemy at random location
            int randomLocation = Random.Range(0, _spawnPos.Length);
            GameObject newEnemy = Instantiate(_enemy, _spawnPos[randomLocation].transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2f);
        }
    }
}
