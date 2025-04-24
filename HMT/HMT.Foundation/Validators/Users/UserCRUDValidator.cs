using HMT.Foundation.DTOs;
using HMT.Foundation.Enums;
using HMT.Foundation.Helpers;

namespace HMT.Foundation.Validators.Users
{
    public class UserCRUDValidator
    {
        public List<VResponse> VAddEditUser(UsersDataDto data)
        {
            if (data is null)
                return
                [
                    new()
                        {
                            ResponseType = VResponseTypes.UnknownError,
                            Info = "not found"
                        }
                ];

            List<VResponse> result = [];

            if (string.IsNullOrEmpty(data.Name))
            {
                result.Add(new VResponse
                {
                    ResponseType = VResponseTypes.SomethingWrong,
                    Info = "no name"
                });
            }
            else if (data.Name.Length < 6 || data.Name.Length > 20)
            {
                result.Add(new VResponse
                {
                    ResponseType = VResponseTypes.SomethingWrong,
                    Info = "name length count"
                });
            }

            if (string.IsNullOrEmpty(data.Email))
                result.Add(new VResponse
                {
                    ResponseType = VResponseTypes.SomethingWrong,
                    Info = "no email"
                });            
            else
            {
                if (!EmailHelpers.SimpleEmailValidation(data.Email))
                    result.Add(new VResponse()
                    {
                        ResponseType = VResponseTypes.SomethingWrong,
                        Info = "wrong email"
                    });
            }

            if (string.IsNullOrEmpty(data.Password))
                result.Add(new VResponse
                {
                    ResponseType = VResponseTypes.SomethingWrong,
                    Info = "no password"
                });
            else
            {
                if (data.Password.Length < 6 || data.Password.Length > 20)
                    result.Add(new VResponse
                    {
                        ResponseType = VResponseTypes.SomethingWrong,
                        Info = "password lenght count"
                    });
            }

            if (result.Count == 0)
                result.Add(new VResponse()
                {
                    ResponseType = VResponseTypes.Valid,
                    Info = "all good"
                });

            return result;
        }
    }
}