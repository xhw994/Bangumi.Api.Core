using System;

namespace Bangumi.Api.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new DefaultApiService();
            var res = api.GetSubject(253, Model.Subjects.ResponseGroup.Large);
            Console.WriteLine(res.ToString());
        }
    }
}
