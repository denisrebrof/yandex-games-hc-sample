using System.Collections.Generic;

namespace Gameplay.PlayerInput
{
    public class InputHandler
    {
        private Dictionary<string, float> inputHandler = new();

        public float GetInput(string axis)
        {
            return inputHandler.ContainsKey(axis) ? inputHandler[axis] : 0f;
        }

        public void SetInput(float input, string axis)
        {
            inputHandler[axis] = input;
        }

        public void Reset()
        {
            foreach (var key in inputHandler.Keys)
            {
                inputHandler[key] = 0f;
            }
        }
    }
}