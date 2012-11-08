using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Formula
{
    public class Formula
    {
        // list of priorities
        public enum OperatorPriority
        {
            Low,
            High
        }
        // Operator description
        public struct Operator
        {
            // Fields
            private string name;
            private OperatorPriority priority;

            // Properties 
            public string Name
            {
                get { return name; }
            }
            public OperatorPriority Priority
            {
                get { return priority; }
            }

            // Constructor
            public Operator(string Operator, OperatorPriority Priority)
            {
                this.name = Operator;
                this.priority = Priority;
            }
        }
        
        // Dictionary of operators
        public static readonly Dictionary<string, Operator> operatorDictionary =
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
                if (operatorDictionary.ContainsKey(token))
                {
                    Operator operatorTemp = operatorDictionary[token];
                    if (stackOperators.Count != 0 &&
                        operatorTemp.Priority < stackOperators.Peek().Priority)
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
                        stackOperators.Count != 0 &&
                        operatorTemp.Priority == stackOperators.Peek().Priority)
                    {
                        AtomicInfixToPostfix(stackOperands, stackOperators);
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

        /// <summary>
        /// Atomic operation which converts last two operands of stackOperands (e.g. "A","B") and
        /// last operator of stackOperators (e.g. "+") into postfix notation formula ({"A","B","+"})
        /// and stores this formula as new operand in stackOperands
        /// </summary>
        /// <param name="stackOperands"></param>
        /// <param name="stackOperators"></param>
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

        /// <summary>
        /// Splits input math string (e.g. "A+B") into string array of tokens (e.g. {"A","+","B"})
        /// </summary>
        /// <param name="mathString">String representation of mathematical formula 
        /// (Allowed operations are +, -, *, /)</param>
        /// <returns>String array of tokens String[]</returns>
        private static string[] LexicalParse(string mathString)
        {
            // Remove whitespaces
            mathString = mathString.Replace(" ", "");

            Queue<string> queueTokens = new Queue<string>();
            Queue<string> queueOperand = new Queue<string>();

            // Check every symbol in math string
            foreach (char symbol in mathString)
            {

                if (operatorDictionary.ContainsKey(symbol.ToString()))
                {
                    // New operator has been found
                    if (queueOperand.Count != 0)
                    {
                        // Join and store operand preceding this operator
                        JoinOperand(queueOperand, queueTokens);
                    }
                    // Store found operator to tokens
                    queueTokens.Enqueue(symbol.ToString());
                }
                else
                {
                    // Another symbol between operators
                    queueOperand.Enqueue(symbol.ToString());
                }
            }
            // Join last operand in string
            if (queueOperand.Count != 0)
                JoinOperand(queueOperand, queueTokens);

            return queueTokens.ToArray();
        }
        /// <summary>
        /// Joins all symbols in queueOperand into string operator and stores this string
        /// into queueTokens. Clears queueOperand.
        /// </summary>
        /// <param name="queueOperand"></param>
        /// <param name="queueTokens"></param>
        private static void JoinOperand(Queue<string> queueOperand, Queue<string> queueTokens)
        {
            queueTokens.Enqueue(String.Join("", queueOperand.ToArray()));
            queueOperand.Clear();
        }
    }
}
