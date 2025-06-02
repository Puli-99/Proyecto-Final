using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // define los posibles turnos en el combate
    private enum Turno { Player, Enemigo }
    
    // Variable que almacena quién tiene el turno actualmente
    private Turno turnoActual = Turno.Player;

    void Start()
    {
        // Se ejecuta al inicio de la escena y establece el turno inicial
        turnoActual = Turno.Player; // Comienza con el turno del jugador
    }

    public void EjecutarAccionJugador()
    {
        // Esta función se ejecuta cuando el jugador realiza una acción, como atacar
        Debug.Log("El jugador ataca!");
        CambiarTurno(); // Cambia el turno al enemigo
    }

    void CambiarTurno()
    {
        // Alterna el turno entre el jugador y el enemigo
        turnoActual = (turnoActual == Turno.Player) ? Turno.Enemigo : Turno.Player;
        
        // Si es el turno del enemigo, ejecuta su ataque automáticamente
        if (turnoActual == Turno.Enemigo)
        {
            RealizarAtaqueEnemigo();
        }
    }

    void RealizarAtaqueEnemigo()
    {
        // Esta función simula el ataque del enemigo
        Debug.Log("El enemigo ataca!");

        // Después del ataque del enemigo, el turno vuelve al jugador
        CambiarTurno();
    }
}