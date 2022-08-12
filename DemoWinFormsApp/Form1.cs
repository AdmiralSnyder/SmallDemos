namespace DemoWinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void helloButtons_Click(object sender, EventArgs e)
        {
            if (sender is Button b)
            {
                MessageBox.Show($"Hello {b.Text}");
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                BackColor = Color.Red;
            }
            else
            {
                BackColor = Color.GreenYellow;
            }
        }
    }
}