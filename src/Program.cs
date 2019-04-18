using System;
using System.Collections.Generic;
using System.Web;
using Bangumi.Api.Core.Client;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.Subjects;

namespace Bangumi.Api.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            DefaultApiService api = new DefaultApiService();
            BangumiClient client = new BangumiClient();
            //var res = api.GetSubject(253, Model.Subjects.ResponseGroup.Large);
            //Console.WriteLine(res.ToString());
            //string xf = "小圆 新房昭之";
            //var res = api.SearchSubjectByKeywords(xf, Model.Subjects.SubjectType.Anime);
            //Console.WriteLine(res);
            var res = api.GetDailyCalendar();
            //var res = client.Request<IEnumerable<CalendarResponse>>(new DailyCalendarRequest());
            foreach (var r in res)
            {
                Console.WriteLine(r);
            }
        }
    }
}
