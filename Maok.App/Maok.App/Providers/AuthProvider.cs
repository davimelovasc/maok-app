using Maok.App.Modules.Login.Models;
using Maok.App.Modules.Login.Services;
using Maok.App.Modules.Login.Services.Dtos.Request;
using Maok.App.Modules.Shared.PageModels;
using Refit;
using System;
using System.Threading.Tasks;

namespace Maok.App.Providers
{
    public class AuthProvider
    {
        private static readonly Lazy<AuthProvider> _instance = _instance ??= new Lazy<AuthProvider>(() => new AuthProvider());
        public static AuthProvider Instance => _instance.Value;
        public HttpTokenModel Token { get; protected set; }

        public void SetToken(HttpTokenModel token)
        {
            Token = token;
        }

        public void ClearToken()
        {
            Token = null;
        }

        public async Task Login<T>(LoginRequestDto request, BasePageModel<T> pageModel)
        {
            try
            {
                var apiResponse = RestService.For<ILoginService>("https://maok-api.herokuapp.com");
                var token = await apiResponse.Auth(request);

                SetToken(new HttpTokenModel(token));
            }
            //catch (ApiException e)
            //{
            //    pageModel.SetErrorApi(e);
            //}
            catch (Exception ex)
            {
                //pageModel.SetErrorApi(ex.ToString());
            }
        }

        //public async Task Logout<T>(BasePageModel<T> pageModel)
        //{
        //    try
        //    {
        //        await _authService.LogoutAsync();
        //    }
        //    catch (ApiException e)
        //    {
        //        pageModel.SetErrorApi(e);
        //    }
        //}

        //public async Task PostPassword<T>(PasswordRecoveryRequestDto requestAuth, BasePageModel<T> pageModel)
        //{
        //    try
        //    {
        //        var encryption = await Encryption.GetInstance();
        //        requestAuth.Document = encryption.Encrypt(requestAuth.Document);
        //        requestAuth.Password = encryption.Encrypt(requestAuth.Password);

        //        await _authService.PutPasswordRecoveryAsync(requestAuth);

        //    }
        //    catch (ApiException e)
        //    {
        //        pageModel.SetErrorApi(e);
        //    }
        //}

        //public async Task<bool> PostFingerprint<T>(string document, BasePageModel<T> pageModel)
        //{
        //    try
        //    {
        //        await _authService.PostFingerprintAsync(new FingerprintRequestDto(document));
        //        return true;
        //    }
        //    catch (ApiException e)
        //    {
        //        AsyncErrorHandler.HandleException(e);
        //    }
        //    return false;
        //}

        //public async Task PostRegisterBiometry(BiometricRequestDto request)
        //{
        //    if (request.Password != "0")
        //    {
        //        var encryption = await Encryption.GetInstance();
        //        request.Password = encryption.Encrypt(request.Password);
        //    }

        //    await _authService.PostRegisterBiometryAsync(request);
        //}

        //public async Task RefreshToken()
        //{
        //    try
        //    {
        //        var tokenResponse = await _authService.RefreshTokenAsync(new RefreshTokenRequestDto() { RefreshToken = Instance.Token.RefreshToken });

        //        if (tokenResponse != null)
        //        {
        //            SetToken(MapperProvider.Instance.Map<HttpTokenModel>(tokenResponse));
        //        }
        //        else
        //        {
        //            ClearToken();
        //        }
        //    }
        //    catch (Exception ApiException)
        //    {
        //        AsyncErrorHandler.HandleException(ApiException);
        //        App.Logout(LogoutReason.Unauthorized);
        //    }
        //}
    }
}