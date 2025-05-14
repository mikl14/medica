using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public static List<Control> selectedListControl = new List<Control>();
        public static Dictionary<String, String> decoder;
        public Form1()
        {
            InitializeComponent();
        }

        private void ucEchocardioscopy1_Load(object sender, EventArgs e)
        {

           
           selectedListControl =  GetControlsInGroupBox(this, "gbAorticValve");
           selectedListControl.RemoveAll(c => !(c is TextBox));

            Decoder.decoder.TryGetValue("Кот", out string value);

            double ss = RussianNumberParcer.Parse("двести двадцать семь");

            if (selectedListControl.Any(c => value.Equals(c.Tag)))
            {
                SetTextBoxValue(selectedListControl.Find(c => c.Tag.Equals("AorticValveMaxVelocity")), ss.ToString());
            }
                
           Console.WriteLine(selectedListControl.Count);
        }

        public void SetTextBoxValue(Control control, string value)
        {
            if (control is TextBox textBox)
            {
                textBox.Text = value;
            }
            // Можно добавить else или логирование, если нужно обработать случай, когда контрол не найден или не TextBox
        }

        public List<Control> GetControlsInGroupBox(Control parent, string groupBoxName)
        {
            // Найти GroupBox по имени среди всех контролов (рекурсивно)
            GroupBox groupBox = FindControlRecursive(parent, groupBoxName) as GroupBox;
            if (groupBox == null)
                return new List<Control>(); // Если не найден, вернуть пустой список

            // Вернуть все дочерние элементы этого GroupBox
            return groupBox.Controls.Cast<Control>().ToList();
        }

        // Рекурсивный поиск контрола по имени
        private Control FindControlRecursive(Control parent, string name)
        {
            foreach (Control control in parent.Controls)
            {
                if (control.Name == name)
                    return control;

                Control found = FindControlRecursive(control, name);
                if (found != null)
                    return found;
            }
            return null;
        }
    }
}
