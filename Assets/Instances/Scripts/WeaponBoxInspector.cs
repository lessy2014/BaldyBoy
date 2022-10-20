using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponBoxInspector : MonoBehaviour
{
    [SerializeField] private GameObject Weapon;
    [SerializeField] private List<Transform> transformSpawnPoints = new List<Transform>();
    private List<Vector3> SpawnPoints;

    public void Start()
    {
        SpawnPoints = transformSpawnPoints.Select(x => x.position).ToList();
        StartCoroutine(SpawnWeaponContinuously());
    }

    IEnumerator SpawnWeaponContinuously()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if (SpawnPoints.Count == 0) continue;
            var spawnPointPos = SpawnPoints[Random.Range(0, SpawnPoints.Count)];
            var weapon = Instantiate(Weapon, spawnPointPos, new Quaternion());
            SpawnPoints.Remove(spawnPointPos);
        }
    }

    public void PickedUpAt(Vector3 position)
    {
        SpawnPoints.Add(position);
    }
}
