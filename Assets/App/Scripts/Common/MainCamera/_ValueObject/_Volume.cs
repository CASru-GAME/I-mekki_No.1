using System;

namespace App.Common.MainCamera.ValueObject
{
    public class Volume
    {
        private readonly float _currentValue;
        public float CurrentValue => _currentValue;

        public Volume(float currentValue)
        {
            if (currentValue < 0 || currentValue > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(currentValue), "Volume must be between 0 and 100.");
            }

            this._currentValue = currentValue;
        }

        public Volume()
        {
            this._currentValue = 75f;
        }

        public void Dump(string message)
        {
            UnityEngine.Debug.Log($"Message : {message}, Volume: {CurrentValue}.");
        }
    }
}