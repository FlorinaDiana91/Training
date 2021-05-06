using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Training.Job.Client
{
    public class ServiceClient
    {
        public enum ExceptionDataKeys
        {
            ResponseContent
        }

        private const string HEADER_VALUE_JSON = "application/json";
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public static T MakeServiceCallWithFiles<T>(string serviceUrl, string methodUrl, Method methodType,
                                           Dictionary<string, string> urlSegments = null,
                                           Dictionary<string, string> urlParameters = null,
                                           string serializedBodyParameter = null,
                                           bool useJsonHelperSerializer = false,
                                           int timeoutInSeconds = 30,
                                           HttpBasicAuthenticator authenticator = null) where T : new()
        {
            _logger.Info("MakeServiceCall serviceUrl:{0} methodUrl:{1} methodType:{2}", serviceUrl, methodUrl, methodType.ToString());

            // Restsharp cannot handle both body and URL parameters
            if (!string.IsNullOrWhiteSpace(serializedBodyParameter) && urlParameters != null && urlParameters.Any())
            {
                throw new Exception("Invalid parameter configuration. Cannot pass in both urlParameters and a serializedBodyParameter.");
            }

            // WebAPI will not accept body parameters for GET requests by default. This is difficult to track down so let's throw an exception.
            if (methodType == Method.GET && !string.IsNullOrWhiteSpace(serializedBodyParameter))
            {
                throw new Exception("Web API will not correctly deserialize GET requests with body parameters. Please change method type or use urlParameters instead.");
            }

            var client = new RestClient(serviceUrl);
            if (authenticator != null)
            {
                client.Authenticator = authenticator;
            }
            var request = new RestRequest(methodUrl, methodType);

            request.RequestFormat = DataFormat.Json;
            request.Timeout = timeoutInSeconds * 1000; // Needs to be set in milliseconds

            AddParameters(request, urlSegments, urlParameters, serializedBodyParameter);

            // Check if we need to use an alternate serializer
            if (useJsonHelperSerializer)
            {
                IRestResponse jsonHelperResponse = client.Execute(request);
                _logger.Info("MakeServiceCall RestSharp Response Status (jsonHelper): " + jsonHelperResponse.ResponseStatus);
                return HandleResponse<T>(jsonHelperResponse);
            }

            // Set up the response
            var response = client.Execute<T>(request);
            _logger.Info("MakeServiceCall RestSharp Response Status: " + response.ResponseStatus);

            return HandleResponse(response);
        }

        public static T MakeServiceCall<T>(string serviceUrl, string methodUrl, Method methodType,
                                           Dictionary<string, string> urlSegments = null,
                                           Dictionary<string, string> urlParameters = null,
                                           string serializedBodyParameter = null,
                                           bool useJsonHelperSerializer = false,
                                           int timeoutInSeconds = 30,
                                           HttpBasicAuthenticator authenticator = null,
                                           bool isAthena = false) where T : new()
        {
            return MakeServiceCallWithFiles<T>(
                serviceUrl,
                methodUrl,
                methodType,
                urlSegments: urlSegments,
                urlParameters: urlParameters,
                serializedBodyParameter: serializedBodyParameter,
                useJsonHelperSerializer: useJsonHelperSerializer,
                timeoutInSeconds: timeoutInSeconds,
                authenticator: authenticator);
        }

        /// <summary>
        /// A version of make service call that always returns the raw string content.  Returns null for void calls.
        /// </summary>
        /// <param name="serviceUrl"></param>
        /// <param name="methodUrl"></param>
        /// <param name="methodType"></param>
        /// <param name="urlSegments"></param>
        /// <param name="urlParameters"></param>
        /// <param name="serializedBodyParameter"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public static string MakeServiceCall(string serviceUrl, string methodUrl, Method methodType,
                                           Dictionary<string, string> urlSegments = null,
                                           Dictionary<string, string> urlParameters = null,
                                           string serializedBodyParameter = null,
                                           int timeoutInSeconds = 30,
                                           HttpBasicAuthenticator authenticator = null)
        {
            _logger.Info("MakeServiceCall (string) serviceUrl:{0} methodUrl:{1} methodType:{2}", serviceUrl, methodUrl, methodType.ToString());

            // Restsharp cannot handle both body and URL parameters (or at least I haven't figured out how to get it to)
            if (!string.IsNullOrWhiteSpace(serializedBodyParameter) && urlParameters != null && urlParameters.Any())
            {
                throw new Exception("Invalid parameter configuration. Cannot pass in both urlParameters and a serializedBodyParameter.");
            }

            var client = new RestClient(serviceUrl);
            if (authenticator != null)
            {
                client.Authenticator = authenticator;
            }
            var request = new RestRequest(methodUrl, methodType);

            request.RequestFormat = DataFormat.Json;
            request.Timeout = timeoutInSeconds * 1000; // Needs to be set in milliseconds

            AddParameters(request, urlSegments, urlParameters, serializedBodyParameter);

            _logger.Info("Service call expected response is of type string, special case without deserialization.");
            var stringResponse = client.Execute(request);
            _logger.Info("MakeServiceCall RestSharp Response Status (string): " + stringResponse.ResponseStatus);

            return HandleResponse(stringResponse);
        }



        private static void AddParameters(IRestRequest request,
                                          Dictionary<string, string> urlSegments = null,
                                          Dictionary<string, string> urlParameters = null,
                                          string serializedBodyParameter = null)
        {

            // Add URL segments if they exist
            if (urlSegments != null && urlSegments.Any())
            {
                _logger.Trace("Adding {0} urlSegments", urlSegments.Count);
                foreach (var urlSegment in urlSegments)
                {
                    request.AddUrlSegment(urlSegment.Key, urlSegment.Value);
                }
            }

            // Add URL parameters if they exist
            if (urlParameters != null && urlParameters.Any())
            {
                _logger.Trace("Adding {0} urlParameters", urlParameters.Count);
                foreach (var urlParameter in urlParameters)
                {
                    request.AddParameter(urlParameter.Key, urlParameter.Value);
                }
            }

            // Add body parameter if it exists
            if (!string.IsNullOrWhiteSpace(serializedBodyParameter))
            {
                _logger.Trace("Adding serializedBodyParameter");
                request.AddParameter(HEADER_VALUE_JSON, serializedBodyParameter, ParameterType.RequestBody);
            }

        }



        public static T HandleResponse<T>(IRestResponse response)
        {
            if (response.ResponseStatus == RestSharp.ResponseStatus.Error)
            {
                throw NewServiceCommunicationException(response);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = JsonHelper.Deserialize<T>(response.Content);
                    return result;
                }
                else
                {
                    throw new Exceptions.JobServiceException(response.StatusCode, response.Content);
                }
            }
        }

        public static T HandleResponse<T>(IRestResponse<T> response)
        {
            if (response.ResponseStatus == RestSharp.ResponseStatus.Error)
            {
                throw NewServiceCommunicationException(response);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return response.Data;
                }
                else
                {
                    throw new Exceptions.JobServiceException(response.StatusCode, response.Content);
                }
            }
        }

        public static string HandleResponse(IRestResponse response)
        {
            if (response.ResponseStatus == RestSharp.ResponseStatus.Error)
            {
                throw NewServiceCommunicationException(response);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return response.Content.Trim('"');
                }
                else
                {
                    throw new Exceptions.JobServiceException(response.StatusCode, response.Content);
                }
            }
        }

        private static Exception NewServiceCommunicationException(IRestResponse response)
        {
            var ex = new Exception("Error communicating with service");

            ex.Data["Rest Response Error Message"] = response.ErrorMessage;
            ex.Data[ExceptionDataKeys.ResponseContent.ToString()] = response.Content;

            if (response.Request != null)
            {
                ex.Data["Request Resource"] = response.Request.Resource;
            }

            return ex;
        }
    }
}
