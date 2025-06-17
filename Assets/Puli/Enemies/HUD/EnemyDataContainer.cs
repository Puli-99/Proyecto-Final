
//DataContainer
public class EnemyDataContainer
{
    public enum NotificationType { TookDamage } //Creeating values of NotificationTypes

    public NotificationType Type; //Notification Type Var
    public int Value;            //Value Var

    public EnemyDataContainer(NotificationType type, int value) //Constructor which allows to create new PlayerDataContainers                                                                                
    {                                                           //With a Notification Type (LifeLost, Took Damage)
        Type = type;                                           //With a variable value type (life, damage, score, etc)
        Value = value;
    }
}