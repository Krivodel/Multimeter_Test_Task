using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(TMP_Text))]
    public class MultimeterResultText : MonoBehaviour
    {
        [SerializeField] private string _fullFormat = "{K} = {V}";
        [SerializeField] private string _emptyFormat = "{V}";

        private TMP_Text _text;
        private Multimeter _multimeter;
        private Dictionary<MultimeterMode, string> _multimeterModeSymbolMap;

        public void Construct(Multimeter multimeter, Dictionary<MultimeterMode, string> multimeterModeSymbolMap)
        {
            _multimeter = multimeter;
            _multimeterModeSymbolMap = multimeterModeSymbolMap;

            _multimeter.ResultChanged += OnResultChanged;
        }

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void OnDestroy()
        {
            _multimeter.ResultChanged -= OnResultChanged;
        }

        private void OnResultChanged(float result)
        {
            _text.text = GetResultText(result, GetSymbol());
        }

        private string GetSymbol()
        {
            if (!_multimeterModeSymbolMap.TryGetValue(_multimeter.Mode, out var symbol))
                throw new NotImplementedException($"Symbol for mode '{_multimeter.Mode}' not implemented.");

            return symbol;
        }

        private string GetFormatBySymbol(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
                return _emptyFormat;
            else
                return _fullFormat;
        }

        private string GetResultText(float result, string symbol)
        {
            return GetFormatBySymbol(symbol)
                .Replace("{K}", symbol)
                .Replace("{V}", $"{result:F2}");
        }
    }
}
