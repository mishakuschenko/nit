using System.Collections;
using Tokens;
using Exceptions;
using System.Text;

namespace Lexer
{
    public class Lexer
    {
        private static string OperatorChars = "+-*/%()[]{}=<>!&|,^~?:";
        private static readonly Dictionary<string, TokenType> Operators;
        static Lexer()
        {
            Operators = new Dictionary<string, TokenType>
            {
                {"+", TokenType.PLUS},
                {"-", TokenType.MINUS},
                {"*", TokenType.STAR},
                {"/", TokenType.SLASH},
                {"%", TokenType.PERCENT},
                {"(", TokenType.LPAREN},
                {")", TokenType.RPAREN},
                {"[", TokenType.LBRACKET},
                {"]", TokenType.RBRACKET},
                {"{", TokenType.LBRACE},
                {"}", TokenType.RBRACE},
                {"=", TokenType.EQ},
                {"<", TokenType.LT},
                {">", TokenType.GT},
                {",", TokenType.COMMA},
                {"^", TokenType.CARET},
                {"~", TokenType.TILDE},
                {"?", TokenType.QUESTION},
                {":", TokenType.COLON},
                {"!", TokenType.EXCL},
                {"&", TokenType.AMP},
                {"|", TokenType.BAR},
                {"==", TokenType.EQEQ},
                {"!=", TokenType.EXCLEQ},
                {"<=", TokenType.LTEQ},
                {">=", TokenType.GTEQ},
                {"&&", TokenType.AMPAMP},
                {"||", TokenType.BARBAR},
                {"<<", TokenType.LTLT},
                {">>", TokenType.GTGT},
                {">>>", TokenType.GTGTGT},
            };
        }
        private string input;
        private int len;

        private List<Token> tokens;

        private int pos;
        private int row, col;

        public Lexer(string input) 
        {
            this.input = input;
            len = input.Length;
            tokens = new List<Token>();

            row = col = 1;
        }

        public List<Token> Tokenize() {}

        private void TokenizeNumber() {}

        private void TokenizeHexNumber() {}

        private static bool IsHex(char current) {}

        private void TokenizeOperator() {}

        private void TokenizeWord() 
        {
            StringBuilder buffer = new StringBuilder();
            char current = peek(0);
            while (true)
            {
                if (!Char.IsLetterOrDigit(current) && (current!='_') && (current!='$')) break;
                buffer.Append(current);
                current = next();
            }
            string word = buffer.ToString();
            switch (word)
            {
                case "print": AddToken(TokenType.PRINT); break;
                case "if": AddToken(TokenType.IF); break;
                case "else": AddToken(TokenType.ELSE); break;
                case "while": AddToken(TokenType.WHILE); break;
                case "for": AddToken(TokenType.FOR); break;
                case "do": AddToken(TokenType.DO); break;
                case "break": AddToken(TokenType.BREAK); break;
                case "continue": AddToken(TokenType.CONTINUE); break;
                case "def": AddToken(TokenType.DEF); break;
                case "return": AddToken(TokenType.RETURN); break;
                case "use": AddToken(TokenType.USE); break;
                
                default: 
                    AddToken(TokenType.WORD, word);
                    break;
            } 
        }

        private void TokenizeText() 
        {
            next();
            StringBuilder buffer = new StringBuilder();
            char current = peek(0);
            while (true)
            {
                if (current == '\0') throw Error("Reached end of file while parsing text string.");
                if (current == '\\') {
                    current = next();
                    switch (current) {
                        case '"': current = next(); buffer.Append('"'); continue;
                        case 'n': current = next(); buffer.Append('\n'); continue;
                        case 't': current = next(); buffer.Append('\t'); continue;
                    }
                    buffer.Append('\\');
                    continue;
                }
                if (current == '"') break;
                buffer.Append(current);
                current = next();
            }
            next();

            AddToken(TokenType.TEXT, buffer.ToString());
        }

        private void TokenizeComment() 
        {
            char current = peek(0);
            while("\r\n\0".IndexOf(current) == -1) current = next();
        }

        private void TokenizeMultilineComment() 
        {
            char current = peek(0);
            while (true)
            {
                if (current == '\0') throw Error("Reached end of file while parsing multiline comment");
                if (current == '*' && peek(1) == '/') break;
                current = next();
            }
            next();
            next();
        }

        private char next() 
        {
            pos++;
            char result = peek(0);
            if (result == '\n') // END OF STRING CODE -->  `print()` or `print();` 
            {
                row++;
                col = 1;
            } else col++;   
            return result;
        }

        private char peek(int RelativePosition) 
        {
            int position = pos + RelativePosition;
            if (position >= len) return '\0';
            return input[position];
        }

        private void AddToken(TokenType type) 
        {
            AddToken(type, "");
        }

        private void AddToken(TokenType type, string text) 
        {
            tokens.Add(new Token(type, text, row, col));
        }

        private LexerException Error(string text) 
        {
            return new LexerException(row, col, text);
        }
    }
}