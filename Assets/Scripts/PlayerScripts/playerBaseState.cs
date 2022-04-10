public abstract class playerBaseState
{
    private bool _isRootState = false;
    private playerStateMachine _ctx;
    private playerStateFactory _factory;
    private playerBaseState _currentSubState;
    private playerBaseState _currentSuperState;

    protected bool isRootState {set {_isRootState = value;}}
    protected playerStateMachine Ctx {get {return _ctx;}}
    protected playerStateFactory Factory {get {return _factory;}}


    public playerBaseState(playerStateMachine currentContext, playerStateFactory playerStateFactory){
        _ctx = currentContext;
        _factory = playerStateFactory;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void InitializeSubState();

    public void UpdateStates(){
        UpdateState();
        if(_currentSubState != null){
            _currentSubState.UpdateStates();
        }
    }

    protected void SwitchState(playerBaseState newState){
        // current state exit state
        ExitState();

        //new state enters state
        newState.EnterState();

        if(_isRootState){
            //switch current state of context
            _ctx.CurrentState = newState;
        } else if(_currentSuperState != null){
            _currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(playerBaseState newSuperState){
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(playerBaseState newSubState){
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

}
