using UnityEngine;

public class PlayerSneakState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("I'm Sneaking!");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //What are we doing during this state?
        player.MovePlayer(player.default_speed / 2);

        //On what conditions do we leave the state?
        if (player.movement.magnitude < 0.1)
        {
            player.SwitchState(player.idleState);
        }
        else if (player.isSneaking == false)
        {
            player.SwitchState(player.walkState);
        }
    }
}
