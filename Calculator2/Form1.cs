using CuoreUI.Controls;

namespace Calculator2
{
    public partial class Form1 : Form
    {
        private double lastResult = 0;
        private readonly char leftBracket = '(';
        private readonly char rightBracket = ')';

        // Dimensões iniciais do formulário
        private Size initialFormSize;
        private Rectangle[] initialControlBounds;
        private float[] initialFontSizes;

        public Form1()
        {
            InitializeComponent();

            // Salva as dimensões iniciais do formulário
            initialFormSize = this.ClientSize;

            // Salva as dimensões e posições iniciais de cada controle
            initialControlBounds = new Rectangle[Controls.Count];
            initialFontSizes = new float[Controls.Count];
            for (int i = 0; i < Controls.Count; i++)
            {
                initialControlBounds[i] = Controls[i].Bounds;
                initialFontSizes[i] = Controls[i].Font.Size;
            }
        }

        private void addOperation_Click(object sender, EventArgs e)
        {
            cuiButton button = sender as cuiButton;
            if (button != null)
                resultTextBox.Content += button.Content;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(resultTextBox.Content)) // Verifica se o texto não está vazio
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

                // Avalia a expressão matemática
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

        private void resizeControl(object sender, EventArgs e)
        {
            // Obtém as dimensões atuais do formulário
            int currentFormWidth = this.ClientSize.Width;
            int currentFormHeight = this.ClientSize.Height;

            // Calcula as proporções de redimensionamento
            float widthRatio = (float)currentFormWidth / initialFormSize.Width;
            float heightRatio = (float)currentFormHeight / initialFormSize.Height;

            // Ajusta cada controle com base nos valores originais
            for (int i = 0; i < Controls.Count; i++)
            {
                Control control = Controls[i];
                Rectangle originalBounds = initialControlBounds[i];

                control.Left = (int)(originalBounds.Left * widthRatio);
                control.Top = (int)(originalBounds.Top * heightRatio);
                control.Width = (int)(originalBounds.Width * widthRatio);
                control.Height = (int)(originalBounds.Height * heightRatio);

                // Redimensiona a fonte
                float originalFontSize = initialFontSizes[i];
                float newFontSize = originalFontSize * Math.Min(widthRatio, heightRatio);
                control.Font = new Font(control.Font.FontFamily, newFontSize, control.Font.Style);
            }

            this.Refresh();
        }
    }
}
