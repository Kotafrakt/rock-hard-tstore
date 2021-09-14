using Exchange;
using Newtonsoft.Json;
using System;
using System.IO;
using TransactionStore.Business.Constants;

namespace TransactionStore.Business.Helpers
{
    public static class CurrencyRatesHelper
    {
        public static void WriteCurrencyRates(RatesExchangeModel currencyRates, string directoryPath, string filePath)
        {
            var json = SerializeCurrencyRates(currencyRates);
            if (!CheckDirectory(directoryPath))
            {
                if (!CreateDirectory(directoryPath))
                {
                    throw new Exception(string.Format(ServiceMessages.CannotCreateDirectoryMessage, json, filePath));
                }
            }
            if (!WriteFile(filePath, json))
            {
                throw new Exception(string.Format(ServiceMessages.CannotWriteFileMessage, json, filePath));
            }
        }

        public static RatesExchangeModel ReadCurrencyRates(string filePath)
        {
            var dataCurrency = new RatesExchangeModel();
            var json = ReadFile(filePath);
            if (json != null)
            {
                dataCurrency = DeserializeCurrencyRates(json);
            }
            else
            {
                throw new Exception(string.Format(ServiceMessages.CannotReadFileMessage, filePath));
            }
            return dataCurrency;
        }

        private static string SerializeCurrencyRates(RatesExchangeModel currency)
        {
            return JsonConvert.SerializeObject(currency);
        }

        private static RatesExchangeModel DeserializeCurrencyRates(string json)
        {
            return JsonConvert.DeserializeObject<RatesExchangeModel>(json);
        }

        private static bool WriteFile(string path, string json)
        {
            var isOk = false;
            try
            {
                using (var sWriter = new StreamWriter(path))
                {
                    sWriter.WriteLine(json);
                    isOk = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isOk;
        }

        private static string ReadFile(string path)
        {
            var json = string.Empty;
            try
            {
                using (var sReader = new StreamReader(path))
                {
                    json = sReader.ReadToEnd();
                }
            }
            catch (FileNotFoundException ex)
            {
                throw ex;
            }
            catch (DirectoryNotFoundException ex)
            {
                throw ex;
            }
            return json;
        }

        private static bool CheckDirectory(string path)
        {
            return Directory.Exists(path);
        }

        private static bool CreateDirectory(string path)
        {
            var isOk = false;
            try
            {
                Directory.CreateDirectory(path);
                isOk = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isOk;
        }
    }
}