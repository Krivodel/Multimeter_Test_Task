using System;
using System.Collections.Generic;

namespace Project
{
    public class MultimeterModeMap<TTarget>
    {
        private readonly IReadOnlyDictionary<MultimeterMode, TTarget> _map;

        public MultimeterModeMap(IReadOnlyDictionary<MultimeterMode, TTarget> map)
        {
            _map = map;
        }

        public TTarget Get(MultimeterMode mode)
        {
            if (!_map.TryGetValue(mode, out var symbol))
                throw new NotImplementedException($"Target for mode '{mode}' not implemented.");

            return symbol;
        }
    }
}
