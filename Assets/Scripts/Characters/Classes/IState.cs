namespace Characters.Classes
{
    public interface  IState
    {
        public Fsm fsm { get; }
        public  void EnterState();
        public  void ExitState();
        public  void Update();
    }
}