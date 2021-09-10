using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MassTransit;
using Newtonsoft.Json;
using Exchange;
using System;
using System.Globalization;
using TransactionStore.Business.Constants;

namespace TransactionStore.Business.Services
{
    public class CurrencyRatesService : ICurrencyRatesService
    {
        private const string _dateFormat = "dd.MM.yyyy HH:mm";
        private string _apDir = "./";
        private const string _fileName = "CurrencyRates.json";
        private string _fullPath;

        public RatesExchangeModel RatesModel { get; set; }
        public CurrencyRatesService()
        {
            _fullPath = Path.Combine(_apDir, _fileName);
            LoadCurrencyRates();
        }

        public void LoadCurrencyRates()
        {
            if (RatesModel == null)
            {
                var rates = ReadCurrencyQuotes(_fullPath);
                RatesModel = rates;
            }
        }

        public void SaveCurrencyRates(RatesExchangeModel rates)
        {
            WriteCurrencyQuotes(rates, _apDir, _fullPath);
        }

        private void WriteCurrencyQuotes(RatesExchangeModel currencyRates, string directoryPath, string filePath)
        {
            var json = SerializeCurrency(currencyRates);
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

        private RatesExchangeModel ReadCurrencyQuotes(string filePath)
        {
            var dataCurrency = new RatesExchangeModel();
            var json = ReadFile(filePath);
            if (json != null)
            {
                dataCurrency = DeserializeCurrency(json);
            }
            else
            {
                throw new Exception(string.Format(ServiceMessages.CannotReadFileMessage, filePath));
            }
            return dataCurrency;
        }

        private string SerializeCurrency(RatesExchangeModel currency)
        {
            return JsonConvert.SerializeObject(currency);
        }

        private RatesExchangeModel DeserializeCurrency(string json)
        {
            return JsonConvert.DeserializeObject<RatesExchangeModel>(json);
        }

        private bool WriteFile(string path, string json)
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
            catch
            {
                //ignore
            }
            return isOk;
        }

        private string ReadFile(string path)
        {
            var json = string.Empty;
            try
            {
                using (var sReader = new StreamReader(path))
                {
                    json = sReader.ReadToEnd();
                }
            }
            catch
            {
                //ignore
            }
            return json;
        }

        private bool CheckDirectory(string path)
        {
            return Directory.Exists(path);
        }

        private bool CreateDirectory(string path)
        {
            var isOk = false;
            try
            {
                Directory.CreateDirectory(path);
                isOk = true;
            }
            catch
            {
                //ignore
            }
            return isOk;
        }
    }
}