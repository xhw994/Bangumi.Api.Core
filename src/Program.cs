using System;
using System.Web;

namespace Bangumi.Api.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new DefaultApiService();
            //var res = api.GetSubject(253, Model.Subjects.ResponseGroup.Large);
            //Console.WriteLine(res.ToString());
            string xf = "小圆 新房昭之";
            var res = api.SearchSubjectByKeywords(xf, Model.Subjects.SubjectType.Anime);
            Console.WriteLine(res);
        }
    }
}
