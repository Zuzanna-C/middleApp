using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDynamicApiClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Configure((apiUri, apiToken) =>
            //{
                //UseAnonymousApiFlow(apiUri, apiToken);
                //UseTypedApiFlow(apiUri, apiToken);
            //});
        }

        void Configure(Action<string, string> action)
        {
            // Configuration implementation
        }

        void UseAnonymousApiFlow(string apiUri, string apiToken)
        {
            // UseAnonymousApiFlow implementation
        }

        void UseTypedApiFlow(string apiUri, string apiToken)
        {
            // UseTypedApiFlow implementation
        }

        void ProceedResult(dynamic data)
        {
            // ProceedResult implementation
        }

        void LogError(object err)
        {
            // LogError implementation
        }
    }
}
