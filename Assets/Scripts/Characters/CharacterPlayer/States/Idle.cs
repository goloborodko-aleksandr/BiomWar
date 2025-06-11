using Characters.Fsm;

namespace Characters.CharacterPlayer.States
{
    public class Idle: IState
    {
        private BaseCharacter _character;

        public Fsm.Fsm fsm { get; }

        public Idle(Fsm.Fsm fsm,  BaseCharacter character)
        {
            this.fsm = fsm; 
            _character = character;
        }
        public void EnterState()
        {
            _character.Progress.StartProgress(_character.CharacterSpeed, _character.CoolDown);
            _character.CharacterAnimator.Idle();
        }

        public void ExitState()
        {
            
        }

        public void Update()
        {
            
        }
    }
}