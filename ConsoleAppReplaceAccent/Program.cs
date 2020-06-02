using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppReplaceAccent
{
    class Program
    {

        static void Main(string[] args)
        {
            var path = @"D:\Documentos\Cesgranrio\Evento Alinhamento\2 - Normalizado";
            var itens = Directory.GetFiles(path, "*.xlsx");
            Parallel.ForEach(itens, a => RenameFiles(a));
        }

        static void RenameFiles(string originalPath)
        {
            var destinyPath = RemoveDiacritics(originalPath);
            Console.WriteLine($"{originalPath} - {destinyPath}");
            File.Move(originalPath, destinyPath);
        }

        static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory == UnicodeCategory.NonSpacingMark)
                {
                    continue;
                }
                stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
