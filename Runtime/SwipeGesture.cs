using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.TouchInput
{
    public class SwipeGesture
    {
        private readonly Vector2 _direction;
        private readonly float _angleLimit;
        private readonly float _deltaThreshold;
        private readonly float _timeThreshold;

        public SwipeGesture(Vector2 direction, float angleLimit = 45, float deltaThreshold = 0.05f, float timeTreshold = 0.3f)
        {
            _direction = direction;
            _angleLimit = angleLimit;
            _deltaThreshold = deltaThreshold;
            _timeThreshold = timeTreshold;
        }

        private List<Finger> _fingers = new();

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

                if (finger.ElapsedTime <= _timeThreshold)
                {
                    Vector2 swipeDelta = finger.NormalizedPosition - finger.NormalizedStartPosition;
                    if (swipeDelta.magnitude >= _deltaThreshold)
                    {
                        if (Vector2.Angle(_direction, swipeDelta) <= _angleLimit)
                        {
                            IsActuated = true;
                            _fingers.RemoveAt(fingerIterator);
                            break;
                        }
                    }
                }

                if (finger.Phase == FingerPhase.Lifted)
                    _fingers.RemoveAt(fingerIterator);
            }
        }
    }
}
