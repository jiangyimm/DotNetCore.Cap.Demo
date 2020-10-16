using System;
using System.Collections.Generic;

namespace DotNetCore.Cap.Demo.Publisher.PgModels
{
    public partial class ApiConfig
    {
        public ApiConfig()
        {
            ApiConfigParam = new HashSet<ApiConfigParam>();
        }

        public long Id { get; set; }
        public string ApiName { get; set; }
        public string ApiDesc { get; set; }
        public string ReturnType { get; set; }
        public string ReturnExpect { get; set; }
        public bool IsAsync { get; set; }
        public string OperCode { get; set; }
        public DateTime OperTime { get; set; }

        public ICollection<ApiConfigParam> ApiConfigParam { get; set; }
    }
}
