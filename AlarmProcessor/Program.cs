using System.Net.Mail;

namespace AlarmProcessor;

public class Program
{
    public static void Main(string[] args)
    {
        var alarmService = new AlarmService();
        alarmService.Invoke();
    }
}

public class AlarmService
{

    private readonly ConfigurationRepository _configurationRepository;
    private readonly AlarmsRepository _alarmsRepository;
    private readonly Notifier _notifier;

    public AlarmService()
    {
        _configurationRepository = new ConfigurationRepository();
        _alarmsRepository = new AlarmsRepository();
        _notifier = new Notifier();
    }

    public void Invoke()
    {
        var destination = _configurationRepository.Get("destination");
        var alarms = _alarmsRepository.GetAll();
        foreach (var alarm in alarms)
        {
            _notifier.Notify(destination, alarm);
        }
    }
    
}

public class Notifier
{
    public void Notify(string destination, string alarm)
    {
        Console.WriteLine($"{destination} : {alarm}");
    }
}

public class AlarmsRepository
{
    public List<string> GetAll()
    {
        return new List<string>() {"an alarm", "another alarm"};
    }
}

public class ConfigurationRepository
{
    public string Get(string property)
    {
        if (property == "destination")
        {
            return "foo destination";
        }

        return "";
    }
}