using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.TouchInput
{
    public class Touchscreen : ITouchInputSource
    {
        private readonly Dictionary<int, Finger> _touchIdToFinger = new();
        private readonly Queue<Finger> _newFingers = new();

        public bool HasNewTouches => _newFingers.Count > 0;

        public Finger GetNextNewTouch()
        {
            return _newFingers.Dequeue();
        }

        public void PollInput(float deltaTime)
        {
            foreach (Touch touch in Input.touches)
            {
                if (!_touchIdToFinger.TryGetValue(touch.fingerId, out Finger finger))
                {
                    finger = new Finger(touch.position);
                    //Debug.Log($"finger {touch.fingerId} is {touch.phase}, so it's {FingerPhase.Pressed}");
                    _touchIdToFinger[touch.fingerId] = finger;
                    _newFingers.Enqueue(finger);
                }
                else
                {
                    finger.Move(touch.position);
                    finger.IncrementTime(deltaTime);

                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        //Debug.Log($"finger {touch.fingerId} is {touch.phase}, so it's {FingerPhase.Lifted}");
                        finger.Phase = FingerPhase.Lifted;
                        _touchIdToFinger.Remove(touch.fingerId);
                    }
                    else
                    {
                        //Debug.Log($"finger {touch.fingerId} is {touch.phase}, so it's {FingerPhase.Held}");
                        finger.Phase = FingerPhase.Held;
                    }
                }
            }

            // Force clear touches when nothing is held just so the
            // WebGLInput.mobileKeyboardSupport = false;
            // could properly work without getting stuck multitouch fingers
            if (Input.touchCount == 0)
            {
                foreach (int touchId in _touchIdToFinger.Keys)
                    _touchIdToFinger[touchId].Phase = FingerPhase.Lifted;

                _touchIdToFinger.Clear();
            }
        }
    }
}
