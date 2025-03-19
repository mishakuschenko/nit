using System.Collections.Generic;   


namespace Variables
{
    public class Variables 
    {
        private static readonly Dictionary<string, double> variables;
        static Variables() 
        {
            variables = new Dictionary<string, double>
            {
                {"pi", 3.1415},
            };
        }
    }
}