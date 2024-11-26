using System;
using System.Collections.Generic;
using System.Linq;

namespace Project
{
    public class MultimeterModeSelector
    {
        public MultimeterMode SelectedMode => _availableModes[_selectedModeIndex];

        public event Action<MultimeterMode> Selected;

        private readonly MultimeterMode[] _availableModes;
        private int _selectedModeIndex;

        public MultimeterModeSelector(IEnumerable<MultimeterMode> availableModes)
        {
            _availableModes = availableModes.ToArray();
        }

        public void SelectNextMode()
        {
            _selectedModeIndex++;

            if (_selectedModeIndex == _availableModes.Length)
                _selectedModeIndex = 0;

            Selected?.Invoke(SelectedMode);
        }

        public void SelectPreviousMode()
        {
            _selectedModeIndex--;

            if (_selectedModeIndex == -1)
                _selectedModeIndex = _availableModes.Length - 1;

            Selected?.Invoke(SelectedMode);
        }
    }
}
