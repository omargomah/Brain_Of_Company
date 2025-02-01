//using Interfaces;
//using System.Security.Claims;

//namespace Brain_API
//{
//    public class SoftDeleteOfEmployeeMiddleWare : IMiddleware
//    {


//        private readonly IUnitOfWork unitOfWork;

//        public SoftDeleteOfEmployeeMiddleWare(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;
//        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
//        {
//            bool check = unitOfWork.Employees.IsDeletedBySSN();
//            if (UserId is not null)
//            {
//                bool? check = tokenRepo.CheckTokenIsRevoked(UserId);
//                if (check is not null && (bool)check)
//                {
//                    await context.Response.WriteAsync("Invalid Token Login again");
//                    return;
//                }
//            }
//            await next(context);
//        }
//    }
//}