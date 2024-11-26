using System.Collections.Generic;

namespace Project
{
    public class MultimeterModePool
    {
        private readonly List<MultimeterMode> _pool;

        public MultimeterModePool(IEnumerable<MultimeterMode> pool)
        {
            _pool = new(pool);
        }

        public TMode Get<TMode>() where TMode : MultimeterMode, new()
        {
            foreach (var mode in _pool)
            {
                if (mode is TMode pooledMode)
                    return pooledMode;
            }

            var newMode = new TMode();

            _pool.Add(newMode);

            return newMode;
        }

        public void Clear()
        {
            _pool.Clear();
        }
    }
}
