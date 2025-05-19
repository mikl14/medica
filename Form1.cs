using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Vosk;
using NAudio.Wave;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
   

        private WaveInEvent waveIn;
        private VoskRecognizer recognizer;
        private Model model;

        public static List<Control> selectedListControl = new List<Control>();
        public static Dictionary<String, String> decoder;
        public Form1()
        {
            InitializeComponent();

            string modelPath = @"models\vosk-model-mod-ru-0.22";
            model = new Model(modelPath);

            // Создаём распознаватель с частотой 16000 Гц
            recognizer = new VoskRecognizer(model, 16000.0f);

            // Настраиваем захват с микрофона
            waveIn = new WaveInEvent();
            waveIn.DeviceNumber = 0; // номер микрофона, если несколько - можно выбирать
            waveIn.WaveFormat = new WaveFormat(16000, 1); // 16 kHz, моно
            waveIn.DataAvailable += WaveIn_DataAvailable;
            waveIn.RecordingStopped += WaveIn_RecordingStopped;
            waveIn.RecordingStopped += WaveIn_RecordingStopped;

        }

        private void ucEchocardioscopy1_Load(object sender, EventArgs e)
        {
            selectedListControl = GetControlsInGroupBox(this, "gbAorticValve");
            selectedListControl.RemoveAll(c => !(c is TextBox));

            Console.WriteLine(selectedListControl.Count);
        }

        public void SetTextBoxValue(Control control, string value)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

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
                throw new ArgumentException("Control is not a TextBox", nameof(control));
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

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            // Передаём аудио данные в распознаватель
            if (recognizer.AcceptWaveform(e.Buffer, e.BytesRecorded))
            {
                var result = RemoveSpecialCharactersExceptSpaces(recognizer.Result());


                String[] splitResult = result.Split(new char[] { ' ' });
                if(splitResult.Length > 1)
                {
                    if (Decoder.decoder.ContainsKey(splitResult[0]))
                    {
                        Decoder.decoder.TryGetValue(splitResult[0], out string value);

                        double ss = RussianNumberParcer.Parse(splitResult[1]);

                        if (selectedListControl.Any(c => value.Equals(c.Tag)))
                        {
                            Control control = selectedListControl.Find(c => c.Tag.Equals(value));

                            SetTextBoxValue(control, ss.ToString());
                        }
                    }
                }

            
               // AppendText(result);
            }
            else
            {
                var partial = recognizer.PartialResult();
                //AppendText(partial, partialResult: true);
            }
        }

        private void WaveIn_RecordingStopped(object sender, StoppedEventArgs e)
        {
            recognizer?.Dispose();
            model?.Dispose();
            waveIn?.Dispose();
        }



        public static string RemoveSpecialCharactersExceptSpaces(string input)
        {
            if (input == null)
                return null;

            // Удаляем все символы, кроме букв (латиница и кириллица), цифр и пробелов
            // Также удаляем символы переноса строки \r и \n
            string pattern = @"[^a-zA-Zа-яА-Я0-9 ]"; // пробел оставляем, переносы строки убираем
            string result = Regex.Replace(input, pattern, "");
            result = result.Replace("text", "");
            result = result.Trim();
            return result;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
         //   textBoxResults.Clear();
           // toolStripStatusLabel.Text = "Начинаем запись...";
            waveIn.StartRecording();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            waveIn?.StopRecording();
            waveIn?.Dispose();
            recognizer?.Dispose();
            model?.Dispose();
            base.OnFormClosing(e);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
       
          //  toolStripStatusLabel.Text = "Остановлено";
        }

        private void stopButton_Click_1(object sender, EventArgs e)
        {
            waveIn.StopRecording();
        }

    }
}

