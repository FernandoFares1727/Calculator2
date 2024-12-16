namespace Calculator2
{
    public class ResultOperations
    {
        public static double CalcularExpressao(string expressao)
        {
            // Substitui o operador de potência
            expressao = SubstituirPotencia(expressao);

            // Substitui a raiz quadrada
            expressao = SubstituirRaizQuadrada(expressao);

            // Usa DataTable para calcular a expressão
            System.Data.DataTable tabela = new System.Data.DataTable();
            tabela.Columns.Add("Expressao", typeof(string), expressao);
            System.Data.DataRow linha = tabela.NewRow();
            tabela.Rows.Add(linha);

            // Calcula e arredonda o valor final em até 4 casas decimais
            return double.Parse((string)linha["Expressao"]);
        }

        private static string SubstituirPotencia(string expressao)
        {
            while (expressao.Contains("^"))
            {
                // Localiza a posição do operador `^`
                int index = expressao.IndexOf("^");

                // Identifica o operando à esquerda
                int start = index - 1;
                while (start >= 0 && (char.IsDigit(expressao[start]) || expressao[start] == '.'))
                {
                    start--;
                }
                start++;

                // Identifica o operando à direita
                int end = index + 1;
                while (end < expressao.Length && (char.IsDigit(expressao[end]) || expressao[end] == '.'))
                {
                    end++;
                }

                // Extrai os operandos
                string baseValue = expressao.Substring(start, index - start);
                string exponent = expressao.Substring(index + 1, end - index - 1);

                // Substitui na expressão
                string potencia = Math.Pow(double.Parse(baseValue), double.Parse(exponent)).ToString();
                expressao = expressao.Substring(0, start) + potencia + expressao.Substring(end);
            }

            return expressao;
        }

        private static string SubstituirRaizQuadrada(string expressao)
        {
            while (expressao.Contains("√"))
            {
                // Localiza a posição do operador `√`
                int index = expressao.IndexOf("√");

                // Identifica o operando à direita da raiz
                int end = index + 1;
                while (end < expressao.Length && (char.IsDigit(expressao[end]) || expressao[end] == '.'))
                {
                    end++;
                }

                // Extrai o operando
                string valor = expressao.Substring(index + 1, end - index - 1);

                // Calcula a raiz quadrada
                string raiz = Math.Sqrt(double.Parse(valor)).ToString().Replace(',', '.');

                // Substitui na expressão
                expressao = expressao.Substring(0, index) + raiz + expressao.Substring(end);
            }

            return expressao;
        }
    }
}
