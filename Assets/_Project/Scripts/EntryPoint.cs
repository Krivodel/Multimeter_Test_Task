using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    // Своего рода внедрение зависимостей. Лучше юзать VContainer/Zenject/etc.
    // Считаю это костылём на время отсутствия DI контейнера.
    public sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ElectricalDevice _device;
        [SerializeField] private Multimeter _multimeter;
        [SerializeField] private MultimeterRegulator _multimeterRegulator;
        [SerializeField] private MultimeterRegulatorActivator _multimeterRegulatorActivator;
        [SerializeField] private MultimeterRegulatorTwister _multimeterRegulatorTwister;
        [SerializeField] private MultimeterResultText[] _multimeterResultTexts;

        private GameInput _input;
        private MultimeterMode[] _availableModes;
        private Dictionary<MultimeterMode, string> _multimeterModeSymbolMap;
        private Dictionary<MultimeterMode, float> _multimeterModeAngleMap;

        private void Awake()
        {
            _input = new();
            _availableModes = new MultimeterMode[]
            {
                new NeutralMode(),
                new DCVoltageMode(),
                new ACVoltageMode(),
                new ResistanceMode(),
                new AmperageMode()
            };
            _multimeterModeSymbolMap = new(new KeyValuePair<MultimeterMode, string>[]
            {
                new(new NeutralMode(), ""),
                new(new DCVoltageMode(), "V"),
                new(new ACVoltageMode(), "V~"),
                new(new ResistanceMode(), "Ω"),
                new(new AmperageMode(), "A")
            });
            _multimeterModeAngleMap = new(new KeyValuePair<MultimeterMode, float>[]
            {
                new(new NeutralMode(), -45f),
                new(new DCVoltageMode(), 17.3f),
                new(new ACVoltageMode(), 73.5f),
                new(new ResistanceMode(), 144.6f),
                new(new AmperageMode(), 255f)
            });

            _multimeterRegulator.Construct(_input, _multimeter, _availableModes);
            _multimeterRegulatorActivator.Construct(_multimeterRegulator);
            _multimeterRegulatorTwister.Construct(_multimeter, _multimeterModeAngleMap);

            foreach (var multimeterResultText in _multimeterResultTexts)
                multimeterResultText.Construct(_multimeter, _multimeterModeSymbolMap);

            _input.Enable();
            _multimeter.ConnectDevice(_device);
            _multimeter.SetMode<NeutralMode>();
        }
    }
}
