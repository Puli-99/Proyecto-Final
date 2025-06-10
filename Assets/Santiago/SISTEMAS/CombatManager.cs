using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    [SerializeField] PlayerButtons playerButtons;
    [SerializeField] PlayerLife playerLife; //Referencia del player para que el enemigo pueda hacerle daño
    [SerializeField] int sceneToReturn; //Escena a volver si el jugador gana el combate
    [SerializeField] GameObject baseEnemyPrefab; //Prefab para instanciar enemigos según la cantidad de enemigos que persiguieron al player.
    [SerializeField] public List<BaseEnemy> enemiesAlive = new List<BaseEnemy>(); //Lista de enemigos en escena para determinar si estan todos vivos o no. 
    BaseEnemy enemy;
    public List<EnemyData> currentEnemies = new List<EnemyData>(); //Lista de enemigos que persiguieron al Player hasta que fue llevado a la escena de combate
    [SerializeField] GameObject EnemyStatsHUD;


    [SerializeField] GameObject enemyButtonPrefab;
    [SerializeField] BaseEnemy selectedEnemy;

    //Textos para cada enemigo cuando es seleccionado
    [SerializeField] TMP_Text selectedEnemyHealthText;
    [SerializeField] TMP_Text selectedEnemyDamageText;
    [SerializeField] TMP_Text selectedEnemyDefenseText;

    //Transforms para ubicar a los botones y prefabs. (Faltaría agregar un offsett para que no spawneen en el exacto mismo lugar y no se apilen)
    [SerializeField] Transform buttonParent;
    [SerializeField] Transform prefabParent;
    Button btn;




    // Define los posibles turnos en el combate
    private enum Turno { Player, Enemigo }
    
    // Variable que almacena quién tiene el turno actualmente
    private Turno turnoActual = Turno.Player;

    void Start()
    {
        PrepareCombatEnemies();
        CreateEnemies();     
        turnoActual = Turno.Player;
    }

    void CreateEnemies()
    {
        int count = currentEnemies.Count;
        float radius = 3f;
        int i = 0;

        foreach (EnemyData data in currentEnemies)
        {
            // Calculamos offset circular para este enemigo
            float angle = i * Mathf.PI * 2 / count;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            Vector3 offset = new Vector3(x, 0, z);
            Vector3 buttonOffset = new Vector3(x, y, 0);

            // Instanciamos el enemigo en la posición desplazada
            GameObject newEnemy = Instantiate(baseEnemyPrefab, prefabParent.position + offset, Quaternion.identity, prefabParent);

            // Asignamos los datos del enemigo original
            BaseEnemy combatScript = newEnemy.GetComponent<BaseEnemy>();
            combatScript.Setup(data);
            enemiesAlive.Add(combatScript);

            // Creamos el botón correspondiente al enemigo
            GameObject newButton = Instantiate(enemyButtonPrefab, buttonParent.position + buttonOffset * 50, Quaternion.identity, buttonParent);
            newButton.GetComponentInChildren<TMP_Text>().text = data.enemyName;

            Button btn = newButton.GetComponent<Button>();
            btn.onClick.AddListener(() => SelectEnemy(combatScript));

            i++; // Avanzamos el índice
        }
    }
    public void PrepareCombatEnemies()
    {
        currentEnemies.Clear();

        foreach (Enemigo enemigo in GameManager.Instance.chasingEnemies)
        {
            currentEnemies.Add(enemigo.GetCombatData());
        }
    }
    public void SelectEnemy(BaseEnemy enemy)
    {
        if (!EnemyStatsHUD.activeInHierarchy)
        {
            EnemyStatsHUD.SetActive(true);
        }
        selectedEnemy = enemy;
        selectedEnemyHealthText.text = "Vida: " + enemy.GetHealth();
        selectedEnemyDamageText.text = "Daño: " + enemy.GetDamage();
        selectedEnemyDefenseText.text = "Defensa: " + enemy.GetDefense();
    }
    


    public void EjecutarAccionJugador() //Lo ejecuta cada botón del player
    {
        FinishCombat();
        CambiarTurno(); // Cambia el turno al enemigo
    }

    public void PlayerDealsDamage() //Lo Ejecuta PlayerButton en Attack
    {
        if (selectedEnemy == null) return;
        selectedEnemy.TakeDamage(playerButtons.GetDamage()); // Daño del jugador
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

    void EnemyTurn() //Falta Introducir una Corrutina para que no sea inmediato y agregar animaciones, audio, etc
    {
        FinishCombat();

        BaseEnemy[] enemigos = FindObjectsOfType<BaseEnemy>(); //Cada enemigo que hay en la escena ataca.
        foreach (BaseEnemy enemy in enemigos)
        {
            if (enemy.isActiveAndEnabled)
            {
                enemy.EnemyAttack(playerLife);
            }
        }

        // Después del ataque del enemigo, el turno vuelve al jugador
        CambiarTurno();
    }

  

    void FinishCombat() //Chequea si queda algun enemigo vivo en escena, si no hay ninguno vivo, volvemos a la escena sceneToReturn (Editar donde corresponda en el motor)
    {
           bool anyAlive = false;
           foreach (BaseEnemy enemies in enemiesAlive)
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