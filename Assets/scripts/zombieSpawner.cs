using UnityEngine;
using UnityEngine.AI;

public class zombieSpawner : MonoBehaviour
{
    public GameObject zombie;

    public Vector2 xRange = new Vector2(300f, 650f);
    public Vector2 zRange = new Vector2(300f, 650f);

    public float spawnRate = 5f;
    public float sampleMaxDist = 100f;   // Yükseklik farklarýný kapsayacak kadar BÜYÜK
    public int maxTries = 8;

    float sayac;

    void Start() => sayac = spawnRate;

    void Update()
    {
        sayac -= Time.deltaTime;
        if (sayac <= 0f)
        {
            TrySpawnOnNavMesh();
            sayac = spawnRate;
        }
    }

    void TrySpawnOnNavMesh()
    {
        for (int i = 0; i < maxTries; i++)
        {
            // Y'yi kafana göre verebilirsin; önemli olan alttaki SamplePosition yarýçapýnýn büyük olmasý
            Vector3 guess = new Vector3(
                Random.Range(xRange.x, xRange.y),
                70f, // tepe noktalarýn civarý olabilir
                Random.Range(zRange.x, zRange.y)
            );

            if (NavMesh.SamplePosition(guess, out var hit, sampleMaxDist, NavMesh.AllAreas))
            {
                var go = Instantiate(zombie, hit.position, Quaternion.identity);

                // Garanti olsun:
                var agent = go.GetComponent<NavMeshAgent>();
                if (agent) agent.Warp(hit.position);

                Debug.Log("zombie olustu @" + hit.position);
                return;
            }
        }

        Debug.LogWarning("Uygun NavMesh bulunamadý; aralýklarý/bake'i kontrol et.");
    }
}
