using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(TMP_Text))]
    public class MultimeterAllResultsText : MonoBehaviour
    {
        private TMP_Text _text;
        private Multimeter _multimeter;
        private IEnumerable<MultimeterMode> _availableModes;
        private MultimeterModeMap<string> _multimeterModeSymbolMap;
        private MultimeterResultFormatter _multimeterResultFormatter;
        private StringBuilder _textBuilder;

        public void Construct(Multimeter multimeter, IEnumerable<MultimeterMode> availableModes,
            MultimeterModeMap<string> multimeterModeSymbolMap, MultimeterResultFormatter multimeterResultFormatter)
        {
            _text = GetComponent<TMP_Text>();
            _multimeter = multimeter;
            _availableModes = availableModes;
            _multimeterModeSymbolMap = multimeterModeSymbolMap;
            _multimeterResultFormatter = multimeterResultFormatter;
            _textBuilder = new();

            _multimeter.Ticked += OnTicked;
        }

        private void OnDestroy()
        {
            _multimeter.Ticked -= OnTicked;
        }

        private void OnTicked()
        {
            _textBuilder.Clear();

            foreach (var mode in _availableModes)
            {
                var symbol = _multimeterModeSymbolMap.Get(mode);
                float result;

                if (mode == _multimeter.CurrentMode)
                    result = _multimeter.GetResult(mode);
                else
                    result = 0f;

                _textBuilder.AppendLine(GetResultText(result, symbol));
            }

            _text.text = _textBuilder.ToString();
        }

        private string GetResultText(float result, string symbol)
        {
            return $"{symbol} = {_multimeterResultFormatter.Format(result)}";
        }
    }
}
