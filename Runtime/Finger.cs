using UnityEngine;

namespace BananaParty.TouchInput
{
    public class Finger
    {
        public Vector2 StartPosition { get; private set; }
        public Vector2 NormalizedStartPosition => StartPosition / ScreenResolution;
        public Vector2 Position { get; private set; }
        public Vector2 NormalizedPosition => Position / ScreenResolution;
        public Vector2 DeltaPosition { get; private set; }
        public Vector2 NormalizedDeltaPosition => DeltaPosition / ScreenResolution;

        public float ElapsedTime { get; private set; }
        public FingerPhase Phase { get; internal set; } = FingerPhase.Pressed;

        private Vector2 ScreenResolution => new(Screen.width, Screen.height);

        public Finger(Vector2 screenPosition)
        {
            StartPosition = Position = screenPosition;
        }

        public void Move(Vector2 position)
        {
            DeltaPosition = position - Position;
            Position = position;
        }

        public void IncrementTime(float deltaTime)
        {
            ElapsedTime += deltaTime;
        }
    }
}
