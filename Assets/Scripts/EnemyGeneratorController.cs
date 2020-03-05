using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{
    public GameObject enemyPrefab;
    [Range(0f,20f)]
    public float generatorTimer;
    Vector3 tempPos;
    public float tiempo=0;
    public float aux=10;

    

    // Start is called before the first frame update
    void Start()
    {
        tempPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;
        if (tiempo > aux)
        {
            generatorTimer -= 0.1f;
            aux = aux + 10;
        }
        tempPos.y = Random.Range(-4f, 4f);
        
    }
    void CreateEnemy()
    {
        transform.position = tempPos;
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
       
    }
    public void StartGenerator()
    {
        ResetTimmer();
        InvokeRepeating("CreateEnemy", 0f, generatorTimer);
    }
    public void CancelGenerator()
    {
        CancelInvoke("CreateEnemy");
    }
    public void ResetTimmer()
    {
        generatorTimer = 0.5f;
    }
    public void ResetTiempo()
    {
        tiempo = 0;
    }
}
