using UnityEngine;

public class MinionController : BaseMonster
{
    // Không cần override Start và Update nếu không thêm logic gì
    
    // Chỉ override khi cần custom behavior
    protected override void OnDeath()
    {
        Debug.Log("-----Minion Death-----");
    }
    
    // Nếu cần custom death animation hoặc effects
    protected override void PlayDeathAnimation()
    {
        base.PlayDeathAnimation();
        // Thêm effects đặc biệt cho minion nếu cần
    }
}