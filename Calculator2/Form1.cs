using CuoreUI.Controls;

namespace Calculator2
{
    public partial class Form1 : Form
    {
        private double lastResult = 0;
        private readonly char leftBracket = '(';
        private readonly char rightBracket = ')';

        public Form1()
        {
            InitializeComponent();
        }

        private void addOperation_Click(object sender, EventArgs e)
        {
            cuiButton button = sender as cuiButton;
            if (button != null)
                resultTextBox.Content += button.Content;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(resultTextBox.Content)) // Verifica se o texto n�o est� vazio
            {
                resultTextBox.Content = resultTextBox.Content.Remove(resultTextBox.Content.Length - 1);
            }
        }

        private void resultButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Captura o texto da TextBox
                string expressao = resultTextBox.Content;

                // Avalia a express�o matem�tica
                var resultado = ResultOperations.CalcularExpressao(expressao);
                lastResult = resultado;

                // Mostra o resultado na TextBox
                resultTextBox.Content = resultado.ToString().Replace(',', '.');
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao calcular: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void resetTextButton_Click(object sender, EventArgs e)
        {
            resultTextBox.Content = string.Empty;
            lastResult = 0;
        }

        private void bracketButton_Click(object sender, EventArgs e)
        {
            var leftBracketCount = resultTextBox.Content.Count(x => x == leftBracket);
            var rightBracketCount = resultTextBox.Content.Count(x => x == rightBracket);

            var diff = leftBracketCount - rightBracketCount;

            resultTextBox.Content += diff == 0
                ? leftBracket
                : rightBracket;
        }

        private void lastResultButton_Click(object sender, EventArgs e)
        {
            resultTextBox.Content += lastResult;
        }

        private void PIButton_Click(object sender, EventArgs e)
        {
            resultTextBox.Content += Math.PI;
        }
    }
}
