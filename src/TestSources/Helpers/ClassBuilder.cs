
using System.Text;

namespace TestSources.Helpers
{
    internal class ClassBuilder
    {
        public const string NameSpace = "TestSources";
        private string _indent = "";
        private const int _indentSpaces = 4;
        private int _indentLevel;
        private bool _wasLastCallAppendLine = true;
        private bool _isFirstMember = true;
        private readonly StringBuilder _stringBuilder;

        public ClassBuilder()
        {
            _stringBuilder = new StringBuilder();
        }

        public void Clear()
        {
            _stringBuilder.Clear();
        }

        public int IndentLevel => _indentLevel;

        public void IncreaseIndent()
        {
            _indentLevel++;
            _indent += new string(' ', _indentSpaces);
        }

        public bool DecreaseIndent()
        {
            if (_indent.Length >= _indentSpaces)
            {
                _indentLevel--;
                _indent = _indent.Substring(_indentSpaces);
                return true;
            }

            return false;
        }

        public void AppendLineBeforeMember()
        {
            if (!_isFirstMember)
            {
                _stringBuilder.AppendLine();
            }

            _isFirstMember = false;
        }

        public void AppendLine(string line)
        {
            if (_wasLastCallAppendLine) // If last call was only Append, you shouldn't add the indent
            {
                _stringBuilder.Append(_indent);
            }

            _stringBuilder.AppendLine($"{line}");
            _wasLastCallAppendLine = true;
        }

        public void AppendLine()
        {
            _stringBuilder.AppendLine();
            _wasLastCallAppendLine = true;
        }

        public void Append(string stringToAppend)
        {
            if (_wasLastCallAppendLine)
            {
                _stringBuilder.Append(_indent);
                _wasLastCallAppendLine = false;
            }

            _stringBuilder.Append(stringToAppend);
        }

        public override string ToString() => _stringBuilder.ToString();
    }
}
