namespace GA.Sessions.Class_02.Scripts.Input
{
    public static class CharacterInputFactory
    {
        public static ICharacterInput CreateInput(InputType type)
        {
            switch (type)
            {
                case InputType.Player:
                    return new PlayerInput();
                case InputType.Enemy:
                    return new EnemyInput();
                default:
                    return new PlayerInput();
            }
        }
        public enum InputType
        {
            Player,
            Enemy
        }
    }
}