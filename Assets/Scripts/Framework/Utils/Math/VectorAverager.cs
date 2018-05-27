using UnityEngine;

namespace Game.Utils
{
    public class VectorAverager : Averager<Vector3>
    {
        public VectorAverager(float samplingWindow) : base(samplingWindow)
        {
        }

        protected override void Recalculate()
        {
            var combinedValue = Vector3.zero;

            var enumerator = Samples.GetEnumerator();
            while (enumerator.MoveNext())
            {
                combinedValue += enumerator.Current.Value;
            }
            enumerator.Dispose();

            Value = combinedValue * (1f / Samples.Count);
        }
    }
}