namespace AjLisp
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

    using AjLisp.Compiler;
    using AjLisp.Language;
    using AjLisp.Primitives;

    public class Machine
    {
        private ValueEnvironment environment = new ValueEnvironment();

        public Machine()
        {
            this.Define("cons", new SubrCons());
            this.Define("first", new SubrFirst());
            this.Define("rest", new SubrRest());
            this.Define("list", new SubrList());
            this.Define("quote", new FSubrQuote());
            this.Define("backquote", new FSubrBackquote());
            this.Define("append", new SubrAppend());
            this.Define("cond", new FSubrCond());
            this.Define("atom?", new SubrAtom());
            this.Define("eval", new SubrEval());
            this.Define("lambda", new FSubrLambda());
            this.Define("progn", new FSubrProgN());
            this.Define("flambda", new FSubrFLambda());
            this.Define("nlambda", new FSubrNLambda());
            this.Define("mlambda", new FSubrMLambda());
            this.Define("number?", new SubrNumberP());
            this.Define("function?", new SubrFunctionP());
            this.Define("id?", new SubrIdP());
            this.Define("nil?", new SubrNilP());
            this.Define("define", new FSubrDefine());
            this.Define("definef", new FSubrDefineF());
            this.Define("definen", new FSubrDefineN());
            this.Define("definem", new FSubrDefineM());
            this.Define("if", new FSubrIf());
            this.Define("let", new FSubrLet());
            this.Define("lets", new FSubrLetS());
            this.Define("set", new SubrSet());
            this.Define("cons?", new SubrConsP());

            this.Define("==", new SubrEq());
            this.Define("<", new SubrLess());
            this.Define("<=", new SubrLessEqual());
            this.Define(">", new SubrGreater());
            this.Define(">=", new SubrGreaterEqual());

            this.Define("+", new SubrPlus());
            this.Define("-", new SubrDifference());
            this.Define("*", new SubrTimes());
            this.Define("/", new SubrQuotient());
            this.Define("%", new SubrRemainder());

            this.Define("invoke", new SubrInvoke());
            this.Define("type-invoke", new FSubrTypeInvoke());
            this.Define("new", new FSubrNew());
        }

        public ValueEnvironment Environment { get { return this.environment; } }

        public static object Evaluate(object value, ValueEnvironment environment)
        {
            Debug.WriteLine("Machine Evaluate: " + Conversions.ToPrintString(value));

            IExpression expression = value as IExpression;

            if (expression == null)
                return value;

            return expression.Evaluate(environment);
        }

        public object Evaluate(object expr)
        {
            return Machine.Evaluate(expr, this.environment);
        }

        public object Evaluate(string expr)
        {
            Compiler.Parser cmp = new Compiler.Parser(expr);
            return Evaluate(cmp.Compile());
        }

        private void Define(string name, object value)
        {
            this.environment.SetValue(name, value);
        }
    }
}
