using UnityEngine;

public static class MovementHelper
{
    /// <summary>
    /// Checks if the given gameobject is touching the ground in the given direction
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="gravityDirection"></param>
    /// <returns></returns>
    public static bool IsGrounded(GameObject entity, Vector2 gravityDirection)
    {
        Collider2D hitbox = entity.GetComponent<Collider2D>();
        return Physics2D.BoxCast(entity.transform.position, new Vector2(hitbox.bounds.size.x, hitbox.bounds.size.y), 0, gravityDirection, 0.2f, LayerMask.GetMask("Ground"));
    }
}
