namespace BananaParty.TouchInput
{
    public interface ITouchInputSource
    {
        bool HasNewTouches { get; }
        Finger GetNextNewTouch();
        void PollInput(float deltaTime);
    }
}
