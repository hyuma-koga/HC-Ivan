using UnityEngine;
using System.Collections;

public class FruitDropper : MonoBehaviour
{
    public GameObject   fruitPrefab;
    public Transform    spawnPoint;
    public FruitManager fruitManager;

    private FruitType   nextFruitType;
    private GameObject  standbyFruit;

    private void Start()
    {
        nextFruitType = GetRandomFruitType();
        CreateStandbyFruit();
    }

    private void Update()
    {
        if (standbyFruit != null)
        {
            standbyFruit.transform.position = spawnPoint.position;
        }

        if (Input.GetMouseButtonDown(0))
        {
            DropFruit();
        }
    }

    public void DropFruit()
    {
        if (standbyFruit == null)
        {
            return;
        }

        Rigidbody2D rb = standbyFruit.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;

        standbyFruit = null;

        StartCoroutine(SpawnNextFruitAfterDelay(0.4f));
    }

    private IEnumerator SpawnNextFruitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        //éüÇÃÉtÉãÅ[ÉcéÌóﬁÇåàíË
        nextFruitType = GetRandomFruitType();
        CreateStandbyFruit();
    }

    private void CreateStandbyFruit()
    {
        FruitData data = fruitManager.GetFruitData(nextFruitType);

        standbyFruit = Instantiate(fruitPrefab, spawnPoint.position, Quaternion.identity);

        FruitController fruitCtrl = standbyFruit.GetComponent<FruitController>() ;
        fruitCtrl.Init(data);

        Rigidbody2D rb = standbyFruit.GetComponent <Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private FruitType GetRandomFruitType()
    {
        int random = Random.Range(0, 5);
        return (FruitType)random;
    }
}
