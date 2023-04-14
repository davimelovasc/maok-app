using Refit;
using System;
using System.Collections.Generic;
using System.Net;

namespace Maok.App.Utils.Handlers
{
    public static class AsyncErrorHandler
    {
        private static HashSet<int> handledExceptions = new HashSet<int>();

        public static void HandleException(Exception e)
        {
            //try
            //{
            //    Console.Error.WriteLine(e);

            //    if (!ShouldHandleException(e))
            //        return;

            //    if (HandleIfAggregatedException(e))
            //        return;

            //    IEnumerable<string> stackLines;

            //    try
            //    {
            //        stackLines = EnhancedStackTrace
            //            .Current()
            //            .ToString()?
            //            .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
            //            .Skip(1);
            //    }
            //    catch
            //    {
            //        stackLines = Environment.StackTrace.ToString()?
            //            .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
            //            .Skip(2);
            //    }

            //    var topStackTraceLines = stackLines.Take(20).ToArray();
            //    var attachments = new List<ErrorAttachmentLog>();
            //    var properties = new Dictionary<string, string>();

            //    attachments.Add(ErrorAttachmentLog.AttachmentWithText(string.Join(Environment.NewLine, topStackTraceLines), "stacktrace.txt"));

            //    if (e is ApiException apiException && apiException.HasContent)
            //    {
            //        properties.Add("URL", apiException.Uri?.ToString());
            //        attachments.Add(ErrorAttachmentLog.AttachmentWithText(apiException.Content, "response.txt"));
            //    }

            //    if (e.InnerException != null)
            //    {
            //        attachments.Add(ErrorAttachmentLog.AttachmentWithText(e.InnerException.ToString(), "inner_exception.txt"));
            //    }

            //    Crashes.TrackError(e.Demystify(), properties, attachments.ToArray());
            //    handledExceptions.Add(e.GetHashCode());
            //}
            //catch (Exception handlerException)
            //{
            //    Crashes.TrackError(handlerException);
            //    Crashes.TrackError(e);
            //}
        }

        private static bool HandleIfAggregatedException(Exception exception)
        {
            if (exception is AggregateException aggregateException && aggregateException.InnerExceptions?.Count > 0)
            {
                foreach (var innerException in aggregateException.Flatten().InnerExceptions)
                {
                    HandleException(innerException);

                    return true;
                }
            }
            return false;
        }

        //internal static bool ShouldProcessErrorReport(ErrorReport report) => ShouldHandleException(report.Exception);

        private static bool ShouldHandleException(Exception ex)
        {
            if (ex is ApiException apiEx)
            {
                if (apiEx.RequestMessage?.RequestUri?.PathAndQuery == "/auth/v2/customers/codes"
                    && apiEx.StatusCode == HttpStatusCode.Forbidden)
                {
                    return false;
                }

                if (apiEx.RequestMessage?.RequestUri?.PathAndQuery == "/auth/v2/customers/codes/validate"
                    && apiEx.StatusCode == HttpStatusCode.Forbidden)
                {
                    return false;
                }

                if (apiEx.RequestMessage?.RequestUri?.PathAndQuery == "/auth/v2/oauth/token"
                   && apiEx.StatusCode == HttpStatusCode.Forbidden)
                {
                    return false;
                }

                if (apiEx.StatusCode == HttpStatusCode.GatewayTimeout)
                {
                    return false;
                }
            }

            if (ex is System.Threading.Tasks.TaskCanceledException)
            {
                return false;
            }

            return ex != null && !handledExceptions.Contains(ex.GetHashCode());
        }
    }
}