namespace AU.AlgorithmsOnStrings
{
    using System;
    using System.Text;

    /// <summary>
    /// Costs of operations: copy, replace, insert and delete respectively.
    /// </summary>
    public struct Costs
    {
        public static sbyte cC = -1;
        public static byte cR = 1;
        public static byte cI = 2;
        public static byte cD = 2;
    }

    public class Transformation
    {
        /// <summary>
        /// 
        /// </summary>
        private string[,] _op;

        /// <summary>
        /// 
        /// </summary>
        private int[,] _cost;

        /// <summary>
        /// The compute transformation table procedure.
        /// </summary>
        /// <param name="x">a string of length m.</param>
        /// <param name="y">a string of length n.</param>
        /// <param name="cost">A strcut contains the cost of the operations. Set to null to use defaults.</param>
        /// <returns>The same object after setting states and tables _cost and _op, to facilitates chaining.</returns>
        public Transformation ComputeTransformTable(string x, string y, Costs? c = null)
        {
            int m = x.Length + 1,
                n = y.Length + 1;

            c = c ?? new Costs();

            _cost = new int[m, n];
            _op = new string[m, n];

            _cost[0, 0] = 0;

            for (int i = 1; i < m; ++i)
            {
                _cost[i, 0] = i * Costs.cD;
                _op[i, 0] = F("delete", x[i - 1]);
            }

            for (int j = 1; j < n; ++j)
            {
                _cost[0, j] = j * Costs.cI;
                _op[0, j] = F("insert", y[j - 1]);
            }

            for (int i = 1; i < m; ++i)
                for (int j = 1; j < n; ++j)
                {
                    if (x[i - 1] == y[j - 1])
                    {
                        _cost[i, j] = _cost[i - 1, j - 1] + Costs.cC;
                        _op[i, j] = F("copy", x[i - 1]);
                    }
                    else
                    {
                        _cost[i, j] = _cost[i - 1, j - 1] + Costs.cR;
                        _op[i, j] = FR("replace", x[i - 1], y[j - 1]);
                    }
                    
                    if (_cost[i - 1, j] + Costs.cD < _cost[i, j])
                    {
                        _cost[i, j] = _cost[i - 1, j] + Costs.cD;
                        _op[i, j] = F("delete", x[i - 1]);
                    }
                    
                    if (_cost[i, j - 1] + Costs.cI < _cost[i, j])
                    {
                        _cost[i, j] = _cost[i, j - 1] + Costs.cI;
                        _op[i, j] = F("insert", y[j - 1]);
                    }
                }

            return this;
        }

        /// <summary>
        /// The assemble transformation procedure.
        /// </summary>
        /// <param name="i">the highest index of the first string, &quot;i.e: x.Length - 1&quot;.</param>
        /// <param name="j">the highest index of the second string,  &quot;i.e: y.Length - 1&quot;.</param>
        /// <returns></returns>
        public string AssembleTransformation(int i, int j)
        {
            if (i == 0 && j == 0)
                return string.Empty;

            string lastMove = _op[i, j].Split(' ')[0];

            if (lastMove == "copy" || lastMove == "replace")
                return AssembleTransformation(i - 1, j - 1) + _op[i, j];

            if (lastMove == "delete")
                return AssembleTransformation(i - 1, j) + _op[i, j];

            return AssembleTransformation(i, j - 1) + _op[i, j];
        }

        #region Helpers

        /// <summary>
        /// A formatter helper.
        /// </summary>
        private Func<string, char, string> F = (op, ch) => string.Format("{0} {1}, \n", op, ch);

        /// <summary>
        /// A formatter helper for replace operation.
        /// </summary>
        private Func<string, char, char, string> FR = (op, chX, chY) => string.Format("{0} {1} by {2}, \n", op, chX, chY);

        #endregion
    }
}