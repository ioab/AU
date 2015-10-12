namespace Demo
{
    using AU.AlgorithmsOnStrings;
    using System;

    internal class AlgorithmsOnStrings
    {
        public static void Demo()
        {
            #region LCS

            string x = "I love you honey good night";
            string y = "love you g night";

            var table = LCS.ComputeLCSTable(x, y);

            var lcs = LCS.AssembleLCS(x, y, table, x.Length - 1, y.Length - 1);

            Console.WriteLine("--> x  : {0} \n--> y  : {1} \n--> LCS: {2}", x, y, lcs);

            Console.WriteLine
            (
                "--> x  : {0} \n--> y  : {1} \n--> LCS: {2}", "Hi", "",
                LCS.AssembleLCS("Hi", "", LCS.ComputeLCSTable("Hi", ""), 2 - 1, 0 - 1)
            );
            Console.WriteLine();

            #endregion

            #region Transformation on Strings

            string w = "ACAAGC", k = "CCGT";

            var t = new Transformation().ComputeTransformTable(w, k);

            var res = t.AssembleTransformation(w.Length, k.Length);

            Console.WriteLine("Lowest cost to transform {0} to {1}:\n{2}", w, k, res);


            // Another example .. the 1st in the book //

            Console.WriteLine(
                "Lowest cost to transform ATGATCGGCAT to CAATGTGAATC:\n{0}",
                new Transformation()
                    .ComputeTransformTable("ATGATCGGCAT", "CAATGTGAATC")
                        .AssembleTransformation(11, 11)
            );

            #endregion

            #region String Matching

            Console.WriteLine("\n[[using Simple-Matcher ]]");
            foreach (var shift in Matching.SimpleMatcher("GTAACAGTAAACG", "AAC"))
                Console.WriteLine(shift);

            foreach (var shift in Matching.SimpleMatcher("", ""))
                Console.WriteLine(shift);

            Console.WriteLine("\n[[using FA ]]");
            var nextState = Matching.BuildNextStateTable("AAC");

            foreach (var match in Matching.FAStringMatcher("GTAACAGTAAACG", 13, 3, nextState))
                Console.WriteLine(match);

            #endregion

        }
    }
}