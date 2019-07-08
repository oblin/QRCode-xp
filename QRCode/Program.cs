using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace QRCode
{
    class Program
    {
        static void Main(string[] args)
        {
            if ((args.Length % 2 ) != 0 )
            {
                Console.WriteLine("參數必須要雙數，請重新輸入");
                return;
            }

            for (var i = 0; i < args.Length; i += 2)
            {
                var content = args[i];
                var fileName = args[i + 1];


                var file = CreateSaveFile(fileName);
                CreateQRCode(content, file);
            }

            //ReadQRCode(file);
        }

        private static string CreateSaveFile(string fileName)
        {
            return GetImportBaseDir() + fileName + ".jpeg";
        }

        private static void ReadQRCode(string file)
        {
            Bitmap bitmap = new Bitmap(file);

            BarcodeReader reader = new BarcodeReader { AutoRotate = true };
            Result result = reader.Decode(bitmap);

            Console.WriteLine("Result: {0}", result.Text);
        }

        private static void CreateQRCode(string content, string file)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 300,
                Height = 300
            };

            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                
                Options = options
            };

            Bitmap bitmap = writer.Write(content);
            bitmap.Save(file, ImageFormat.Jpeg);
        }

        private static string GetImportBaseDir()
        {
            var baseDir = ConfigurationManager.AppSettings["FileDir"];
            if (!baseDir.EndsWith(@"\"))
                baseDir += @"\";

            return baseDir;
        }
    }
}
