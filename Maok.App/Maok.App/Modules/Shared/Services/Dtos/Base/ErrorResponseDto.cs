using Newtonsoft.Json;
using System;
using System.Linq;

namespace Maok.App.Modules.Shared.Services.Dtos.Base
{
    public class ErrorResponseDto
    {
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("alert")]
        public string Alert { get; set; }

        [JsonProperty("errors")]
        public ErrorResponse[] Errors { get; set; }

        public class ErrorResponse
        {
            [JsonProperty("category")]
            public string Category { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("mensagem")]
            public string Message2 { get; set; }

            [JsonProperty("campo")]
            public string Field { get; set; }
        }

        private string FormatError(ErrorResponseDto error)
        {
            var msg = string.Empty;

            if (error == null)
            {
                return string.Empty;
            }

            if (!string.IsNullOrEmpty(error?.Alert))
                return error.Alert;

            if (!string.IsNullOrEmpty(error.ErrorDescription))
            {
                msg += error.ErrorDescription + "\n";
            }

            if (error.Errors != null && error.Errors.Length > 0)
            {
                if (error.Errors.Any(x => !string.IsNullOrEmpty(x.Field) && x.Message2.Contains("obrigat")))
                    msg = error.Errors.Aggregate(msg, (current, e) => current + (e.Message + e.Message2 + " " + char.ToUpper(e.Field[0]) + e.Field.Substring(1) + "\n"));
                else
                    msg = error.Errors.Aggregate(msg, (current, e) => current + (e.Message + e.Message2 + "\n"));
            }

            if (!string.IsNullOrEmpty(msg))
            {
                msg = msg.TrimEnd('\n');
            }

            return msg;
        }

        public override string ToString()
        {
            return FormatError(this);
        }
    }
}