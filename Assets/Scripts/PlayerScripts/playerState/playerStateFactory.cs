public class playerStateFactory
{
    playerStateMachine _context;

    public playerStateFactory(playerStateMachine currentContext){
        _context = currentContext;
    }

    public playerBaseState Idle(){
        return new playerIdleState(_context, this);
    }
    public playerBaseState Run(){
        return new playerRunState(_context, this);
    }
    public playerBaseState Jump(){
        return new playerJumpState(_context, this);
    }
    public playerBaseState Fall(){
        return new playerFallState(_context, this);
    }
    public playerBaseState Grounded(){
        return new playerGroundedState(_context, this);
    }
    public playerBaseState WallSlide(){
        return new playerWallSlideState(_context, this);
    }
    public playerBaseState WallJump(){
        return new playerWallJumpState(_context, this);
    }
    // public playerBaseState NotGrounded(){
    //     return new playerGroundedState(_context, this);
    // }
    public playerBaseState Attack1(){
        return new playerAttack1State(_context, this);
    }
    public playerBaseState AirAttack1(){
        return new playerAirAttack1State(_context, this);
    }
    public playerBaseState Parry(){
        return new playerParryState(_context, this);
    }
    public playerBaseState AirParry(){
        return new playerAirParryState(_context, this);
    }
}