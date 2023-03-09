using System;
using System.Linq;

namespace Combinations
{

    public static class LinearIterations
    {

		public static T[][] Combina<T>(T[] elementi, int numeroPosizioni)
		{
			var combinatore = new GeneratoreCombinazioniLineare<T>(elementi, numeroPosizioni);
			return combinatore.ToArray();
		}
	}
}
