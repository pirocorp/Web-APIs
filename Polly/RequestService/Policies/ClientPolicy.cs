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
        }

        /// <summary>
        /// Retry immediately 5x times
        /// </summary>
        public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get; }
    }
}
