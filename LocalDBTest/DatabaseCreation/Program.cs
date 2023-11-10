namespace DatabaseCreation
{
  internal class Program
  {
    static async Task Main(string[] args)
    {
      //EliMathEnigme();

      RawDataGenerator generator = new RawDataGenerator();

      await generator.Generate(@"C:\Users\sideshowbob\Downloads\clientsList.json", @"C:\Users\sideshowbob\Downloads\productList.json").ConfigureAwait(false);
      Console.ReadLine();
    }

    static void EliMathEnigme()
    {
      List<int> candidatPotentiels = new List<int>();
      List<int> uniqueDigit = new List<int>();
      int tmp = 0;

      for(int i = 100; i < 999; i++)
      {
        if(i % 10 == 0)
          continue;
        if(i % 9 == 0)
          continue;
        if(i % 4 == 0)
          continue;
        if(i % 5 == 0)
          continue;
        if(i % 6 == 0)
          continue;
        if(i % 2 == 0)
          continue;
        if(i % 3 != 0)
          continue;

        uniqueDigit.Clear();
        tmp = i;
        for(; tmp > 0; tmp /= 10)
        {
          if(uniqueDigit.Contains(tmp % 10) == false)
          {
            uniqueDigit.Add(tmp % 10);
          }
          else
            break;
        }

        if(uniqueDigit.Count != 3) continue;

        if(uniqueDigit[1] < uniqueDigit[0] || uniqueDigit[1] < uniqueDigit[2]) continue;

        if(!(uniqueDigit[0] == 3 ||
         uniqueDigit[0] == 6 ||
         uniqueDigit[1] == 3 ||
         uniqueDigit[1] == 6 ||
         uniqueDigit[2] == 3 ||
         uniqueDigit[2] == 6))
          continue;

        if(uniqueDigit[0] == 4 ||
         uniqueDigit[0] == 9 ||
         uniqueDigit[1] == 4 ||
         uniqueDigit[1] == 9 ||
         uniqueDigit[2] == 4 ||
         uniqueDigit[2] == 9)
          continue;

        if(!(uniqueDigit[0] % 2 == 0 ||
         uniqueDigit[1] % 2 == 0 ||
         uniqueDigit[2] % 2 == 0))
          continue;

        if(!(uniqueDigit[0] % 2 != 0 ||
         uniqueDigit[1] % 2 != 0 ||
         uniqueDigit[2] % 2 != 0))
          continue;

        candidatPotentiels.Add(i);
      }

      candidatPotentiels.ForEach(i => Console.WriteLine(i));
      Console.ReadLine();
    }

  }
}