using System;

namespace DotNetCore.Cap.Demo.Publisher.PgModels
{
    public partial class ApiConfigParam
    {
        public long Id { get; set; }
        public long ApiConfigId { get; set; }
        public string ParamType { get; set; }
        public string ParamName { get; set; }
        public string ParamDefaultValue { get; set; }
        public bool IsRequired { get; set; }
        public string ParamDesc { get; set; }
        public short ParamSort { get; set; }
        public string OperCode { get; set; }
        public DateTime OperTime { get; set; }

        public ApiConfig ApiConfig { get; set; }
    }
}
