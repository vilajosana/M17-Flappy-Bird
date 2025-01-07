using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesPool : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private int poolSize = 5; // Número fijo de pipes en el pool
    [SerializeField] private float spawnTime = 2.5f; // Tiempo entre apariciones
    [SerializeField] private float xSpawnPosition = 12f; // Posición X constante de aparición
    [SerializeField] private float minYPosition = -2f; // Límite inferior de posición Y
    [SerializeField] private float maxYPosition = 3f; // Límite superior de posición Y

    private float timeElapsed; // Temporizador para las apariciones
    private int obstacleIndex; // Índice del pool para reutilizar pipes
    private GameObject[] obstacles; // Array de pipes

    void Start()
    {
        // Inicializa el pool de obstáculos
        obstacles = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            obstacles[i] = Instantiate(obstaclePrefab);
            obstacles[i].SetActive(false); // Desactiva inicialmente
        }
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > spawnTime && !GameManager.Instance.isGameOver)
        {
            SpawnObstacle(); // Genera un nuevo obstáculo
        }
    }

    private void SpawnObstacle()
    {
        timeElapsed = 0f; // Reinicia el temporizador

        // Calcula una posición Y aleatoria dentro de los límites especificados
        float ySpawnPosition = Random.Range(minYPosition, maxYPosition);
        Vector2 spawnPosition = new Vector2(xSpawnPosition, ySpawnPosition);

        // Reutiliza un obstáculo del pool
        GameObject obstacle = obstacles[obstacleIndex];
        obstacle.transform.position = spawnPosition; // Asigna la nueva posición
        obstacle.SetActive(true); // Activa el obstáculo

        // Incrementa el índice del pool
        obstacleIndex++;
        if (obstacleIndex >= poolSize)
        {
            obstacleIndex = 0; // Reinicia el índice si alcanza el tamaño del pool
        }
    }
}
