using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public bool canBePopped; // Determines if the enemy can be popped
    public float speed; // Movement speed of the enemy
    public float size; // Size of the enemy
    public int pointsOnPop; // Points awarded if popped

}
