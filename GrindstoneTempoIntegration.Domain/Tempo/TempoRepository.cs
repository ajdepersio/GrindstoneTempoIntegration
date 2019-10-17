using GrindstoneTempoIntegration.Domain.Tempo.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrindstoneTempoIntegration.Domain.Tempo
{
    public class TempoRepository
    {
        private static string _bearertoken;
        public static string BearerToken
        {
            private get 
            {
                return _bearertoken;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(_bearertoken))
                {
                    _bearertoken = value;
                }
                else
                {
                    throw new InvalidOperationException("Value already set");
                }
            }
        }

        private static readonly RestClient Client = new RestClient("https://api.tempo.io/core/3");

        public GetTeamsResponse GetTeams()
        {
            var request = new RestRequest("teams", Method.GET);
            request.AddHeader("Authorization", $"Bearer {BearerToken}");

            IRestResponse response = Client.Execute(request);
            var content = response.Content;

            var getTeamsResponse = JsonConvert.DeserializeObject<GetTeamsResponse>(content);

            return getTeamsResponse;
        }

        public GetMembersResponse GetMembers(int teamId)
        {
            var request = new RestRequest($"teams/{teamId}/members", Method.GET);
            request.AddHeader("Authorization", $"Bearer {BearerToken}");

            IRestResponse response = Client.Execute(request);
            var content = response.Content;

            var getMembersResponse = JsonConvert.DeserializeObject<GetMembersResponse>(content);

            return getMembersResponse;
        }
    }
}
