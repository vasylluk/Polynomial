using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class Monomial
    {
        int degree;
        double coeficient;
        public int Degree
        {
            get => degree;
            set
            {
                degree = value;
            }
        }

        public double Coeficient
        {
            get => coeficient;
            set
            {
                coeficient = value;
            }
        }

        public Monomial(int degree, double coeficient)
        {
            Degree = degree;
            Coeficient = coeficient;
        }
        public Monomial() : this(0,1) { }
    }

    public class Polynomial : Monomial, ICloneable, IEquatable<Polynomial>, IEnumerable<Polynomial>
    {
        int degree;

        public double[] Coeficients { get; set; }
        public double FreeCoef { get; set; }
        public int Degree
        {
            get => degree;
            set
            {
                degree = value;
            }
        }

        public Polynomial(int degree, double[] coefs, double free)
        {
            Degree = degree;
            Coeficients = coefs;
            FreeCoef = free;
        }

        public Polynomial() : this(0, new double[0], 1) { }

        public List<double> ToArray()
        {
            int l = Coeficients.Length + 3;
            List<double> arr = new List<double>();
            foreach (var item in Coeficients)
            {
                arr.Add(item);
            }
            arr.Add(FreeCoef);
            return arr;
        }
        public object Clone() =>
            new Polynomial(Degree, Coeficients, FreeCoef);
        
        public double MaxCoeficient()
        {
            double max = Double.MinValue;
            for(int i = 0; i < Coeficients.Length; i++)
            {
                if (Coeficients[i] > max) max = Coeficients[i];
            }
            return (max>FreeCoef)? max:FreeCoef;
        }

        public double Point(double x)
        {
            double res = 0;
            for(int i = 0; i < Coeficients.Length; i++)
            {
                res += Math.Pow(x,(Degree-i)) * Coeficients[i];
            }
            return res + FreeCoef;
        }

        public bool Equals(Polynomial other)
        {
            if (Degree != other.Degree) return false;
            bool t = true;
            double def = Coeficients[0] / other.Coeficients[0];
            for (int i = 1; i < Coeficients.Length; i++)
            {
                if (Coeficients[i] / other.Coeficients[i] != def)
                    t = false;
            }
            if (t) return true;
            else return false;
        }

        public int CompareTo(Polynomial other)
        {
            var f = this.Degree - other.Degree;
            return (f > 0) ? 1 : (f != 0) ? -1 : 0;
        }

        public IEnumerator<Polynomial> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public static Polynomial operator +(Polynomial f1, Polynomial f2)
        {
            int l = Math.Max(f1.Coeficients.Length, f2.Coeficients.Length);
            int d = f1.Degree - f2.Degree;
            double free = f1.FreeCoef + f2.FreeCoef;
            double[] c = new double[l];
            for (int i = 0; i < (l - d-1); i++)
            {
                c[i] = f1.Coeficients[i] + f2.Coeficients[i];
            }
            if (f1.Degree >= f2.Degree)
            {
                for (int i = l - d; i < l; i++)
                {
                    c[i] = f1.Coeficients[i];
                }
            }
            else
            {
                for (int i = l - d; i < l; i++)
                {
                    c[i] = f2.Coeficients[i];
                }
            }
            Polynomial res = new Polynomial
            {
                FreeCoef = free,
                Degree = l,
                Coeficients = c
            };
            return res;
        }

        public static bool operator ==(Polynomial f1, Polynomial f2)
        {
            double e = 0.0000000001;
            int def = Math.Abs(f1.Degree - f2.Degree);
            int n = Math.Max(f1.Degree, f2.Degree) - 1;
            for (int i = 0; i < n - def; i++)
            {
                if (Math.Abs(f1.Coeficients[i] - f2.Coeficients[i + def])>2*e) return false;
            }
            return true;
        }

        public static bool operator !=(Polynomial f1, Polynomial f2)
        {
            return !(f1==f2);
        }

        public static Polynomial operator /(Polynomial f1, Monomial f2)
        {
            int degree = f1.Degree - f2.Degree;
            double[] coefs = new double[f1.Degree];
            for (int i = 0; i < f1.Coeficients.Length; i++)
            {
                coefs[i] = f1.Coeficients[i] / f2.Coeficient;
            }
            return new Polynomial()
            {
                Degree = degree,
                Coeficients = coefs,
                FreeCoef = f1.FreeCoef / f2.Coeficient
            };
        }

        public static Polynomial operator -(Polynomial f1, Polynomial f2)
        {
            int l = Math.Max(f1.Coeficients.Length, f2.Coeficients.Length);
            int d = f1.Degree - f2.Degree;
            double free = f1.FreeCoef - f2.FreeCoef;
            double[] c = new double[l];
            for (int i = 0; i < l - d; i++)
            {
                c[i] = f1.Coeficients[i] - f2.Coeficients[i];
            }
            if (f1.Degree >= f2.Degree)
            {
                for (int i = l - d; i < l; i++)
                {
                    c[i] = f1.Coeficients[i];
                }
            }
            else
            {
                for (int i = l - d; i < l; i++)
                {
                    c[i] = f2.Coeficients[i];
                }
            }
            Polynomial res = new Polynomial
            {
                FreeCoef = free,
                Degree = l,
                Coeficients = c
            };
            return res;
        }

        public static Polynomial operator *(Polynomial f1, Polynomial f2)
        {
            int l = Math.Max(f1.Coeficients.Length, f2.Coeficients.Length);
            double[] c = new double[l];
            int i = 0;
            foreach (var item in f1.Coeficients)
            {
                foreach (var jtem in f2.Coeficients)
                {
                    if (!(item == 0 || jtem == 0))
                    {
                        c[i] += item * jtem;
                    }
                    else
                    {
                        c[i] += item > 0 ? item : jtem > 0 ? jtem : 0;
                    }
                }
                i++;
            }
            Polynomial res = new Polynomial
            {
                Degree = l,
                FreeCoef = f1.FreeCoef * f2.FreeCoef,
                Coeficients = c
            };
            return res;
        }
    }
}
