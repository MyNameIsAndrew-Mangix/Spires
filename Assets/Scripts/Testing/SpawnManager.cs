using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _testDummyPrefab;
    [SerializeField] private bool _testBool = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TestSpawningRoutine());
    }


    IEnumerator TestSpawningRoutine()
    {
        yield return new WaitForSeconds(2);
        while (_testBool)
        {
            float randomX = Random.Range(-7, 7);
            Vector3 posToSpawn = new Vector3(randomX, 3);
            Instantiate<GameObject>(_testDummyPrefab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }
}
