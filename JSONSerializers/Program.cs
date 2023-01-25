using System;
using System.Text.Json;
using System.Net;
using Newtonsoft.Json;

namespace JSONSerializers
{
    class Program
    {
        static void Main(string[] args)
        {
            //Pobieranie danych ze strony internetowej 

            string[] d = File.ReadAllLines(@"C:\Users\Developer\Desktop\API\API\bin\Debug\net6.0\input.txt.txt");
            Console.WriteLine(d);

            var picture = new PictureClass();

            for (int i = 0; i <= d.Length - 1; i++)
            {
                string id = d[i];
                var a = _download_serialized_json_data<JsonTest>(id);

                Console.WriteLine($"{a.safe_title}");

                using (WebClient webClient = new WebClient())
                {
                    byte[] dataArr = webClient.DownloadData($"{a.img}");
                    File.WriteAllBytes(@$"C:\Users\Developer\Desktop\API\API\bin\Debug\net6.0\output\path{i + 1}.png", dataArr);
                }
            }
            #region 1
            //XmlSerializer ser = new XmlSerializer(typeof(Orders));
            //Orders ap;
            //using (StreamReader reader = new StreamReader(@"C:\Users\Developer\Downloads\test\noprocess_aae7366e3279ed93-51c62748_BATCH_MANUAL-2SHORD_A78IDRIVE.xml", Encoding.UTF8))
            //{
            //    ap = (Orders)ser.Deserialize(reader);
            //}

            //string fileName = @"C:\Users\Developer\Downloads\test\fulfillment.json";
            //string jsonString = File.ReadAllText(fileName);

            //JToken fulfillment = JToken.Parse(jsonString);

            //List<AsnPackage> asnPackages = new List<AsnPackage>();

            //JArray jsonPackages = fulfillment["packages"] as JArray;
            //foreach (JToken jsonPackage in jsonPackages)
            //{
            //    var SipDataCarrierNumberMethod = new AsnPackage
            //    {
            //        ShippingDate = (DateTime)fulfillment["dateShipped"],
            //        ShippingCarrier = (string)fulfillment["shippingService"],
            //        TrackingNumber = (string)jsonPackage["trackingNumber"],
            //    };

            //    JArray items = jsonPackage["items"] as JArray;
            //    List<Detail> details = new List<Detail>();
            //    foreach (JToken item in items)
            //    {
            //        string sku = (string)item["sku"];

            //        var x = ap.Details;
            //        var a = new Detail
            //        {
            //            ShippedQuantity = item["quantity"].Value<decimal>(),
            //            ShiperProductCode = sku,

            //        };
            //        foreach (var xs in x)
            //        {
            //            if(xs.ShipperProductCode == sku)
            //            {
            //                a.SalesChannelProductCode = xs.SalesChannelProductCode;
            //            }
            //        }
            //        details.Add(a);
            //    };
            //    SipDataCarrierNumberMethod.Details = details;
            //    asnPackages.Add(SipDataCarrierNumberMethod);
            //}

            //string asnPackagesJson = System.Text.Json.JsonSerializer.Serialize(asnPackages);

            //using (var sw = new StreamWriter(@"C:\Users\Developer\Downloads\test\output.json", false, Encoding.UTF8))
            //{
            //    sw.WriteLine(asnPackagesJson);
            //}
            #endregion
        }

        public static T _download_serialized_json_data<T>(string id) where T : new()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                try
                {
                    json_data = w.DownloadString($"https://xkcd.com/{id}/info.0.json"); //test
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }
    }
}


