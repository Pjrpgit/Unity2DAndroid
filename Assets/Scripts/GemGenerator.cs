using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gemaPrefab;
    [Range(0f, 20f)]
    public float generatorTimer;
    Vector3 tempPos;

    void Start()
    {
        tempPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        tempPos.y = Random.Range(-4f, 4f);
        tempPos.x = Random.Range(-4f, 4f);
    }
    void CreateGema()
    {
        transform.position = tempPos;
        Instantiate(gemaPrefab, transform.position, Quaternion.identity);

    }
    public void StartGenerator()
    {
        InvokeRepeating("CreateGema", 0f, generatorTimer);
    }
    public void CancelGenerator()
    {
        CancelInvoke("CreateGema");
        Destroy(gameObject);
    }
}
