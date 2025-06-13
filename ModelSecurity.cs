using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

public class ModelSecurity
{
    private static Dictionary<string, string> referenceHashes = new Dictionary<string, string>
    {
        { @"config\models\vosk-model-mod-ru-0.22\graph\disambig_tid.int", "C3AF3DBC5EB301C4B282480D785C04B1642A8DB83210A12332E9790E50146FD2" },
        { @"config\models\vosk-model-mod-ru-0.22\graph\Gr.fst", "077154DB7B88FE6989FED43E7BC2242308B49413DE5A22B6D1546EF0CA8127ED" },
        { @"config\models\vosk-model-mod-ru-0.22\graph\HCLr.fst", "C070E55494BE2E88F73720A61EE801C7918D24F92209C04DBCD547B9ABC54860" },
        { @"config\models\vosk-model-mod-ru-0.22\graph\phones\word_boundary.int", "23081404FE1CC7338C013A4BEEC133637147D60472C06AB64FF88EE3D028A7B5" }
        };

    // Метод рекурсивного обхода и вычисления хешей всех файлов
    public static Dictionary<string, string> GenerateHashes(string rootDirectory)
    {
        if (!Directory.Exists(rootDirectory))
            throw new DirectoryNotFoundException($"Директория не найдена: {rootDirectory}");

        var hashes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var allFiles = Directory.GetFiles(rootDirectory, "*.*", SearchOption.AllDirectories);

        using (var sha256 = SHA256.Create())
        {
            foreach (var filePath in allFiles)
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                byte[] hashBytes = sha256.ComputeHash(fileBytes);
                string hashHex = BitConverter.ToString(hashBytes).Replace("-", "");

                // Относительный путь от корневой папки, чтобы не зависеть от абсолютного пути
                string relativePath = filePath;

                hashes[relativePath] = hashHex;
            }
        }

        return hashes;
    }

    // Метод проверки файлов по эталонным хешам
    public static void VerifyHashes(string rootDirectory)
    {
        if (!Directory.Exists(rootDirectory))
            throw new DirectoryNotFoundException($"Директория не найдена: {rootDirectory}");

        using (var sha256 = SHA256.Create())
        {
            foreach (var kvp in referenceHashes)
            {
                string relativePath = kvp.Key;
                string expectedHash = kvp.Value;

                if (!File.Exists(relativePath))
                    throw new FileNotFoundException($"Файл не найден: {relativePath}");

                byte[] fileBytes = File.ReadAllBytes(relativePath);
                byte[] actualHashBytes = sha256.ComputeHash(fileBytes);
                string actualHash = BitConverter.ToString(actualHashBytes).Replace("-", "");

                if (!string.Equals(actualHash, expectedHash, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Модель не соотвествует заданной!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new InvalidOperationException(
                        $"Хеш файла '{relativePath}' не совпадает с эталоном. Ожидалось: {expectedHash}, получено: {actualHash}");
                }
            }
        }
    }
}
