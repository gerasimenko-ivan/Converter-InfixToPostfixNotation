using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Formula
{
    public class Formula
    {
        public enum OperatorPriority
        {
            Low,
            High
        }

        public struct Operator
        {
            #region Fields

            private string name;
            private OperatorPriority priority;

            #endregion
            /////////////////////////////////////////////////////////
            #region Properties

            public string Name
            {
                get { return name; }
            }
            public OperatorPriority Priority
            {
                get { return priority; }
            }
            #endregion
            /////////////////////////////////////////////////////////
            #region Constructors

            public Operator(string Operator, OperatorPriority Priority)
            {
                this.name = Operator;
                this.priority = Priority;
            }
            #endregion
        }

        public static readonly Dictionary<string, Operator> Operators =
            new Dictionary<string, Operator>()
        {
            {"+", new Operator("+", OperatorPriority.Low)},
            {"-", new Operator("-", OperatorPriority.Low)},
            {"*", new Operator("*", OperatorPriority.High)},
            {"/", new Operator("/", OperatorPriority.High)}
        };

        /// <summary>
        /// Converts infix formula notation to postfix notation
        /// </summary>
        /// <param name="mathString">String representation of mathematical formula 
        /// (Allowed operations are +, -, *, /)</param>
        /// <returns>String array of tokens in postfix notation</returns>
        public static string[] InfixToPostfix(string mathString)
        {
            Stack<String[]> stackOperands = new Stack<string[]>();
            Stack<Operator> stackOperators = new Stack<Operator>();

            string[] tokenArray = LexicalParse(mathString);

            foreach (string token in tokenArray)
            {
                if (Operators.ContainsKey(token))
                {
                    Operator operatorTemp = Operators[token];
                    if (stackOperators.Count != 0 &&
                        operatorTemp.Priority <= stackOperators.Peek().Priority)
                    {
                        do
                        {
                            AtomicInfixToPostfix(stackOperands, stackOperators);
                        }
                        while (
                            stackOperators.Count != 0 &&
                            operatorTemp.Priority == stackOperators.Peek().Priority);

                        stackOperators.Push(operatorTemp);

                    }
                    else if (
                        (stackOperators.Count != 0 &&
                        operatorTemp.Priority > stackOperators.Peek().Priority) ||
                        stackOperators.Count == 0)
                    {
                        stackOperators.Push(operatorTemp);
                    }
                }
                else
                {
                    stackOperands.Push(new string[] { token });
                }
            }
            do
            {
                AtomicInfixToPostfix(stackOperands, stackOperators);
            }
            while (stackOperators.Count != 0);

            return stackOperands.Peek();
        }

        
        private static void AtomicInfixToPostfix(Stack<string[]> stackOperands, Stack<Operator> stackOperators)
        {
            string[] tempOperandSecond = stackOperands.Pop();
            string[] tempOperandFirst = stackOperands.Pop();
            string[] tempOperand = new string[tempOperandFirst.Length + tempOperandSecond.Length + 1];
            Array.Copy(tempOperandFirst, 0, tempOperand, 0, tempOperandFirst.Length);
            Array.Copy(tempOperandSecond, 0, tempOperand, tempOperandFirst.Length, tempOperandSecond.Length);
            tempOperand[tempOperand.Length - 1] = stackOperators.Pop().Name;
            stackOperands.Push(tempOperand);
        }

        private static string[] LexicalParse(string mathString)
        {
            mathString = mathString.Replace(" ", "");

            Queue<string> queueTokens = new Queue<string>();
            Queue<string> queueOperand = new Queue<string>();

            foreach (char symbol in mathString)
            {

                if (Operators.ContainsKey(symbol.ToString()))
                {
                    if (queueOperand.Count != 0)
                    {
                        JoinOperandSymbols(queueOperand, queueTokens);
                    }

                    queueTokens.Enqueue(symbol.ToString());
                }
                else
                {
                    queueOperand.Enqueue(symbol.ToString());
                }
            }
            if (queueOperand.Count != 0)
                JoinOperandSymbols(queueOperand, queueTokens);

            return queueTokens.ToArray();
        }
        
        private static void JoinOperandSymbols(Queue<string> queueOperand, Queue<string> queueTokens)
        {
            queueTokens.Enqueue(String.Join("", queueOperand.ToArray()));
            queueOperand.Clear();
        }
    }
}
