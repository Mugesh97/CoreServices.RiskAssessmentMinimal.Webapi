using System.Diagnostics.CodeAnalysis;

namespace CoreServices.RiskAssessmentMinimal.Webapi.Common
{
    [ExcludeFromCodeCoverage]
    public static class Constants
    {
        public const string RouteName = "actionlistener/";
        public static string LandMarkCallerId { get; } = "x-landmark-caller-id";
        public static string AuthUserId { get; } = "x-auth-user-id";
        public static string Authorization { get; } = "x-authorization";

        public const string AuthenticationBearer = "Bearer ";
        public const string ContentMediaType = "application/json";
        public const string ProductValidationServiceUrl = "/validation/validate";
        public const string ConstraintTypeGeometry = "geometryType";
        public const string ConstraintGeometryTypePolygon = "Polygon";
        public const string ConstraintGeometryTypeWithoutHoles = "WithoutHoles";

        public static readonly string LandmarkAuthorizationHeaderName = "x-landmark-authorization";

        public const string AcceptHeaderName = "Accept";

        public const string ApplicationJsonHeaderValue = "application/json";

        public const string ContentTypeHeaderName = "Content-Type";

        /// <summary>
        /// Claim name for allowing order on bahalf of
        /// </summary>
        public static readonly string OOBClaimName = "https://api.landmark.co.uk/claims/on_behalf_of";
    }
}
