using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp4.Polynomial;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"Let's test:");
            var f1 = new Polynomial(3, new double[] { 4, 3, 5 }, 2);
            var f2 = new Polynomial(3, new double[] { 12, 9, 15 }, 6);
            var m1 = new Monomial(1, 3);
            var f4 = (f1 / m1).ToArray();
            Console.WriteLine($"f4 Coeficients: ");
            for (int i = 0; i < f4.Count; i++)
            {
                Console.Write("{0:F3}\t", f4[i]);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"f1 value in point 3: {f1.Point(3)}");
            Console.WriteLine($"f1 MaxCoeficient: {f1.MaxCoeficient()}");
            Console.WriteLine();
            Console.WriteLine($"f1 equals f2?: {f1.Equals(f2)}");
            Console.WriteLine($"f1 = f2?: {f1 == f2}");
            f1.Degree = 4;
            var f3 = (f2 + f1).ToArray();
            Console.WriteLine();
            Console.WriteLine($"Degree of f1 = {f1.Degree}\n");
            Console.WriteLine();
            Console.Write($"Sum of f1 and f2 = "+Convert.ToChar(123)+" ");
            var n = f3.Count - 1;
            foreach (var item in f3)
            {       
                if (n>1)
                Console.Write('('+Convert.ToString(item) + "x^"+Convert.ToString(n)+") + ");
                n--;
            }

            Console.Write('(' + Convert.ToString(f3[f3.Count-2]) + "x) + ");
            Console.Write('(' + Convert.ToString(f3[f3.Count-1]) + ") ");
            Console.WriteLine(Convert.ToChar(125));
            Console.ReadKey();
        }
    }
}
