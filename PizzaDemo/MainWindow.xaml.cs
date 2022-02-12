using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfGraphControl
{
    /// <summary>
    /// Interaction logic for PizzaWindow.xaml
    /// </summary>
    public partial class PizzaWindow : Window
    {
        public PizzaWindow()
        {
            InitializeComponent();

            // just add a checkbox at runtime
            CheckBox PepperoniCB = new() { Content = "Pepperoni", Tag = "1" };
            (Content as StackPanel)?.Children.Add(PepperoniCB);

            foreach (var tb in new ToggleButton[]
            {
                BigRB, BiggerRB, HungryRB, CheeseCB, SalamiCB, TunaCB, PepperoniCB
            })
            {
                tb.Checked += RadioButton_OnCheckedChange;
                tb.Unchecked += RadioButton_OnCheckedChange;
            }
        }

        private int Price;

        private void RadioButton_OnCheckedChange(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton tb)
            {

                //if (int.TryParse("1234", out int oneTwoThreeFour))
                //{
                //    //oneTwoThreeFour == 1234;
                //}

                Price += (int.TryParse(tb.Tag as string, out var value) ? value : 0)
                    * (tb.IsChecked ?? false ? 1 : -1); // handle adding and removing

                // TODO do it properly with binding
                PriceTB.Text = Price.ToString();
            }
        }
    }
}
