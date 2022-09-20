using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SAT.Recruitment.Business.DTO;
using SAT.Recruitment.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SAT.Recruitment.Business.Validation
{
    public class New
    {
        public class Execute: IRequest<ResultDTO>
        {
            public string Name
            {
                get; set;
            }
            public string Email
            {
                get; set;
            }
            public string Address
            {
                get; set;
            }
            public string Phone
            {
                get; set;
            }
            public string UserType
            {
                get; set;
            }
            public decimal Money
            {
                get; set;
            }
        }

        public class ExecuteValidation: AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("The name is required");
                RuleFor(x => x.Email).NotEmpty().WithMessage("The email is required");
                RuleFor(x => x.Address).NotEmpty().WithMessage("The address is required");
                RuleFor(x => x.Phone).NotEmpty().WithMessage("The phone is required");
            }
        }

        public class Handler: IRequestHandler<Execute, ResultDTO>
        {
            public Handler()
            {

            }

            public async Task<ResultDTO> Handle(Execute request, CancellationToken cancellationToken)
            {
                ExecuteValidation validation = new ExecuteValidation();
                ValidationResult validationResult = validation.Validate(request);
                ResultDTO result = new ResultDTO();

                if (validationResult.IsValid)
                {                    
                    User user = new User
                    {
                        Name = request.Name,
                        Email = Data.NormalizeEmail(request.Email),
                        Address = request.Address,
                        Phone = request.Phone,
                        UserType = request.UserType,
                        Money = Data.ValidateUserSalary(request.UserType, request.Money),
                    };

                    List<User> users = Data.GetUsersFromFile();

                    var foundUser = users.FirstOrDefault(usr => usr.Name == user.Name && usr.Address == user.Address &&
                        (usr.Email == user.Email || usr.Phone == user.Phone));

                    if (foundUser != null)
                    {
                        result.Message = "The user is duplicated";
                        result.IsSuccess = false;
                    }
                    else
                    {
                        result.Message = "User Created";
                        result.IsSuccess = true;
                        Data.WriteFileData(user);
                    }

                    await Task.Delay(1000);
                }
                else
                {
                    result.Message = validationResult.ToString(", ");
                    result.IsSuccess = false;
                }

                return result;

            }
        }
        
    }
}
