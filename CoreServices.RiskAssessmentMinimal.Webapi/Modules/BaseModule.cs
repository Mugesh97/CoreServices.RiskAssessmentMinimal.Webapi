using System.Diagnostics.CodeAnalysis;

namespace CoreServices.RiskAssessmentMinimal.Webapi.Modules
{

    [ExcludeFromCodeCoverage]
    public class BaseModule
    {
        #region protected fields

        protected IConfiguration? _config;

        #endregion

        #region constructor

        public BaseModule(IConfiguration config)
        {
            _config = config;
        }

        #endregion
    }
}
