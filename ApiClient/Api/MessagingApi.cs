using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vevro.Rasa.ApiClient.Client;
using Vevro.Rasa.ApiClient.Model;

namespace Vevro.Rasa.ApiClient.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IMessagingApiAsync : IApiAccessor
    {
        Task<Vevro.Rasa.ApiClient.Client.ApiResponse<List<WebhookMessage>>> SendMessageAsyncWithHttpInfo(string conversationId, string message);
        Task<List<WebhookMessage>> SendMessageAsync(string conversationId, string message);
    }
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IMessagingApiSync : IApiAccessor
    {
        Vevro.Rasa.ApiClient.Client.ApiResponse<List<WebhookMessage>> SendMessageWithHttpInfo(string conversationId, string message);
        List<WebhookMessage> SendMessage(string conversationId, string message);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IMessagingApi : IMessagingApiAsync, IMessagingApiSync { }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class MessagingApi : IMessagingApi
    {
        private Vevro.Rasa.ApiClient.Client.ExceptionFactory _exceptionFactory = (name, response) => null;
        private const string RestPath = "/webhooks/rest/webhook";

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingApi"/> class.
        /// </summary>
        /// <returns></returns>
        public MessagingApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingApi"/> class.
        /// </summary>
        /// <returns></returns>
        public MessagingApi(String basePath)
        {
            this.Configuration = Vevro.Rasa.ApiClient.Client.Configuration.MergeConfigurations(
                Vevro.Rasa.ApiClient.Client.GlobalConfiguration.Instance,
                new Vevro.Rasa.ApiClient.Client.Configuration { BasePath = basePath }
            );
            this.Client = new Vevro.Rasa.ApiClient.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new Vevro.Rasa.ApiClient.Client.ApiClient(this.Configuration.BasePath);
            this.ExceptionFactory = Vevro.Rasa.ApiClient.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public MessagingApi(Vevro.Rasa.ApiClient.Client.Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = Vevro.Rasa.ApiClient.Client.Configuration.MergeConfigurations(
                Vevro.Rasa.ApiClient.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.Client = new Vevro.Rasa.ApiClient.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new Vevro.Rasa.ApiClient.Client.ApiClient(this.Configuration.BasePath);
            ExceptionFactory = Vevro.Rasa.ApiClient.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public MessagingApi(Vevro.Rasa.ApiClient.Client.ISynchronousClient client, Vevro.Rasa.ApiClient.Client.IAsynchronousClient asyncClient, Vevro.Rasa.ApiClient.Client.IReadableConfiguration configuration)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (asyncClient == null) throw new ArgumentNullException("asyncClient");
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = Vevro.Rasa.ApiClient.Client.Configuration.DefaultExceptionFactory;
        }


        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public Vevro.Rasa.ApiClient.Client.IReadableConfiguration Configuration { get; set; }


        public string GetBasePath()
        {
            return this.Configuration.BasePath;
        }
        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public Vevro.Rasa.ApiClient.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public Vevro.Rasa.ApiClient.Client.IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public Vevro.Rasa.ApiClient.Client.ISynchronousClient Client { get; set; }

        public async Task<Vevro.Rasa.ApiClient.Client.ApiResponse<List<WebhookMessage>>> SendMessageAsyncWithHttpInfo(string conversationId, string message)
        {
            // verify the required parameter 'conversationId' is set
            if (conversationId == null)
                throw new Vevro.Rasa.ApiClient.Client.ApiException(400, "Missing required parameter 'conversationId' when calling MessagingApi->SendMessage");

            // verify the required parameter 'message' is set
            if (message == null)
                throw new Vevro.Rasa.ApiClient.Client.ApiException(400, "Missing required parameter 'message' when calling MessagingApi->SendMessage");


            Vevro.Rasa.ApiClient.Client.RequestOptions requestOptions = new Vevro.Rasa.ApiClient.Client.RequestOptions();

            String[] @contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] @accepts = new String[] {
                "application/json"
            };

            foreach (var contentType in @contentTypes)
                requestOptions.HeaderParameters.Add("Content-Type", contentType);

            foreach (var accept in @accepts)
                requestOptions.HeaderParameters.Add("Accept", accept);

            requestOptions.Data = new WebhookMessage { Sender = conversationId, Message = message };

            // authentication (JWT) required
            // http basic authentication required
            if (!String.IsNullOrEmpty(this.Configuration.Username) || !String.IsNullOrEmpty(this.Configuration.Password))
            {
                requestOptions.HeaderParameters.Add("Authorization", "Basic " + Vevro.Rasa.ApiClient.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }
            // authentication (TokenAuth) required
            if (!String.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("token")))
            {
                foreach (var kvp in Vevro.Rasa.ApiClient.Client.ClientUtils.ParameterToMultiMap("", "token", this.Configuration.GetApiKeyWithPrefix("token")))
                {
                    foreach (var value in kvp.Value)
                    {
                        requestOptions.QueryParameters.Add(kvp.Key, value);
                    }
                }
            }

            // make the HTTP request

            var response = await this.AsynchronousClient.PostAsync<List<WebhookMessage>>(RestPath, requestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception exception = this.ExceptionFactory("ConversationsConversationIdMessagesPost", response);
                if (exception != null) throw exception;
            }

            return response;
        }
        public async Task<List<WebhookMessage>> SendMessageAsync(string conversationId, string message)
        {
            Vevro.Rasa.ApiClient.Client.ApiResponse<List<WebhookMessage>> localVarResponse = await SendMessageAsyncWithHttpInfo(conversationId, message);
            return localVarResponse.Data;
        }


        public Vevro.Rasa.ApiClient.Client.ApiResponse<List<WebhookMessage>> SendMessageWithHttpInfo(string conversationId, string message)
        {
            // verify the required parameter 'conversationId' is set
            if (conversationId == null)
                throw new Vevro.Rasa.ApiClient.Client.ApiException(400, "Missing required parameter 'conversationId' when calling TrackerApi->ConversationsConversationIdMessagesPost");

            // verify the required parameter 'message' is set
            if (message == null)
                throw new Vevro.Rasa.ApiClient.Client.ApiException(400, "Missing required parameter 'message' when calling TrackerApi->ConversationsConversationIdMessagesPost");

            Vevro.Rasa.ApiClient.Client.RequestOptions requestOptions = new Vevro.Rasa.ApiClient.Client.RequestOptions();

            String[] @contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] @accepts = new String[] {
                "application/json"
            };

            var localVarContentType = Vevro.Rasa.ApiClient.Client.ClientUtils.SelectHeaderContentType(@contentTypes);
            if (localVarContentType != null) requestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Vevro.Rasa.ApiClient.Client.ClientUtils.SelectHeaderAccept(@accepts);
            if (localVarAccept != null) requestOptions.HeaderParameters.Add("Accept", localVarAccept);

            requestOptions.Data = new WebhookMessage { Sender = conversationId, Message = message };

            // authentication (JWT) required
            // http basic authentication required
            if (!String.IsNullOrEmpty(this.Configuration.Username) || !String.IsNullOrEmpty(this.Configuration.Password))
            {
                requestOptions.HeaderParameters.Add("Authorization", "Basic " + Vevro.Rasa.ApiClient.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }
            // authentication (TokenAuth) required
            if (!String.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("token")))
            {
                foreach (var kvp in Vevro.Rasa.ApiClient.Client.ClientUtils.ParameterToMultiMap("", "token", this.Configuration.GetApiKeyWithPrefix("token")))
                {
                    foreach (var value in kvp.Value)
                    {
                        requestOptions.QueryParameters.Add(kvp.Key, value);
                    }
                }
            }

            // make the HTTP request

            var response = this.Client.Post<List<WebhookMessage>>(RestPath, requestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception exception = this.ExceptionFactory("ConversationsConversationIdMessagesPost", response);
                if (exception != null) throw exception;
            }

            return response;
        }

        public List<WebhookMessage> SendMessage(string conversationId, string message)
        {
            Vevro.Rasa.ApiClient.Client.ApiResponse<List<WebhookMessage>> localVarResponse = SendMessageWithHttpInfo(conversationId, message);
            return localVarResponse.Data;
        }
    }
}
