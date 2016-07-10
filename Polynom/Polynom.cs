using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polynom
{
    public class Polynom: ICloneable, IEquatable<Polynom>
    {
        private readonly double[] coefficients;
        private readonly int degree;

        public Polynom()
        {
            degree = 1;
            coefficients = new double[2];
            coefficients[0] = 1;
            coefficients[1] = 1;
        }


        public Polynom(params double[] array)
        {
            if (array == null)
                throw new ArgumentNullException();
            if (array.Length == 0)
                throw new ArgumentException("No coefficients!");


            int range = array.Length - 1;


            while (array[range] == 0)
            {
                range--;
                if (range < 0)
                    throw new NotImplementedException("No coefficients");
            }


            degree = range;

            coefficients = new double[degree + 1];
            Array.Copy(array, coefficients, degree + 1);
        }




        public override int GetHashCode()
        {
            unchecked
            {
                return ((coefficients != null ? coefficients.GetHashCode() : 0) * 397) ^ degree;
            }
        }


        public bool Equals(Polynom other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (other.degree != this.degree) return false;

            for (int i = 0; i < degree; i++)
                if (this.coefficients[i].CompareTo(other.coefficients[i]) != 0)
                    return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Polynom)obj);
        }



        public override string ToString()
        {
            string pol = "Polynom: " + coefficients[0];
            for (int i = 1; i <= degree; i++)
            {
                if (coefficients[i] > 0)
                    pol += " + " + coefficients[i] + "x" + "^" + i;
                else if (coefficients[i] < 0)
                    pol += " " + coefficients[i] + "x" + "^" + i;
                else
                    continue;
            }
            pol += " = 0";
            return pol;
        }



        public Polynom Clone()
        {
            return new Polynom(this.coefficients);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }



        public static bool operator ==(Polynom a, Polynom b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Polynom a, Polynom b)
        {
            if (a.Equals(b))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public double this[int i]
        {
            get { return coefficients[i]; }
        }

        public static Polynom operator +(Polynom a, Polynom b)
        {

            if (a == null || b == null)
                throw new NullReferenceException();

            int maxdeg = Math.Max(a.degree, b.degree);
            int mindeg = Math.Min(a.degree, b.degree);
            double[] coeff = new double[maxdeg + 1];

            for (int i = 0; i <= mindeg; i++)
            {
                checked
                {
                    coeff[i] = a.coefficients[i] + b.coefficients[i];
                }
            }
            for (int i = mindeg + 1; i <= maxdeg; i++)
            {
                coeff[i] = a.degree >= b.degree ? a.coefficients[i] : b.coefficients[i];
            }
            return new Polynom(coeff);
        }

        public static Polynom operator -(Polynom a)
        {
            if (a == null)
                throw new ArgumentNullException("NULL");
            double[] result = new double[a.degree + 1];
            for (int i = 0; i <= a.degree; i++)
            {
                result[i] = -a.coefficients[i];
            }
            return new Polynom(result);
        }

        public static Polynom operator -(Polynom a, Polynom b)
        {

            if (a == null || b == null)
                throw new NullReferenceException();
            return a + (-b);
        }


        public static Polynom operator *(Polynom a, Polynom b)
        {
            if (a == null || b == null)
                throw new NullReferenceException();
            int deg = a.degree + b.degree;
            double[] arrResult = new double[deg + 1];
            for (int i = 0; i <= a.degree; i++)
            {
                for (int j = 0; j <= b.degree; j++)
                {
                    arrResult[i + j] += a.coefficients[i] * b.coefficients[j];
                }
            }
            return new Polynom(arrResult);
        }


        public Polynom Add(Polynom a)
        {
            return (this + a);
        }

        public Polynom Substract(Polynom a)
        {
            return (this - a);
        }

        public Polynom Multiply(Polynom a)
        {
            return (this * a);
        }
    }
}

