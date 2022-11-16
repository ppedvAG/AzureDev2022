using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteineRechnungenReader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Hello State of Azure");

            var filename = "..\\..\\..\\SteineRechnung.pdf";
            var recognizeClient = AuthenticateClient();

            RecognizedFormCollection recognizedForms = await recognizeClient.StartRecognizeCustomForms(modelId, File.OpenRead(filename)).WaitForCompletionAsync();

            foreach (FormPage page in recognizedForms.First().Pages)
            {
                for (int i = 0; i < page.Tables.Count; i++)
                {
                    FormTable table = page.Tables[i];
                    Console.WriteLine($"Table {i} has {table.RowCount} rows and {table.ColumnCount} columns.");
                    foreach (FormTableCell cell in table.Cells)
                    {
                        Console.WriteLine($"    Cell ({cell.RowIndex}, {cell.ColumnIndex}) contains text: '{cell.Text}'.");
                    }
                }

                Console.WriteLine($"Preis: {recognizedForms.First().Fields["Preis"].Value.AsFloat()}");
                Console.WriteLine($"Name: {recognizedForms.First().Fields["Name"].Value.AsString()}");
                Console.WriteLine($"Telefon: {recognizedForms.First().Fields["Telefon"].Value.AsString()}");
            }



            Console.WriteLine("Ende");
            Console.ReadLine();
        }

        static string modelId = "";


        static private FormRecognizerClient AuthenticateClient()
        {
            string endpoint = "";
            string apiKey = "";
            var credential = new AzureKeyCredential(apiKey);
            var client = new FormRecognizerClient(new Uri(endpoint), credential);
            return client;
        }
    }
}
