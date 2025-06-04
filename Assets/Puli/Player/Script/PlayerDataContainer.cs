using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DataContainer
 public class PlayerDataContainer
 {
    public enum NotificationType { TookDamage, LifeHealed, Defense } //Creeating values of NotificationTypes

    public NotificationType Type; //Notification Type Var
    public int Value;            //Value Var

    public PlayerDataContainer(NotificationType type, int value) //Constructor which allows to create new PlayerDataContainers                                                                                
    {                                                           //With a Notification Type (LifeLost, Took Damage)
        Type = type;                                           //With a variable value type (life, damage, score, etc)
        Value = value;
    }
 }