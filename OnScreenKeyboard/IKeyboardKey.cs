namespace OnScreenKeyboard
{
     //test 2
    public interface IKeyboardKey
    {
        KeyStateStyle CurrentStyle { get; }
        bool IsLocked { get; set; }
        KeyboardKeyState GetCurrentState();
        void AddState(KeyboardKeyState state);
    }
}
