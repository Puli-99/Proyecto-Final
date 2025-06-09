using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{

    [SerializeField] PlayerLife playerLife;
    [SerializeField] int sceneToReturn;
    [SerializeField] GameObject baseEnemyPrefab; 
    BaseEnemy[] enemy;
    public List<EnemyData> currentEnemies = new List<EnemyData>();

 
    // Define los posibles turnos en el combate
    private enum Turno { Player, Enemigo }
    
    // Variable que almacena quién tiene el turno actualmente
    private Turno turnoActual = Turno.Player;

    void Start()
    {
        PrepareCombatEnemies();

        foreach (EnemyData data in currentEnemies)
        {
            GameObject newEnemy = Instantiate(baseEnemyPrefab); // Instanciás un nuevo enemigo
            BaseEnemy combatScript = newEnemy.GetComponent<BaseEnemy>(); // Obtenés el script
            combatScript.Setup(data); // Aplicás los datos (vida, ataque, defensa, etc.)
        }

        turnoActual = Turno.Player;
    }


    public void PrepareCombatEnemies()
    {
        currentEnemies.Clear();

        foreach (Enemigo enemigo in GameManager.Instance.chasingEnemies)
        {
            currentEnemies.Add(enemigo.GetCombatData());
        }
    }


    public void EjecutarAccionJugador()
    {
        // Esta función se ejecuta cuando el jugador realiza una acción, como atacar
        CambiarTurno(); // Cambia el turno al enemigo
        FinishCombat();
    }

    void CambiarTurno()
    {
        // Alterna el turno entre el jugador y el enemigo
        turnoActual = (turnoActual == Turno.Player) ? Turno.Enemigo : Turno.Player;
        
        // Si es el turno del enemigo, ejecuta su ataque automáticamente
        if (turnoActual == Turno.Enemigo)
        {
            EnemyTurn();
        }
    }

    void EnemyTurn()
    {
        //enemy.EnemyAttack(playerLife);

        // Después del ataque del enemigo, el turno vuelve al jugador
        CambiarTurno();
    }

  

    void FinishCombat()
    {
           bool anyAlive = false;
           foreach (BaseEnemy enemies in enemy)
           {
               if (enemies.isActiveAndEnabled)
               {
                   anyAlive = true;                    
               }
           }

           if (!anyAlive)
           {
               SceneManager.LoadScene(sceneToReturn);
           }       
    }
}