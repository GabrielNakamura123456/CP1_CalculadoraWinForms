using System;
using System.Windows.Forms;

namespace WindowsFormsCalc
{
    public partial class Form1 : Form
    {
        private double storedValue = 0;     // valor armazenado
        private string pendingOp = null;    // operação pendente
        private bool isNewEntry = true;     // controle de novo número

        public Form1()
        {
            InitializeComponent();
            txtDisplay.Text = "0";
        }

        // ========= MÉTODOS DE BOTÕES =========

        // Números
        private void btn0_Click(object sender, EventArgs e) => AddDigit("0");
        private void btn1_Click(object sender, EventArgs e) => AddDigit("1");
        private void btn2_Click(object sender, EventArgs e) => AddDigit("2");
        private void btn3_Click(object sender, EventArgs e) => AddDigit("3");
        private void btn4_Click(object sender, EventArgs e) => AddDigit("4");
        private void btn5_Click(object sender, EventArgs e) => AddDigit("5");
        private void btn6_Click(object sender, EventArgs e) => AddDigit("6");
        private void btn7_Click(object sender, EventArgs e) => AddDigit("7");
        private void btn8_Click(object sender, EventArgs e) => AddDigit("8");
        private void btn9_Click(object sender, EventArgs e) => AddDigit("9");

        private void btnDot_Click(object sender, EventArgs e) => AddDigit(".");

        // Operações básicas
        private void btnPlus_Click(object sender, EventArgs e) => SetOperation("+");
        private void btnMinus_Click(object sender, EventArgs e) => SetOperation("-");
        private void btnMul_Click(object sender, EventArgs e) => SetOperation("*");
        private void btnDiv_Click(object sender, EventArgs e) => SetOperation("/");

        // Potência e raiz
        private void btnPow_Click(object sender, EventArgs e) => SetOperation("^");

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double current))
            {
                if (current < 0)
                {
                    MessageBox.Show("Não é possível calcular raiz de número negativo!", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                txtDisplay.Text = Math.Sqrt(current).ToString();
                isNewEntry = true;
            }
        }

        // Igual
        private void btnEquals_Click(object sender, EventArgs e)
        {
            if (pendingOp == null) return;

            if (double.TryParse(txtDisplay.Text, out double current))
            {
                storedValue = ApplyOperation(storedValue, current, pendingOp);
                txtDisplay.Text = storedValue.ToString();
                pendingOp = null;
                isNewEntry = true;
            }
        }

        // Limpar
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            storedValue = 0;
            pendingOp = null;
            isNewEntry = true;
        }

        // Sobre
        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Calculadora Semi-Científica\n" +
                "Integrantes:\n" +
                "Julio César Dias Vilella - RM560494\nGuilherme Costeira Braganholo - RM560628\nGabriel Nakamura Ogata - RM560671\n\n");
        }

        // ========= MÉTODOS AUXILIARES =========

        private void AddDigit(string digit)
        {
            if (isNewEntry)
            {
                txtDisplay.Text = (digit == ".") ? "0." : digit;
                isNewEntry = false;
            }
            else
            {
                if (digit == "." && txtDisplay.Text.Contains(".")) return;
                txtDisplay.Text += digit;
            }
        }

        private void SetOperation(string op)
        {
            if (double.TryParse(txtDisplay.Text, out double current))
            {
                if (pendingOp != null && !isNewEntry)
                {
                    storedValue = ApplyOperation(storedValue, current, pendingOp);
                    txtDisplay.Text = storedValue.ToString();
                }
                else
                {
                    storedValue = current;
                }

                pendingOp = op;
                isNewEntry = true;
            }
        }

        private double ApplyOperation(double a, double b, string op)
        {
            switch (op)
            {
                case "+": return a + b;
                case "-": return a - b;
                case "*": return a * b;
                case "/": return b == 0 ? double.NaN : a / b;
                case "^": return Math.Pow(a, b);
                default: return b;
            }
        }
    }
}