using Characters.Mono;


namespace Characters.Interfaces
{
    public interface IAnimator
    {
        void Idle(IPlayer character);
        void Move(IPlayer character);
        void Jump(IPlayer character);
        void Teleport(IPlayer character);
        void Attack(IPlayer character);
        void Die(IPlayer character);
    }
}