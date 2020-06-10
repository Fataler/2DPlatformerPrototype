using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Model/Character Data")]
public class CharacterData : ScriptableObject
{
    public float movementSpeed = 150f;
    public float normalColliderHeight = 2f;
    public float jumpForce = 10f;

    //public float diveForce = 30f;
    //public float diveCooldownTimer = 0.25f;
}