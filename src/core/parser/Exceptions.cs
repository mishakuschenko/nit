namespace Exceptions
{
    public class ParseException : Exception
    {
        public ParseException(string message) 
            : base(message) { }
    }

    public class LexerException : Exception
    {
        public LexerException(string message)
            : base(message) { }

        public LexerException(int row, int col, string message)
            : base($"[{row}:{col}] {message}") { }
    }       
}