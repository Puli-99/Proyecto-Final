using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemigo : MonoBehaviour
{

    //Variables propias de Enemigo para perseguir al jugador
    [SerializeField] float velocidad = 3f;
    [SerializeField] float rangoVision = 10f;
    [SerializeField] float outOfSightRange;
    [SerializeField] string nombreEscenaCombate = "EscenaCombate"; // Define la escena de combate
    [SerializeField] Transform Player;
    bool persiguiendo = false;


    //Variables para transferir al enemigo en escena combate
    public string enemyName = "Enemigo Genérico";
    public int health = 100;
    public int damage = 20;
    public int defense = 10;

    public EnemyData GetCombatData()
    {
        return new EnemyData
        {
            enemyName = this.enemyName,
            health = this.health,
            damage = this.damage,
            defense = this.defense
        };
    }


    void Update()
    {
        DetectarPlayer();
        
        // Movimiento hacia el jugador si está en persecución
        if (persiguiendo)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.position, velocidad * Time.deltaTime);
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
            Debug.Log(GameManager.Instance.chasingEnemies.Count);

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