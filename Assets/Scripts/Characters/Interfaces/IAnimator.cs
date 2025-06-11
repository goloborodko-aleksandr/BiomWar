using Characters.CharacterPlayer.States;

namespace Characters.Interfaces
{
    public interface IAnimator
    {
        public void Init(BaseCharacter character);
        public void Idle();
        public void Run();
        public void Jump();
        public void Attack();
    }
}