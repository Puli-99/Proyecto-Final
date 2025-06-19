using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour, IObserverEnemy
{
    public static CombatManager Instance { get; private set; }

    public List<EnemyUIData> enemyUIList = new List<EnemyUIData>();

    //Variables lógicas
    [SerializeField] PlayerButtons playerButtons; //Referencia para hacer daño. Se ejecuta en PlayerDealsDamage
    [SerializeField] PlayerLife playerLife; //Referencia del player para que el enemigo pueda hacerle daño
    [SerializeField] int sceneToReturn; //Escena a volver si el jugador gana el combate
    [SerializeField] public List<BaseEnemy> enemiesAlive = new List<BaseEnemy>(); //Lista de enemigos en escena para determinar si estan todos vivos o no.


    //Variables de translación de datos
    [SerializeField] GameObject baseEnemyPrefab; //Prefab para instanciar enemigos según la cantidad de enemigos que persiguieron al player.
    public List<EnemyData> currentEnemies = new List<EnemyData>(); //Lista de enemigos que persiguieron al Player hasta que fue llevado a la escena de combate


    //Variables para cada enemigo cuando es seleccionado
    [SerializeField] public BaseEnemy selectedEnemy;
    [SerializeField] TMP_Text selectedEnemyHealthText;
    [SerializeField] TMP_Text selectedEnemyDamageText;
    [SerializeField] TMP_Text selectedEnemyDefenseText;

    //Variables para ubicar los botones y prefabs.
    [SerializeField] GameObject EnemyStatsHUD;
    [SerializeField] public GameObject enemyButtonPrefab;
    [SerializeField] Transform buttonParent;
    [SerializeField] Transform prefabParent;

    //Variables para que el player spawnee en un lugar específico en la escena de combate y no en ReturnPosition
    [SerializeField] Transform dontgoaway;
    [SerializeField] Vector3 posicion = new Vector3(-7f, 1f, -12.5f);



    void Awake()
    {
        Instance = this;
    }

    // Define los posibles turnos en el combate
    private enum Turno { Player, Enemigo }
    
    // Variable que almacena quién tiene el turno actualmente
    private Turno turnoActual = Turno.Player;

    void Start()
    {
        PrepareCombatEnemies();
        CreateEnemies();     
        turnoActual = Turno.Player;

        //Transform del player en donde queremos
        dontgoaway.position = posicion;
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

            GameObject newButton = Instantiate(enemyButtonPrefab, buttonParent.position + buttonOffset * 50, Quaternion.identity, buttonParent);
            newButton.GetComponentInChildren<TMP_Text>().text = data.enemyName;

            Button btn = newButton.GetComponent<Button>();
            btn.onClick.AddListener(() => SelectEnemy(combatScript));

            enemyUIList.Add(new EnemyUIData
            {
                enemy = combatScript,
                button = newButton
            });

            i++; // Avanzamos el índice
        }
    }
    public void PrepareCombatEnemies()
    {      
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
    public void OnNotify(BaseEnemy sourceEnemy, EnemyDataContainer amount)
    {
        if (amount.Type == EnemyDataContainer.NotificationType.TookDamage)
        {
            UpdateHUD(sourceEnemy);
        }
    }
    void UpdateHUD(BaseEnemy enemy)
    {
        selectedEnemy = enemy;
        selectedEnemyHealthText.text = "Vida: " + enemy.GetHealth();
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

    public void OnEnemyDefeated(BaseEnemy defeatedEnemy)
    {
        // Buscar en la lista al enemigo
        EnemyUIData uiData = enemyUIList.FirstOrDefault(e => e.enemy == defeatedEnemy);
        if (uiData != null)
        {
            uiData.button.SetActive(false); // Desactiva el botón
        }
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
               GameManager.Instance.chasingEnemies.Clear();
               SceneManager.LoadScene(sceneToReturn);
           }       
    }
}