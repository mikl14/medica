using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Vosk;

namespace SpeechRecognision
{
    public class Recognizer
    {
        private static WaveInEvent waveIn;
        private VoskRecognizer recognizer;
        private Model model;
        private bool enableRusNumbers, enableWordToCode;

        private string lastResult = "";

        public Recognizer(String modelPath, bool enableRusNumbers = false, bool enableWordToCode = false)
        {
            this.model = new Model(modelPath);
            this.enableRusNumbers = enableRusNumbers;
            this.enableWordToCode = enableWordToCode;

            recognizer = new VoskRecognizer(model, 16000.0f);

            // Настраиваем захват с микрофона
            waveIn = new WaveInEvent();
            waveIn.DeviceNumber = 0; // номер микрофона, если несколько - можно выбирать
            waveIn.WaveFormat = new WaveFormat(16000, 1); // 16 kHz, моно
            waveIn.DataAvailable += WaveIn_DataAvailable;
            waveIn.RecordingStopped += WaveIn_RecordingStopped;
        }

        public void setCodeDictionary(Dictionary<String, String> keyValues)
        {
            Decoder.wordToCode = keyValues;
        }

        public static void setDeviceNumber(int number)
        {
            waveIn.DeviceNumber = number;
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (recognizer.AcceptWaveform(e.Buffer, e.BytesRecorded))
            {
                var result = RemoveSpecialCharactersExceptSpaces(recognizer.Result());
                lastResult = result; // сохраняем результат в поле класса

                if (enableRusNumbers)
                {
                    try
                    {
                        result = RussianNumberParcer.Parse(result).ToString();
                    }
                    catch (Exception ex)
                    {

                    }
                }

                if (enableWordToCode)
                {
                    try
                    {
                        result = Decoder.decode(result);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                OnRecognitionResult(result); // вызываем метод-обработчик результата (опционально)

            }
            else
            {
                var partial = recognizer.PartialResult();
                // Можно также сохранять или обрабатывать частичный результат
            }
        }

        // Пример метода-обработчика, который можно использовать для уведомления других компонентов
        protected virtual void OnRecognitionResult(string result)
        {
            RecognitionResultReceived?.Invoke(this, result);
        }

        // Событие для передачи результата наружу
        public event EventHandler<string> RecognitionResultReceived;

        private void WaveIn_RecordingStopped(object sender, StoppedEventArgs e)
        {
        }

        public static List<String> getDevices()
        {
            int deviceCount = WaveIn.DeviceCount;

            List<String> devices = new List<String>();

            for (int i = 0; i < deviceCount; i++)
            {
                var deviceInfo = WaveIn.GetCapabilities(i);
                devices.Add(deviceInfo.ProductName);
                //Console.WriteLine($"Устройство {i}: {deviceInfo.ProductName}, каналы: {deviceInfo.Channels}");
            }

            return devices;
        }

        public void stopRecording()
        {
            waveIn.StopRecording();
        }

        public void startRecording()
        {
            waveIn.StartRecording();
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
    }
}
