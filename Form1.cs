using Newtonsoft.Json;
using SpeechRecognision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Vosk;



namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

     //   public static List<Control> selectedListControl = new List<Control>();
        public static Dictionary<String, String> decoder;
        Recognizer recognizer;
        public Form1()
        {
            InitializeComponent();
            
            string modelPath = @"config\models\vosk-model-mod-ru-0.22";

            //       Dictionary<String,String> modelHash = ModelSecurity.GenerateHashes(modelPath+ "\\graph");

            ModelSecurity.VerifyHashes(modelPath);
            recognizer = new Recognizer(modelPath, false, true);
            recognizer.RecognitionResultReceived += Recognizer_RecognitionResultReceived;

            micBox.Items.AddRange(Recognizer.getDevices().ToArray());

            recognizer.setCodeDictionary(ReadJsonToDictionary(@"config\vocabulary.json"));
            stopButton.Enabled = false;
        }

        void Recognizer_RecognitionResultReceived(object sender, string result)
        {
            var data = result.Split('|', ' ');

            if (data.Length >= 2)
            {
                if (IsNumber(data[data.Length - 1]))
                {
                    for (int i = 0; i < data.Length - 1; i++)
                    {
                        SetTextBoxValue(FindControlRecursive(this, data[i]), data[data.Length - 1]);
                    }
                }
            }
                

         
            Console.WriteLine(result);
        }

        private void ucEchocardioscopy1_Load(object sender, EventArgs e)
        {
           // selectedListControl = GetControlsInGroupBox(this, "gbAorticValve");
       //     selectedListControl.RemoveAll(c => !(c is TextBox));

         //   Console.WriteLine(selectedListControl.Count);
        }

        public static Dictionary<string, string> ReadJsonToDictionary(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", filePath);
            }

            string jsonContent = File.ReadAllText(filePath);
            var originalDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent);

            var lowerDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var kvp in originalDict)
            {
                lowerDict[kvp.Key.ToLower()] = kvp.Value;
            }

            return lowerDict;
        }

        public void SetTextBoxValue(Control control, string value)
        {
            if (control == null)
                Console.WriteLine("trouble with control Incorect Name!");

            if (control is TextBox textBox)
            {
                if (textBox.InvokeRequired)
                {
                    textBox.Invoke(new Action(() => textBox.Text = value));
                }
                else
                {
                    textBox.Text = value;
                }
            }
            else
            {
                // Можно добавить логирование или обработку случая,
                // когда переданный контрол не является TextBox
                Console.WriteLine("Control is not a TextBox");
            //    throw new ArgumentException("Control is not a TextBox", nameof(control));
            }
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

        public static bool IsNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            double number;
            return double.TryParse(input, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out number);
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            recognizer.stopRecording();
            base.OnFormClosing(e);
        }


        private void stopButton_Click(object sender, EventArgs e)
        {
            recognizer.stopRecording();
            startButton.Enabled = true;
            stopButton.Enabled = false;
        }

        private void micBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Recognizer.setDeviceNumber(micBox.SelectedIndex);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            recognizer.startRecording();
            startButton.Enabled = false;
            stopButton.Enabled = true;
        }

        private void micBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

    }
}

