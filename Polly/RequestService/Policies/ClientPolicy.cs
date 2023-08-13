namespace RequestService.Policies
{
    using Polly;
    using Polly.Retry;

    /// <summary>
    /// Contains all policies defined in this demo
    /// </summary>
    public class ClientPolicy
    {
        public ClientPolicy()
        {
            this.ImmediateHttpRetry = Policy
                .HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .RetryAsync(5);

            this.LinearHttpRetry = Policy
                .HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(3));

            this.ExponentialHttpRetry = Policy
                .HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        /// <summary>
        /// Retry immediately 5x times
        /// </summary>
        public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get; }

        public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; }

        public AsyncRetryPolicy<HttpResponseMessage> ExponentialHttpRetry { get; }
    }
}
