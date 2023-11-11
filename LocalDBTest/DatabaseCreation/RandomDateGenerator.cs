namespace DatabaseCreation
{
  internal class RandomDateGenerator
  {
    private DateTime _startTime;
    private int _dayRange;
    private Random _rand;

    public RandomDateGenerator(DateTime startTime, DateTime endTime, Random? rand = null)
    { 
      _startTime = startTime;
      _dayRange = (int)endTime.Subtract(startTime).TotalDays;
      _rand = rand ?? new Random();
    }

    public DateTime Next()
    {
      return _startTime.AddDays(_rand.NextDouble() * (_dayRange + 1));
    }
  }
}
