using System;
using System.Collections.Generic;
using System.IO;
using Gdot.Care.Common.Api.ErrorHandler;
using Gdot.Care.Common.Enum;
using Gdot.Care.Common.Model;
using Gdot.Care.Common.Utilities;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Api
{
    [ExcludeFromCodeCoverage]
    public class ApiErrorMapping
    {
        public static ApiErrorResponse MapErrorResponse(ApiErrorResponse errorResponse)
        {
            var errorMappingFile = ConfigManager.GetAppSetting("ErrorMapping");
            var filePath = Utility.GetFullpath(errorMappingFile);
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var operationErrorMaps = JsonConvert.DeserializeObject<List<OperationErrorMapInfo>>(json);

                if (operationErrorMaps != null)
                {
                    foreach (var operation in operationErrorMaps)
                    {
                        if (string.Equals(operation.EventType, errorResponse.EventType,
                            StringComparison.OrdinalIgnoreCase))
                        {
                            foreach (var map in operation.Maps)
                            {
                                if (string.Equals(map.ExternalResponseCode, errorResponse.ExternalResponseCode))
                                {
                                    errorResponse.ErrorCode = (ErrorCodeEnum) map.ErrorCode;
                                    errorResponse.ErrorName = map.ErrorName;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    return errorResponse;
                }
            }
            return errorResponse;
        }
    }
}
