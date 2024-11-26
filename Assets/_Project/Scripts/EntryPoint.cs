using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project
{
    // Своего рода внедрение зависимостей. Лучше юзать VContainer/Zenject/etc.
    public sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ElectricalDevice _device;
        [SerializeField] private Multimeter _multimeter;
        [SerializeField] private MultimeterRegulator _multimeterRegulator;
        [SerializeField] private MultimeterRegulatorActivator _multimeterRegulatorActivator;
        [SerializeField] private MultimeterRegulatorTwister _multimeterRegulatorTwister;
        [SerializeField] private MultimeterAllResultsText _multimeterAllResultsText;
        [SerializeField] private MultimeterCurrentModeResultText _multimeterCurrentModeResultText;

        private GameInput _input;
        private MultimeterMode[] _availableModes;
        private MultimeterModePool _multimeterModePool;
        private MultimeterModeMap<string> _multimeterModeSymbolMap;
        private MultimeterModeMap<float> _multimeterModeAngleMap;
        private MultimeterResultFormatter _multimeterResultFormatter;

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
            _multimeterModePool = new(_availableModes);
            _multimeterModeSymbolMap = new(new Dictionary<MultimeterMode, string>(new KeyValuePair<MultimeterMode, string>[]
            {
                new(new NeutralMode(), string.Empty),
                new(new DCVoltageMode(), "V"),
                new(new ACVoltageMode(), "V~"),
                new(new ResistanceMode(), "Ω"),
                new(new AmperageMode(), "A")
            }));
            _multimeterModeAngleMap = new(new Dictionary<MultimeterMode, float>(new KeyValuePair<MultimeterMode, float>[]
            {
                new(new NeutralMode(), -45f),
                new(new DCVoltageMode(), 17.3f),
                new(new ACVoltageMode(), 73.5f),
                new(new ResistanceMode(), 144.6f),
                new(new AmperageMode(), 255f)
            }));
            _multimeterResultFormatter = new();

            _multimeter.Construct(_multimeterModePool);
            _multimeterRegulator.Construct(_input, _multimeter, _availableModes);
            _multimeterRegulatorActivator.Construct(_multimeterRegulator);
            _multimeterRegulatorTwister.Construct(_multimeter, _multimeterModeAngleMap);
            _multimeterAllResultsText.Construct(_multimeter, _availableModes.Where(v => v is not NeutralMode),
                _multimeterModeSymbolMap, _multimeterResultFormatter);
            _multimeterCurrentModeResultText.Construct(_multimeter, _multimeterResultFormatter);

            _multimeter.SetCurrentMode<NeutralMode>();
            _multimeter.ConnectDevice(_device);
            _input.Enable();
        }
    }
}
