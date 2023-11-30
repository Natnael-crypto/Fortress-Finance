namespace AdminService.Model;



public class Log{

    public int ID { get; set; }

    public string LogType { get; set; }

    public string IpAddress { get; set; }

    public string RequestDetail { get; set; }

    public string LogDateTime { get; set; }

    public string Username { get; set; }

    public string DeviceInfo { get; set; }
}