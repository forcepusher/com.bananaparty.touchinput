using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.TouchInput
{
    public class VirtualJoystick
    {
        public Finger ControllingFinger { get; private set; }
        public Vector2 Input { get; private set; }

        private readonly List<Finger> _fingers = new();

        private readonly float _deadZone;

        public VirtualJoystick(float deadZone = 0.01f)
        {
            _deadZone = deadZone;
        }

        public void AddFinger(Finger finger)
        {
            _fingers.Add(finger);
        }

        public void PollInput()
        {
            Input = Vector2.zero;

            for (int fingerIterator = _fingers.Count - 1; fingerIterator >= 0; fingerIterator -= 1)
            {
                Finger finger = _fingers[fingerIterator];

                Vector2 distanceFromStartPosition = finger.NormalizedPosition - finger.NormalizedStartPosition;
                if (finger.Phase == FingerPhase.Lifted)
                {
                    if (finger == ControllingFinger)
                        ControllingFinger = null;

                    _fingers.RemoveAt(fingerIterator);
                }
                else
                {
                    if (ControllingFinger == null)
                    {
                        if (distanceFromStartPosition.magnitude >= _deadZone)
                            ControllingFinger = finger;
                    }
                }
            }

            if (ControllingFinger != null)
            {
                Vector2 distanceFromStartPosition = ControllingFinger.NormalizedPosition - ControllingFinger.NormalizedStartPosition;
                Input = distanceFromStartPosition;
            }
        }
    }
}
