using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.TouchInput
{
    public class TapGesture
    {
        private readonly float _timeLimit;
        private readonly float _movementLimit;
        
        public TapGesture(float timeLimit = 0.2f, float movementLimit = 0.05f)
        {
            _timeLimit = timeLimit;
            _movementLimit = movementLimit;
        }

        private readonly List<Finger> _fingers = new();

        public bool IsActuated { get; private set; }

        public void AddFinger(Finger finger)
        {
            _fingers.Add(finger);
        }

        public void PollInput()
        {
            IsActuated = false;

            for (int fingerIterator = _fingers.Count - 1; fingerIterator >= 0; fingerIterator--)
            {
                Finger finger = _fingers[fingerIterator];

                if (finger.Phase == FingerPhase.Lifted)
                {
                    if (finger.ElapsedTime <= _timeLimit)
                    {
                        Vector2 positionDelta = finger.NormalizedPosition - finger.NormalizedStartPosition;
                        if (positionDelta.magnitude <= _movementLimit)
                            IsActuated = true;
                    }

                    _fingers.RemoveAt(fingerIterator);
                }
            }
        }
    }
}
