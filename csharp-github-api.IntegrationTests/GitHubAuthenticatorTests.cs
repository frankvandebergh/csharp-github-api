﻿using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;

namespace csharp_github_api.IntegrationTests
{
    [TestFixture]
    public class GitHubAuthenticatorTests
    {
        private SecretsHandler _secretsHandler;
        private RestRequest _restRequest;

        [SetUp]
        public void Setup()
        {
            _secretsHandler = new SecretsHandler(@"C:\development\secretpasswordfile.xml");

            _restRequest = new RestRequest
            {
                Resource = "/users/sgrassie"
            };
        }

        [Test]
        public void MakeAuthenticatedRequest()
        {
            var client = new RestClient
                             {
                                 BaseUrl ="https://api.github.com",
                                 Authenticator = new GitHubAuthenticator(_secretsHandler.Username, _secretsHandler.Password, false)
                             };

            var response = client.Execute(_restRequest);

            Assert.That(response.Content, Is.StringContaining("total_private_repos"));
        }

        [Test]
        [Ignore("API v3 doesn't use tokens anymore")]
        [Obsolete]
        public void MakeAuthenticatedRequestWithToken()
        {
            var client = new RestClient
            {
                BaseUrl = "api.github.com",
                Authenticator = new GitHubAuthenticator(_secretsHandler.Username, _secretsHandler.Token, true)
            };

            var response = client.Execute(_restRequest);

            Assert.That(response.Content, Is.StringContaining("total_private_repos"));
        }
    }
}
