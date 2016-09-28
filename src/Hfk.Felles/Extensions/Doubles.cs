using System;
using System.Linq;

namespace Hfk.Felles
{

    /// <summary>
    ///     Extention methods.
    /// </summary>
    public static class Doubles
    {
        /// <summary>
        ///     Creates a distribution of the provided double array.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double[] Fordeling(this double[] values)
        {
            var sum = values.Sum();
            for (var i = 0; i < values.Length; i++)
            {
                values[i] /= sum;
                values[i] = Math.Round(values[i]*100);
            }

            return (values);
        }
    }
}