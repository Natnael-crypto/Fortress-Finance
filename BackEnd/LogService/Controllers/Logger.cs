namespace LogService.Controller;
class Program
{
    static public async Task SendPostRequest(string LogType,string IpAddress,string RequestDetail,string LogDateTime ,string UserName,string DeviceInfo)
    {
        
        string apiUrl = "http://localhost:5135/api/log";

        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                
                
                var requestBody = new
                {
                    LogType=LogType,
                    IpAddress=IpAddress,
                    RequestDetail=RequestDetail,
                    LogDateTime=LogDateTime,
                    UserName=UserName,
                    DeviceInfo=DeviceInfo
                };

                
                string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);

                
                StringContent content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");

                
                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
