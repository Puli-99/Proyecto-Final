using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemigo : MonoBehaviour
{
       
    [Header("Variables propias de Enemigo para perseguir al jugador")]
    [SerializeField] float velocidad = 3f;
    [SerializeField] float rangoVision = 10f;
    [SerializeField] float outOfSightRange;
    [SerializeField] string nombreEscenaCombate = "EscenaCombate"; // Define la escena de combate
    [SerializeField] Transform Player;
    bool persiguiendo = false;

    [Header("Variables para Patrullaje")]
    [SerializeField] List<Vector3> nextPosition = new List<Vector3>();
    int positionIndex = 0;
    [SerializeField] float rotationSpeed = 3;
    float distanciaMinima = 0.1f;
    int ordenar;

    [Header("")]
    [Tooltip("Añadir ScripteableObject")]
    [SerializeField] EnemiesID enemiesID;


    [Header("Variables para transferir al enemigo en escena combate")]   
    [SerializeField] string enemyName = "Enemigo Genérico";
    [SerializeField] int health = 100;
    [SerializeField] int damage = 20;
    [SerializeField] int defense = 10;
    [SerializeField] string enemigoID;

    public EnemyData GetCombatData()
    {
        return new EnemyData
        {
            enemyName = this.enemyName,
            health = this.health,
            damage = this.damage,
            defense = this.defense,
            uniqueID = this.enemigoID,
        };
    }

    private void Start()
    {
        if (GameManager.Instance.defeatedEnemies.Contains(enemigoID))
        {
            gameObject.SetActive(false);
        }
        if (nextPosition.Count > 0)
        {
            transform.position = nextPosition[0];
        }
    }
    void Update()
    {
        DetectarPlayer();
        
        // Movimiento hacia el jugador si está en persecución
        if (persiguiendo)
        {
            transform.LookAt(Player);
            transform.position = Vector3.MoveTowards(transform.position, Player.position, velocidad * Time.deltaTime);
        }

        else
        {
            Patrolling();
        }
    }

    void Patrolling()
    {
        if (nextPosition.Count == 0) return;

        Vector3 objetivoActual = nextPosition[positionIndex];
        Vector3 direccion = (objetivoActual - transform.position).normalized;
        transform.position += direccion * velocidad * Time.deltaTime;

        if (direccion != Vector3.zero)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * rotationSpeed);
        }

        if (Vector3.Distance(transform.position, objetivoActual) <= distanciaMinima)
        {
            if (positionIndex == 0)
            {
                ordenar = 1;
            }
            else if (positionIndex == nextPosition.Count - 1)
            {
                ordenar = -1;
            }

            positionIndex += ordenar;
        }
    }

    void DetectarPlayer()
    {
        // Detección de visión
        Vector3 direccionPlayer = Player.position - transform.position;
        if (Vector3.Distance(transform.position, Player.position) <= rangoVision)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direccionPlayer.normalized, out hit, rangoVision))
            {
                if (hit.transform == Player)
                {
                    persiguiendo = true;
                    if (!GameManager.Instance.chasingEnemies.Contains(this))
                    {
                        GameManager.Instance.chasingEnemies.Add(this);
                    }
                }
            }
        }

        if (Vector3.Distance(transform.position, Player.position) >= outOfSightRange)
        {
            persiguiendo = false;
            GameManager.Instance.chasingEnemies.Remove(this);
        }
    }

    void OnTriggerEnter(Collider otro)
    {
        // Si el jugador entra en contacto con el enemigo, cambia a la escena de combate
        if (otro.CompareTag("Player"))
        {
            GameManager.Instance.returnPosition = otro.transform.position;
            SceneManager.LoadScene(nombreEscenaCombate);
        }
    }
}