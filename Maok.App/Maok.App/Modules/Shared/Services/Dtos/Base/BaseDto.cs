using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maok.App.Modules.Shared.Services.Dtos.Base
{
    public class BaseRequestDto
    {
        [AliasAs("client_id")]
        public string ClientId { get; set; }//=> AppConfigurationProvider.Instance.ClientId;

        [AliasAs("fingerprint")]
        public string Fingerprint { get; set; } //=> FingerprintProvider.Instance.Key;
    }

    public class BaseResponseDto
    {
        public string Hash { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("idSolicitacao")]
        public int IdSolicitation { get; set; }

        [JsonProperty("paginaAtual")]
        public int CurrentPage { get; set; }

        [JsonProperty("totalDePaginas")]
        public int PagesTotal { get; set; }

        [JsonProperty("itensPorPagina")]
        public int PageItems { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}