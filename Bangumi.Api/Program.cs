using System;

namespace Bangumi.Api
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new DefaultApi();
            var res = api.CalendarGet();
            foreach (var r in res)
            {
                Console.WriteLine(r.ToJson());
            }
        }
    }
}
