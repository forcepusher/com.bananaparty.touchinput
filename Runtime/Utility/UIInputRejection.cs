using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BananaParty.TouchInput
{
    public static class UIInputRejection
    {
        private static readonly List<RaycastResult> s_raycastResults = new();

        public static bool IsOverUI(Vector2 screenPosition)
        {
            var pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = screenPosition
            };

            EventSystem.current.RaycastAll(pointerEventData, s_raycastResults);

            if (s_raycastResults.Count == 0)
                return false;

            return true;
        }
    }
}
