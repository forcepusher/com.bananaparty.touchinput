using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.TouchInput
{
    public class RadialSwipeGesture
    {
        private readonly float _deltaThreshold;
        private readonly float _timeThreshold;

        public RadialSwipeGesture(float deltaThreshold = 0.1f, float timeTreshold = 0.2f)
        {
            _deltaThreshold = deltaThreshold;
            _timeThreshold = timeTreshold;
        }

        private readonly List<Finger> _fingers = new();

        public bool IsActuated { get; private set; }

        public Vector2 SwipeDelta { get; private set; }

        public void AddFinger(Finger finger)
        {
            _fingers.Add(finger);
        }

        public void PollInput()
        {
            IsActuated = false;
            SwipeDelta = Vector2.zero;

            for (int fingerIterator = _fingers.Count - 1; fingerIterator >= 0; fingerIterator--)
            {
                Finger finger = _fingers[fingerIterator];

                if (finger.Phase == FingerPhase.Lifted)
                {
                    if (finger.ElapsedTime <= _timeThreshold)
                    {
                        Vector2 swipeDelta = finger.NormalizedPosition - finger.NormalizedStartPosition;
                        if (swipeDelta.magnitude >= _deltaThreshold)
                        {
                            IsActuated = true;
                            SwipeDelta = swipeDelta;
                        }
                    }

                    _fingers.RemoveAt(fingerIterator);
                }
            }
        }
    }
}
