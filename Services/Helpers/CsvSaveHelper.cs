using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class CsvSaveHelper<TModel>
    {
        public async Task CreateCsvFileASync(string filderPath, string name)
        {
            string path = Path.Combine(filderPath, $"{name}.csv");
            FileStream fileStream = await Task.Run(() => File.Create(path));
            await fileStream.DisposeAsync();
            CreateCsvHeader(path);
        }

        public async Task SaveAsync(string path, IEnumerable<TModel> items)
        {
            using (var writer = new StreamWriter(path, true))
            using (var csvWriter = new CsvWriter(writer,  CultureInfo.InvariantCulture))
            {         
                await CreateRecordsAsync(csvWriter, items);
            }
        }

        public async Task ResetAsync(string path)
        {
            using (var writer = new StreamWriter(path))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                CreateCsvHeader(path);
                await csvWriter.WriteRecordsAsync("");
            }
        }

        private static void CreateCsvHeader(string path)
        {
            using (var writer = new StreamWriter(path))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteHeader<TModel>();
            }
        }

        private static async Task CreateRecordsAsync(CsvWriter csvWriter, IEnumerable<TModel> items)
        {
            await csvWriter.WriteRecordsAsync(items);
        }
    }
}
