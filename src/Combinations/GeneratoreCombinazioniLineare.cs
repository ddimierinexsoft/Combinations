using System;
using System.Collections;
using System.Collections.Generic;

namespace Combinations
{

    public class GeneratoreCombinazioniLineare<T> : IEnumerable<T[]>, IEnumerator<T[]>
    {
        private readonly T[] elementi;
        private readonly int numeroPosizioni;
        private int[] indici;
        private int p;

        public GeneratoreCombinazioniLineare(T[] elementi, int numeroPosizioni)
        {
            this.elementi = (T[])elementi.Clone();
            this.numeroPosizioni = numeroPosizioni;
            indici = new int[numeroPosizioni];
            p = numeroPosizioni;
        }

        public bool MoveNext()
        {
            if (p == numeroPosizioni)
            {
                p -= 1;
                return true;
            }
            else if (p < 0)
            {
                return false;
            }
            else
            {
                p = increment();
                if (p < 0)
                {
                    return false;
                }
                return true;
            }
        }

        private int increment()
        {
            int i = numeroPosizioni - 1;
            indici[i] += 1;
            bool overflow = (indici[i] >= elementi.Length);
            while (overflow)
            {
                for (int k = i; k < numeroPosizioni; k++)
                {
                    indici[k] = 0;
                }
                i -= 1;
                if (i < 0) break;
                indici[i] += 1;
                overflow = (indici[i] >= elementi.Length);
            }
            return i;
        }

        public T[] Current
        {
            get
            {
                //  return indici.Select(i => elementi[i]).ToArray();
                var ret = new T[numeroPosizioni];
                for (int i = 0; i < numeroPosizioni; i++)
                {
                    ret[i] = elementi[indici[i]];
                }
                return ret;
            }
        }

        public void Reset()
        {
            indici = new int[numeroPosizioni];
            p = numeroPosizioni;
        }

        public override int GetHashCode()
        {
            return p.GetHashCode();
        }  

        public override string ToString()
        {
            if (typeof(T) == typeof(char))
            {
                return new string((char[])(object)Current);
            }
            else
            {
                return string.Join(", ", Current);
            }
        }
         
        public IEnumerator<T[]> GetEnumerator()
        {
            return this;
        }

        void IDisposable.Dispose()
        {
            //NOTHING TO DO
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }           
    }
}