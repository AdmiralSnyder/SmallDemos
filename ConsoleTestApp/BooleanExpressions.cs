using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp
{
    internal class BooleanExpressions
    {

        /*
literal = true | false
expression = literal | conjunction | disjunction
conjunction = expression "&&" expression
disjunction = expression "||" expression


(true || false) && (false || true)

expression
  conjunction
    disjunction
      true
      false
    disjunction
      false
      true




step 1 get formula   (A || B) && (C || D) 
step 2 get values    A = true, B = false, C = false, D = true    <--- calculating these so that the formula is true  is called  SAT (saturation problem), very hard
step 3 evaluates to true?   <-- easy
         */

        abstract class Expression
        {
            public abstract bool Evaluate();

            public static Expression Parse(string str)
            {
                throw new NotImplementedException(); 
            }
        }

        class Literal : Expression
        {
            public bool Value { get; set; }
            
            public override bool Evaluate() => Value;

            public override string ToString() => $"Value";
        }

        /// <summary>
        /// A || B
        /// </summary>
        class Disjunction : Expression
        {
            public Expression A { get; set; }
            public Expression B { get; set; }

            public override bool Evaluate() => A.Evaluate() || B.Evaluate();

            public override string ToString() => $"({A} || {B})";
        }

        /// <summary>
        /// A && B
        /// </summary>
        class Conjunction : Expression
        {
            public Expression A { get; set; }
            public Expression B { get; set; }

            public override bool Evaluate() => A.Evaluate() && B.Evaluate();

            public override string ToString() => $"({A} && {B})";
        }

        public void TestStuff()
        {
            // (true || false) && (false || true)
            var exp = new Conjunction()
            {
                A = new Disjunction 
                {
                    A = new Literal { Value = true},
                    B = new Literal { Value = false },
                } ,
                B = new Disjunction
                {
                    A = new Literal { Value = false },
                    B = new Literal { Value = true },
                }
            };

            var value = exp.Evaluate();
            var valueStr = exp.ToString(); // ((true || false) && (false || true))


            // true && false || true     ==    (true && false) || true
            var exp2 = new Disjunction
            {
                A = new Conjunction
                {
                    A = new Literal { Value = true },
                    B = new Literal { Value = false },
                },
                B = new Literal { Value = true },
            };

            var value2 = exp2.Evaluate();







        }




    }
}
