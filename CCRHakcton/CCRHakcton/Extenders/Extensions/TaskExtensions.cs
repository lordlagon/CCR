using Microsoft.AppCenter.Crashes;
using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Core
{
    internal static class TaskExtensions
    {
        public static async Task<ApiResult<bool>> Handle(this Task self, BaseViewModel vm = null)
        {
            SetBusy(vm, true);

            try
            {
                await self.ConfigureAwait(false);
                return ApiResult.Create(true, true, HttpStatusCode.OK);
            }
            catch (ApiException ex)
            {
                return Error<bool>(ex);
            }
            catch (Exception ex)
            {
                return Error<bool>(ex);
            }
            finally
            {
                SetBusy(vm, false);
            }
        }

        public static async Task<ApiResult<T>> Handle<T>(this Task<T> self, BaseViewModel vm = null)
        {
            SetBusy(vm, true);

            try
            {
                var result = await self.ConfigureAwait(false);
                return ApiResult.Create(result, true, HttpStatusCode.OK);
            }
            catch (ApiException apiEx)
            {
                return Error<T>(apiEx);
            }
            catch (System.Exception ex)
            {
                return Error<T>(ex);
            }
            finally
            {
                SetBusy(vm, false);
            }
        }

        static void SetBusy(BaseViewModel vm, bool value)
        {
            if (vm != null) vm.IsBusy = value;
        }

        static ApiResult<T> Error<T>(ApiException apiEx)
        {
            TrackError(apiEx);

            switch (apiEx.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return ApiResult.Create(default(T), false, apiEx.StatusCode, apiEx.Message);
                case HttpStatusCode.GatewayTimeout:
                case HttpStatusCode.RequestTimeout:
                    return ApiResult.Create(default(T), false, apiEx.StatusCode,"Sem Internet");
                default:
                    return ApiResult.Create(default(T), false, apiEx.StatusCode, "Erro Servidor");
            }
        }

        static ApiResult<T> Error<T>(Exception ex)
        {
            TrackError(ex);
            return ApiResult.Create(default(T), false, HttpStatusCode.ExpectationFailed,"Erro Servidor");
        }

        static void TrackError(Exception ex)
        {
#if !DEBUG
            Crashes.TrackError(ex);
#endif
            Console.WriteLine(ConcactException(ex));
        }

        static string ConcactException(Exception ex, StringBuilder str = null)
        {
            if (str == null)
                str = new StringBuilder();

            str.AppendLine($"Message: {ex.Message}");
            str.AppendLine($"StackTrace: {ex.StackTrace}");

            if (ex.InnerException != null)
                str.AppendLine(ConcactException(ex.InnerException, str));

            return str.ToString();
        }
    }
}
