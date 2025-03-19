namespace Tokens
{
    public class Token
    {
        private TokenType Type { get; set; }
        private int Row {get; set;}
        private int Col {get; set;}
        private string Text;

        public Token(TokenType type, string text, int row, int col) {
            this.Type = type;
            this.Text = text;
            this.Row = row;
            this.Col = col;
        }

        public string position()
        {
            return $"[{Row} {Col}]";
        }

        public override string ToString()
        {
            return $"{Type} {position()} {Text}";
        }
    }
}