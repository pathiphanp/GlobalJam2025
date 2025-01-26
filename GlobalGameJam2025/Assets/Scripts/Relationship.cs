using UnityEngine;
public enum Srcoll
{
    Most,Middle,Low,None
}
[CreateAssetMenu(fileName = "DataRelationship", menuName = "Scriptable Objects/DataRelationship")]
public class Relationship : ScriptableObject
{
    public Srcoll grandfather;
    public Srcoll dad;
    public Srcoll mom;
    public Srcoll son;
    public Srcoll daughter;
    public Srcoll steward;
    public Srcoll hunter;
    public Srcoll assisHunter;
}
