using System;

namespace Bangumi.Api.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new DefaultApi();
            var res = api.CollectionBySubjectIdGet(253);
            Console.WriteLine(res.ToString());
        }
    }
}
