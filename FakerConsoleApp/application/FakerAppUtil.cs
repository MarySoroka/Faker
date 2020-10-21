using System;
using System.Collections.Generic;

namespace FakerTest.faker
{
    public static class FakerTestUtil
    {
        internal struct Structure1
        {
            public bool field1;
            public char field2;
        }

        internal struct Structure2
        {
            public int field1;

            public Structure2(int field1)
            {
                this.field1 = field1;
            }
        }

        internal struct Structure3
        {
            int field1;
            public int field2;

            Structure3(int num1, int num2)
            {
                field1 = num1;
                field2 = num2;
            }
        }

        internal struct Structure4
        {
            int field1;
            bool field2;
            public char field3;

            public Structure4(int field1, bool field2)
            {
                this.field1 = field1;
                this.field2 = field2;
                field3 = '1';
            }

            public Structure4(int field1, bool field2, char field3)
            {
                this.field1 = field1;
                this.field2 = field2;
                this.field3 = field3;
            }
        }

        internal class Class1
        {
            public int field;
            public DateTime field2;
            long field3;
            short field4;
            public bool field5;
        }

        internal class Class2
        {
            public int field;
            public DateTime field2;
            long field3;
            short field4;
            public bool field5;

            Class2(long l, short sh)
            {
                this.field3 = l;
                this.field4 = sh;
            }
        }

        internal class Class3
        {
            long field1;
            short field2;

            public Class3(long l, short sh)
            {
                field1 = l;
                field2 = sh;
            }
        }

        internal class Class4
        {
            public int a;
            public Class3 sClass;

            Class4(Class3 sc)
            {
                this.sClass = sc;
            }

            public Class4()
            {
            }
        }

        internal class Class5
        {
            public int i;
            public DateTime t;
            long l;
            public char c { get; }
            public bool b { get; }
            double d;

            public Class5()
            {
            }

            public Class5(long l, double d)
            {
                this.l = l;
                this.d = d;
            }
        }

        internal class Class6
        {
            public List<int> ints;
            List<double> doubles;
            List<char> chars;
            public List<DateTime> times;

            public Class6(List<double> doubles)
            {
                this.doubles = doubles;
            }
        }
    }
}