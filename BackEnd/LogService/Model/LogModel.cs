namespace LogService.Model;


// + Id: int
// + LogType: string
// + Severity: string
// + IpAddress:string
// + RequestDetail: string
// + Date:Date
// + Time: Time
// + Username: string
// + Role: string
// + Device_info: string

public class Log{

    public int ID { get; set; }

    public string LogType { get; set; }

    public string IpAddress { get; set; }

    public string RequestDetail { get; set; }

    public DateTime LogDate { get; set; }

    public DateTime LogTime { get; set; }

    public string Username { get; set; }

    public string Role { get; set; }

    public string DeviceInfo { get; set; }
}